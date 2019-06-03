using System;
using Microsoft.WindowsAzure.Storage.Blob;
using Realtime.Presenter.Function.Common.Storage;

namespace Realtime.Presenter.Function.Files.Storage
{
    public interface IFilesStorageService
    {
        CloudBlockBlob GetBlob(string blobName);
    }

    public class FilesStorageService : IFilesStorageService
    {
        private readonly IStorageAccountFactory _storageAccountFactory;
        private readonly Lazy<CloudBlobContainer> _lazyFilesContainer;

        private CloudBlobContainer FilesContainer => _lazyFilesContainer.Value;
        
        public FilesStorageService(IStorageAccountFactory storageAccountFactory)
        {
            _storageAccountFactory = storageAccountFactory;
            _lazyFilesContainer = new Lazy<CloudBlobContainer>(GetFilesContainer);
        }

        public CloudBlockBlob GetBlob(string blobName)
        {
            return FilesContainer.GetBlockBlobReference(blobName);
        }

        private CloudBlobContainer GetFilesContainer()
        {
            var storageAccount = _storageAccountFactory.Get();
            var client = storageAccount.CreateCloudBlobClient();
            return client.GetContainerReference("files");
        }
    }
}