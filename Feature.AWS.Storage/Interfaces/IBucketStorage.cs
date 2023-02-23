using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Amazon.S3.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Feature.AWS.Storage.Interfaces
{
    public interface IBucketStorage
    {
        Task<PutBucketResponse> CreateBucketAsync(string bucketName);
        Task<ListBucketsResponse> GetAllBucketsAsync();
        Task<DeleteBucketResponse> DeleteBucketAsync(string bucketName);
    }
}
