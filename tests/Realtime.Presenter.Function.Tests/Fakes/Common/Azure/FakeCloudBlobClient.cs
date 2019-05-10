using System;
using System.Collections.Generic;
using Microsoft.WindowsAzure.Storage.Blob;

namespace Realtime.Presenter.Function.Tests.Fakes.Common.Azure
{
    public class FakeCloudBlobClient : CloudBlobClient
    {
        private readonly List<FakeCloudBlobContainer> _containers;
        
        public FakeCloudBlobClient() 
            : base(new Uri("https://something.com"))
        {
            _containers = new List<FakeCloudBlobContainer>();
        }

        public override CloudBlobContainer GetContainerReference(string containerName)
        {
            if (!_containers.Exists(c => c.Name == containerName))
                _containers.Add(new FakeCloudBlobContainer(new Uri($"{BaseUri}{containerName}")));
            
            return _containers.Find(c => c.Name == containerName);
        }

        public FakeCloudBlobContainer GetFakeContainer(string name)
        {
            return _containers.Find(c => c.Name == name);
        }
    }
}