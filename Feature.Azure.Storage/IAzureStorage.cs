using Feature.Azure.Storage.Models;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Feature.Azure.Storage
{
    public interface IAzureStorage
    {

        Task<BlobResponseDto> UploadAsync(IFormFile file);
        Task<BlobDto> DownloadAsync(string blobFilename);
        Task<BlobResponseDto> DeleteAsync(string blobFilename);
        Task<List<BlobDto>> ListAsync();
    }
}
