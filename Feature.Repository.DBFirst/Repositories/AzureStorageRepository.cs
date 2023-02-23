using Feature.Azure.Storage;
using Feature.Azure.Storage.Models;
using Feature.Repository.Interface.Interfaces;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Feature.Repository.DBFirst.Repositories
{
    public class AzureStorageRepository : IAzureStorageRepository,IAzureStorage
    {
        private readonly IAzureStorage _storage;
        public AzureStorageRepository(IAzureStorage storage)
        {
            _storage = storage;
        }

        public Task<BlobResponseDto> DeleteAsync(string blobFilename)
        {
            return _storage.DeleteAsync(blobFilename);
        }

        public Task<BlobDto> DownloadAsync(string blobFilename)
        {
            return _storage.DownloadAsync(blobFilename);
        }

        public Task<List<BlobDto>> ListAsync()
        {
            return _storage.ListAsync();
        }

        public Task<BlobResponseDto> UploadAsync(IFormFile file)
        {
            return _storage.UploadAsync(file);
        }
    }
}
