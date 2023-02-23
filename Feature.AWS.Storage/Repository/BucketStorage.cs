using System.Data;
using Amazon.S3;
using Amazon.S3.Model;
using Feature.AWS.Storage.Interfaces;
using Microsoft.AspNetCore.Http;

namespace Feature.AWS.Storage.Repository
{
    public class BucketStorage : IBucketStorage
    {
        private readonly IAmazonS3 _amazonS3;
        public BucketStorage(IAmazonS3 amazonS3)
        {
            _amazonS3 = amazonS3;
        }
        public async Task<PutBucketResponse> CreateBucketAsync(string bucketName)
        {
            bool bucketExists = await _amazonS3.DoesS3BucketExistAsync(bucketName);
            if (!bucketExists)
            {
                PutBucketResponse putBucketResponse = await _amazonS3.PutBucketAsync(bucketName);
                return putBucketResponse;
            }
            throw new DuplicateNameException();
        }
        public async Task<ListBucketsResponse> GetAllBucketsAsync()
        {
            ListBucketsResponse listBucketsResponse = await _amazonS3.ListBucketsAsync();
            return listBucketsResponse;
        }
        public async Task<DeleteBucketResponse> DeleteBucketAsync(string bucketName)
        {
            bool bucketExists = await _amazonS3.DoesS3BucketExistAsync(bucketName);
            if (bucketExists)
            {
                DeleteBucketResponse deleteBucketResponse = await _amazonS3.DeleteBucketAsync(bucketName);
                return deleteBucketResponse;
            }
            throw new KeyNotFoundException();
        }
    }
}
