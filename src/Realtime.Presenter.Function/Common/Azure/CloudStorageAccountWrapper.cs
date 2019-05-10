using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;

namespace Realtime.Presenter.Function.Common.Azure
{
    public interface ICloudStorageAccount
    {
        CloudBlobClient CreateCloudBlobClient();
    }

    public class CloudStorageAccountWrapper : ICloudStorageAccount
    {
        private readonly CloudStorageAccount _storageAccount;

        public CloudStorageAccountWrapper(CloudStorageAccount storageAccount)
        {
            _storageAccount = storageAccount;
        }

        public CloudBlobClient CreateCloudBlobClient()
        {
            return _storageAccount.CreateCloudBlobClient();
        }
    }
}