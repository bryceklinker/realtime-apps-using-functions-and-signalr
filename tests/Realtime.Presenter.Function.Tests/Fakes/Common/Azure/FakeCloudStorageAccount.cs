using Microsoft.WindowsAzure.Storage.Blob;
using Realtime.Presenter.Function.Common.Azure;

namespace Realtime.Presenter.Function.Tests.Fakes.Common.Azure
{
    public class FakeCloudStorageAccount : ICloudStorageAccount
    {
        public FakeCloudBlobClient BlobClient { get; } = new FakeCloudBlobClient();
        
        public CloudBlobClient CreateCloudBlobClient()
        {
            return BlobClient;
        }

        public bool DoesContainerExist(string containerName)
        {
            return BlobClient.GetFakeContainer(containerName) != null;
        }

        public FakeCloudBlockBlob GetBlob(string containerName, string blobName)
        {
            var container = BlobClient.GetFakeContainer(containerName);
            return container.GetFakeBlob(blobName);
        }
    }
}