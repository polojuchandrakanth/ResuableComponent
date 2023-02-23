using System.Data;
using System.Net;
using Amazon.S3;
using Amazon.S3.Model;
using Amazon.S3.Transfer;
using Feature.AWS.Storage.Interfaces;
using Microsoft.AspNetCore.Http;

namespace Feature.AWS.Storage.Repository
{
    public class FileStorage : IFileStorage
    {
        private readonly IAmazonS3 _amazonS3;
        public FileStorage(IAmazonS3 amazonS3)
        {
            _amazonS3 = amazonS3;
        }
        public async Task<PutObjectResponse> UploadFileAsync(IFormFile formFile, string bucketName, string? prefix)
        {
            bool bucketExists = await _amazonS3.DoesS3BucketExistAsync(bucketName);
            if (bucketExists)
            {
                var request = new PutObjectRequest()
                {
                    BucketName = bucketName,
                    Key = string.IsNullOrEmpty(prefix) ? formFile.FileName : $"{prefix?.TrimEnd('/')}/{formFile.FileName}",
                    InputStream = formFile.OpenReadStream(),
                };
                request.Metadata.Add("Content-Type", formFile.ContentType);
                PutObjectResponse putObjectResponse = await _amazonS3.PutObjectAsync(request);
                return putObjectResponse;
            }
            throw new DuplicateNameException();
        }
        public async Task<ListObjectsResponse> GetAllFilesAsync(string bucketName, string prefix)
        {
            bool bucketExists = await _amazonS3.DoesS3BucketExistAsync(bucketName);
            if (bucketExists)
            {
                ListObjectsRequest request = new ListObjectsRequest()
                {
                    BucketName = bucketName,
                    Prefix = prefix,
                };
                ListObjectsResponse listObjectsResponse = await _amazonS3.ListObjectsAsync(request);
                return listObjectsResponse;
            }
            throw new NullReferenceException();
        }
        public async Task<GetObjectResponse> GetFileByKeyAsync(string bucketName, string key)
        {
            bool bucketExists = await _amazonS3.DoesS3BucketExistAsync(bucketName);
            if (bucketExists)
            {
                using GetObjectResponse getObjectResponse = await _amazonS3.GetObjectAsync(bucketName, key);
                return getObjectResponse;
            }
            throw new KeyNotFoundException();
        }
        public async Task<DeleteObjectResponse> DeleteFileAsync(string bucketName, string key)
        {
            bool bucketExists = await _amazonS3.DoesS3BucketExistAsync(bucketName);
            if (bucketExists)
            {
                DeleteObjectResponse deleteObjectResponse = await _amazonS3.DeleteObjectAsync(bucketName, key);
                return deleteObjectResponse;
            }
            throw new KeyNotFoundException();
        }
        public void DownloadFileAsync(string filePath, string bucketName, string S3Directory)
        {
            using TransferUtility transferUtility = new TransferUtility();
            transferUtility.DownloadDirectory(bucketName, S3Directory, filePath);
        }
        public async Task<byte[]> DownloadFileAsync(string fileName, string bucketName)
        {
            MemoryStream ms = null;

            try
            {
                GetObjectRequest getObjectRequest = new GetObjectRequest
                {
                    BucketName = bucketName,
                    Key = fileName
                };

                using (var response = await _amazonS3.GetObjectAsync(getObjectRequest))
                {
                    if (response.HttpStatusCode == HttpStatusCode.OK)
                    {
                        using (ms = new MemoryStream())
                        {
                            await response.ResponseStream.CopyToAsync(ms);
                        }
                    }
                }

                if (ms is null || ms.ToArray().Length < 1)
                    throw new FileNotFoundException(string.Format("The document '{0}' is not found", fileName));

                return ms.ToArray();
            }
            catch (Exception)
            {
                throw;
            }
        }

    }
}
