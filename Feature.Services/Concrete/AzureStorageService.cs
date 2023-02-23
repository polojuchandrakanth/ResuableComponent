using Feature.Azure.Storage.Models;
using Feature.Repository.Interface.Interfaces;
using Feature.Services.Abstract;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Feature.Services.Concrete
{
    public class AzureStorageService : IAzureStorageService
    {
        private readonly IAzureStorageRepository _azureRepository;
        public AzureStorageService(IAzureStorageRepository azureRepository)
        {
            _azureRepository = azureRepository;
        }

        public Task<BlobResponseDto> DeleteAsync(string blobFilename)
        {
            return  _azureRepository.DeleteAsync(blobFilename);
        }

        public Task<BlobDto> DownloadAsync(string blobFilename)
        {
            return _azureRepository.DownloadAsync(blobFilename);
        }

        public Task<List<BlobDto>> ListAsync()
        {
            return _azureRepository.ListAsync();
        }

        public Task<BlobResponseDto> UploadAsync(IFormFile file)
        {
            return _azureRepository.UploadAsync(file);
        }
    }
}
