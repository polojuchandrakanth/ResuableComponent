using Amazon.S3.Model;
using Feature.Services.Abstract;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;


namespace Feature.API.Controllers
{
    [AllowAnonymous]
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class BucketController : ControllerBase
    {
        private readonly ILogger<BucketController> _seriLogger;
        private readonly IBucketService _bucketService;
        public BucketController(ILogger<BucketController> seriLogger, IBucketService bucketService)
        {
            _seriLogger = seriLogger;
            _bucketService = bucketService;
        }
        [Route("Create Bucket")]
        [HttpPost]
        public async Task<PutBucketResponse> CreateBucketAsync(string bucketName)
        {
            _seriLogger.LogInformation("Create Bucket");
            PutBucketResponse putBucketResponse = await _bucketService.CreateBucketAsync(bucketName);
            return putBucketResponse;
        }
        [Route("GetAllBuckets")]
        [HttpGet]
        public async Task<ListBucketsResponse> GetAllBucketsAsync()
        {
            _seriLogger.LogInformation("Get All Buckets");
            ListBucketsResponse listBucketsResponse =  await _bucketService.GetAllBucketsAsync();
            return listBucketsResponse;
        }
        [Route("DeleteBucket")]
        [HttpDelete]
        public async Task<DeleteBucketResponse> DeleteBucketAsync(string bucketName)
        {
            _seriLogger.LogInformation("Delete Buckets");
            DeleteBucketResponse deleteBucketResponse = await _bucketService.DeleteBucketAsync(bucketName);
            return deleteBucketResponse;
        }
    }
}
