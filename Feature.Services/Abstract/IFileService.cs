using Amazon.S3.Model;
using Microsoft.AspNetCore.Http;

namespace Feature.Services.Abstract
{
    public interface IFileService
    {
        Task<PutObjectResponse> UploadFileAsync(IFormFile formFile, string bucketName, string? prefix);
        Task<ListObjectsResponse> GetAllFilesAsync(string bucketName, string prefix);
        Task<GetObjectResponse> GetFileByKeyAsync(string bucketName, string key);
        Task<DeleteObjectResponse> DeleteFileAsync(string bucketName, string key);
        void DownloadFileAsync(string filePath, string bucketName, string S3Directory);
        Task<byte[]> DownloadFileAsync(string fileName, string bucketName);
    }
}
