using Amazon.S3;
using Amazon.S3.Model;
using Feature.Services.Abstract;
using Microsoft.AspNetCore.Mvc;

namespace Feature.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FileController : ControllerBase
    {
        private readonly ILogger<BucketController> _seriLogger;
        private readonly IFileService _fileService;
        private readonly string _noFile = "NoSuchKey";
        private ILogger<FileController> object1;
        private IFileService object2;

        public FileController(ILogger<BucketController> seriLogger, IFileService fileService)
        {
            _seriLogger = seriLogger;
            _fileService = fileService;
        }

        public FileController(ILogger<FileController> object1, IFileService object2)
        {
            this.object1 = object1;
            this.object2 = object2;
        }

        [Route("Upload Files")]
        [HttpPost]
        public async Task<PutObjectResponse> UploadFileAsync(IFormFile formFile, string bucketName, string? prefix)
        {
            _seriLogger.LogInformation("Uploading Files to S3 Buckets");
            PutObjectResponse putObjectResponse = await _fileService.UploadFileAsync(formFile, bucketName, prefix);
            return putObjectResponse;
        }
        [Route("GetAllFiles")]
        [HttpGet]
        public async Task<ListObjectsResponse> GetAllFilesAsync(string bucketName, string prefix)
        {
            _seriLogger.LogInformation("Get All Files in S3 Bucket");
            ListObjectsResponse listObjectsResponse = await _fileService.GetAllFilesAsync(bucketName, prefix);
            return listObjectsResponse;
        }
        [Route("GetFileByKey")]
        [HttpGet]
        public async Task<GetObjectResponse> GetFileByKeyAsync(string bucketName, string key)
        {
            _seriLogger.LogInformation("Get File by key in S3 Bucket");
            GetObjectResponse getObjectResponse = await _fileService.GetFileByKeyAsync(bucketName, key);
            return getObjectResponse;
        }
        [Route("DeleteFiles")]
        [HttpDelete]
        public async Task<DeleteObjectResponse> DeleteFileAsync(string bucketName, string key)
        {
            _seriLogger.LogInformation("Delete Files in S3 Buckets");
            DeleteObjectResponse deleteObjectResponse = await _fileService.DeleteFileAsync(bucketName, key);
            return deleteObjectResponse;
        }
        [Route("DownloadFiles")]
        [HttpGet]
        public void DownloadFileAsync(string filePath, string bucketName, string S3Directory)
        {
            _seriLogger.LogInformation("Downloading Files");
            _fileService.DownloadFileAsync(filePath, bucketName, S3Directory);
        }
        [Route("Download Single File")]
        [HttpGet]
        public IActionResult DownloadFileAsync(string fileName, string bucketName)
        {
            _seriLogger.LogInformation("Downloading Single File from S3 Bucket");
            try
            {
                if (string.IsNullOrEmpty(fileName))
                    throw new KeyNotFoundException();

                var document = _fileService.DownloadFileAsync(fileName, bucketName).Result;

                return File(document, "application/octet-stream", fileName);
            }
            catch (AmazonS3Exception ex)
            {
                if (ex.ErrorCode.Contains(_noFile))
                {
                    throw new FileNotFoundException(string.Format("The document '{0}' is not found", fileName));
                }
                else
                    throw;

            }
            catch (Exception)
            {
                throw;
            }
        }

    }
}
