using Amazon.S3.Model;
using Feature.AWS.Storage.Interfaces;
using Feature.Services.Abstract;
using Microsoft.AspNetCore.Http;

namespace Feature.Services.Concrete
{
    public class FileService : IFileService
    {
        private readonly IFileStorage _fileStorage;
        public FileService(IFileStorage fileStorage)
        {
            _fileStorage = fileStorage;
        }
        public async Task<PutObjectResponse> UploadFileAsync(IFormFile formFile, string bucketName, string? prefix)
        {
            return await _fileStorage.UploadFileAsync(formFile, bucketName, prefix);
        }
        public async Task<ListObjectsResponse> GetAllFilesAsync(string bucketName, string prefix)
        {
            return await _fileStorage.GetAllFilesAsync(bucketName, prefix);
        }
        public async Task<GetObjectResponse> GetFileByKeyAsync(string bucketName, string key)
        {
            return await _fileStorage.GetFileByKeyAsync(bucketName, key);
        }
        public async Task<DeleteObjectResponse> DeleteFileAsync(string bucketName, string key)
        {
            return await _fileStorage.DeleteFileAsync(bucketName, key);
        }
        public void DownloadFileAsync(string filePath, string bucketName, string S3Directory)
        {
            _fileStorage.DownloadFileAsync(filePath, bucketName, S3Directory);
        }
        public Task<byte[]> DownloadFileAsync(string fileName, string bucketName)
        {
            return _fileStorage.DownloadFileAsync(fileName, bucketName);
        }
    }
}
