using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.WindowsAzure.Storage.Blob;

namespace Realtime.Presenter.Function.Tests.Fakes.Common.Azure
{
    public class FakeCloudBlobContainer : CloudBlobContainer
    {
        private readonly List<FakeCloudBlockBlob> _blobs;
        private bool _exists;

        public FakeCloudBlobContainer(Uri containerUri) 
            : base(containerUri)
        {
            _exists = false;
            _blobs = new List<FakeCloudBlockBlob>();
        }

        public override Task<bool> CreateIfNotExistsAsync()
        {
            _exists = true;
            return Task.FromResult(_exists);
        }

        public override CloudBlockBlob GetBlockBlobReference(string blobName)
        {
            if (!_blobs.Exists(b => b.Name == blobName)) 
                _blobs.Add(new FakeCloudBlockBlob(new Uri($"{Uri}/{blobName}")));
            return _blobs.Find(b => b.Name == blobName);
        }
        
        public FakeCloudBlockBlob GetFakeBlob(string blobName)
        {
            return _blobs.Find(b => b.Name == blobName);
        }
    }
}