using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.WindowsAzure.Storage.Blob;
using Newtonsoft.Json;
using Realtime.Presenter.Function.Common.Azure;
using Realtime.Presenter.Function.Presentations.Dtos;

namespace Realtime.Presenter.Function.Presentations.Services
{
    public interface IPresentationsService
    {
        Task<PresentationDto> AddAsync(PresentationDto dto);
        Task<PresentationDto[]> GetAllAsync();
    }

    public class PresentationsService : IPresentationsService
    {
        private readonly IStorageAccountFactory _storageAccountFactory;

        private ICloudStorageAccount StorageAccount => _storageAccountFactory.Get();

        private CloudBlobClient BlobClient => StorageAccount.CreateCloudBlobClient();

        private CloudBlobContainer PresentationsContainer => BlobClient.GetContainerReference("presentations");
        
        public PresentationsService(IStorageAccountFactory storageAccountFactory)
        {
            _storageAccountFactory = storageAccountFactory;
        }

        public async Task<PresentationDto> AddAsync(PresentationDto dto)
        {
            dto.Id = Guid.NewGuid();
            var presentations = await GetPresentationsListAsync();
            presentations.Add(dto);
            
            var blob = await GetPresentationsBlobAsync();
            await blob.UploadTextAsync(JsonConvert.SerializeObject(presentations));
            return dto;
        }

        public async Task<PresentationDto[]> GetAllAsync()
        {
            return (await GetPresentationsListAsync()).ToArray();
        }

        private async Task<List<PresentationDto>> GetPresentationsListAsync()
        {
            var blob = await GetPresentationsBlobAsync();
            return await blob.ExistsAsync()
                ? await GetPresentationsFromBlobAsync()
                : new List<PresentationDto>();
        }

        private async Task<List<PresentationDto>> GetPresentationsFromBlobAsync()
        {
            var blob = await GetPresentationsBlobAsync();
            return JsonConvert.DeserializeObject<List<PresentationDto>>(await blob.DownloadTextAsync());
        }

        private async Task<CloudBlockBlob> GetPresentationsBlobAsync()
        {
            var container = PresentationsContainer;
            await container.CreateIfNotExistsAsync();
            var blob = container.GetBlockBlobReference("presentations.json");
            return blob;
        }
    }
}