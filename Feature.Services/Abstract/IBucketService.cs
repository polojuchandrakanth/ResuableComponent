using Amazon.S3.Model;
using Microsoft.AspNetCore.Http;

namespace Feature.Services.Abstract
{
    public interface IBucketService
    {
        Task<PutBucketResponse> CreateBucketAsync(string bucketName);
        Task<ListBucketsResponse> GetAllBucketsAsync();
        Task<DeleteBucketResponse> DeleteBucketAsync(string bucketName);
    }
}
