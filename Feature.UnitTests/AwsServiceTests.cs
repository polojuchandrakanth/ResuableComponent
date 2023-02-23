using System.Net;
using Amazon.S3;
using Amazon.S3.Model;
using Feature.API.Controllers;
using Feature.AWS.Storage.Interfaces;
using Feature.Services.Abstract;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using NUnit.Framework;

namespace Feature.UnitTests
{
    public class AwsServiceTests
    {
        private Mock<ILogger<BucketController>> _seriLogger;
        private Mock<IBucketService> _bucketServiceMock;
        private Mock<IBucketStorage> _bucketStorageMock;
        private BucketController _bucketControllerTest;
        private Mock<ILogger<FileController>> _seriLoggerMock;
        private Mock<IFileService> _fileServiceMock;
        private FileController _fileControllerTest;

        [SetUp]
        public void Setup()
        {
            _bucketServiceMock = new Mock<IBucketService>();
            _seriLogger = new Mock<ILogger<BucketController>>();
            _bucketStorageMock = new Mock<IBucketStorage>();
            _bucketControllerTest = new BucketController(_seriLogger.Object, _bucketServiceMock.Object);
            _fileServiceMock = new Mock<IFileService>();
            _seriLoggerMock = new Mock<ILogger<FileController>>();
            _fileControllerTest = new FileController(_seriLoggerMock.Object, _fileServiceMock.Object);
        }
        [Test]
        public async Task GetAllBucketsAsync_ReturnsSuccessfully()
        {
            _bucketServiceMock.Setup(m => m.GetAllBucketsAsync());
            var result = _bucketControllerTest.GetAllBucketsAsync();
            Assert.IsTrue(result.IsCompleted);
        }
        [Test]
        public async Task CreateBucket_ReturnsSuccessfully()
        {
            var bucketName = "qentelli";
            _bucketServiceMock.Setup(m => m.CreateBucketAsync(bucketName));
            var result = _bucketControllerTest.CreateBucketAsync(bucketName);
            Assert.IsTrue(result.IsCompleted);
        }
        [Test]
        public async Task CreateBucket_ReturnsDuplicate()
        {
            var bucketName = "qentelli";
            _bucketServiceMock.Setup(m => m.CreateBucketAsync(bucketName));
            var result = await _bucketControllerTest.CreateBucketAsync(bucketName);
            Assert.IsNull(result);
        }
        [Test]
        public async Task CreateBucket_NotReturnsSuccessfully()
        {
            var bucketName = "qentelli";
            _bucketServiceMock.Setup(m => m.CreateBucketAsync(bucketName));
            var result = await _bucketControllerTest.CreateBucketAsync(bucketName);
            Assert.IsTrue(result is null);
        }
        [Test]
        public async Task DeleteBucketsAsync_ReturnsSuccessfully()
        {
            var bucketName = "qentelli";
            _bucketServiceMock.Setup(m => m.DeleteBucketAsync(bucketName));
            var result = _bucketControllerTest.DeleteBucketAsync(bucketName);
            Assert.IsTrue(result.IsCompleted);
        }
        [Test]
        public async Task DeleteBucketsAsync_NotReturnsSuccessfully()
        {
            var bucketName = "srikari";
            _bucketServiceMock.Setup(m => m.DeleteBucketAsync(bucketName));
            var result = _bucketControllerTest.DeleteBucketAsync(bucketName);
            Assert.IsTrue(result.IsCompleted);
        }
        [Test]
        public async Task GetAllFilesAsync_ReturnsSuccessfully()
        {
            string bucketName = "qentelli";
            string prefix = "test";
            _fileServiceMock.Setup(m => m.GetAllFilesAsync(bucketName, prefix));
            var result = _fileControllerTest.GetAllFilesAsync(bucketName: bucketName, prefix: prefix);
            Assert.IsTrue(result.IsCompleted);
        }
        [Test]
        public async Task GetAllFilesAsync_NotReturnsSuccessfully()
        {
            string bucketName = "Qentelli";
            string prefix = "test";
            _fileServiceMock.Setup(m => m.GetAllFilesAsync(bucketName, prefix));
            var result = _fileControllerTest.GetAllFilesAsync(bucketName, prefix);
            Assert.IsTrue(result.IsCompleted);
        }
        [Test]
        public async Task GetFileByKeyAsync_ReturnsSuccessfully()
        {
            string bucketName = "qentelli";
            var key = "test/CORS.docx";
            _fileServiceMock.Setup(m => m.GetFileByKeyAsync(bucketName, key));
            var result = _fileControllerTest.GetFileByKeyAsync(bucketName, key);
            Assert.IsTrue(result.IsCompleted);
        }
        [Test]
        public async Task GetFileByKeyAsync_NotReturnsSuccessfully()
        {
            string bucketName = "Qentelli";
            var key = "test/CORS.docx";
            _fileServiceMock.Setup(m => m.GetFileByKeyAsync(bucketName, key));
            var result = _fileControllerTest.GetFileByKeyAsync(bucketName, key);
            Assert.IsTrue(result.IsCompleted);
        }
        [Test]
        public async Task DeleteFileAsync_ReturnsSuccessfully()
        {
            string bucketName = "qentelli";
            var key = "test/CORS.docx";
            _fileServiceMock.Setup(m => m.DeleteFileAsync(bucketName, key));
            var result = _fileControllerTest.DeleteFileAsync(bucketName, key);
            Assert.IsNotNull(result);
        }
        [Test]
        public async Task DeleteFileAsync_NotReturnsSuccessfully()
        {
            string bucketName = "Qentelli";
            var key = "test/CORS.docx";
            _fileServiceMock.Setup(m => m.DeleteFileAsync(bucketName, key));
            var result = _fileControllerTest.DeleteFileAsync(bucketName, key);
            Assert.IsTrue(result.IsFaulted);
        }
    }
}
