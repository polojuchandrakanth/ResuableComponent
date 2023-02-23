using Amazon.S3.Model;
using Feature.AWS.Storage.Interfaces;
using Feature.Services.Abstract;

namespace Feature.Services.Concrete
{
    public class BucketService : IBucketService
    {
        private readonly IBucketStorage _bucketStorage;
        public BucketService(IBucketStorage bucketbuStorage)
        {
            _bucketStorage = bucketbuStorage;
        }
        public async Task<PutBucketResponse> CreateBucketAsync(string bucketName)
        {
            return await _bucketStorage.CreateBucketAsync(bucketName);
        }
        public async Task<ListBucketsResponse> GetAllBucketsAsync()
        {
            return await _bucketStorage.GetAllBucketsAsync();
        }
        public async Task<DeleteBucketResponse> DeleteBucketAsync(string bucketName)
        {
            return await _bucketStorage.DeleteBucketAsync(bucketName);
        }
    }
}
