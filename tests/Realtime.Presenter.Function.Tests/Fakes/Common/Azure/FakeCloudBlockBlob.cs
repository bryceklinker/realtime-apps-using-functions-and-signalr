using System;
using System.Threading.Tasks;
using Microsoft.WindowsAzure.Storage.Blob;

namespace Realtime.Presenter.Function.Tests.Fakes.Common.Azure
{
    public class FakeCloudBlockBlob : CloudBlockBlob
    {
        public bool Exists { get; private set; }
        public string Contents { get; private set; }
        
        public FakeCloudBlockBlob(Uri blobUri) 
            : base(blobUri)
        {
        }

        public override Task<bool> ExistsAsync()
        {
            return Task.FromResult(Exists);
        }

        public override Task<string> DownloadTextAsync()
        {
            return Task.FromResult(Contents);
        }

        public override Task UploadTextAsync(string content)
        {
            Exists = true;
            Contents = content;
            return Task.CompletedTask;
        }
    }
}