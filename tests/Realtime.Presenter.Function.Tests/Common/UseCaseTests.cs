using System;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Logging;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Auth;
using Microsoft.WindowsAzure.Storage.Blob;
using Moq;
using Realtime.Presenter.Function.Common.Storage;
using Realtime.Presenter.Function.Tests.Fakes;
using Realtime.Presenter.Test.Utilities.Fakes;

namespace Realtime.Presenter.Function.Tests.Common
{
    public class UseCaseTests
    {
        private readonly IServiceProvider _provider;
        private readonly Mock<CloudBlobClient> _blobClientMock;
        private readonly Mock<CloudBlobContainer> _filesContainerMock;

        private FakeLogger Logger { get; }

        protected FakeHttpClient HttpClient { get; }

        protected Mock<CloudStorageAccount> StorageAccount { get; }

        private IConfiguration Config { get; }

        protected UseCaseTests()
        {
            var loggerFactory = new FakeLoggerFactory();
            Logger = loggerFactory.Logger;
            
            var httpClientFactory = new FakeHttpClientFactory();
            HttpClient = httpClientFactory.Client;
            
            var configurationFactory = new ConfigurationFactory();
            Config = configurationFactory.Create();

            _filesContainerMock = new Mock<CloudBlobContainer>(new Uri("https://something.com/files"));
            _blobClientMock = new Mock<CloudBlobClient>(new Uri("https://something.com"));
            _blobClientMock.Setup(s => s.GetContainerReference("files")).Returns(_filesContainerMock.Object);

            StorageAccount = new Mock<CloudStorageAccount>(new StorageCredentials("storageaccount", ""), false);
            StorageAccount.Setup(s => s.CreateCloudBlobClient()).Returns(_blobClientMock.Object);
            
            var storageAccountFactory = new Mock<IStorageAccountFactory>();
            storageAccountFactory.Setup(s => s.Get()).Returns(StorageAccount.Object);


            _provider = new ServiceCollection()
                .AddRealTimePresenter(Config)
                
                .RemoveAll<ILoggerFactory>()
                .AddSingleton<ILoggerFactory>(loggerFactory)
                
                .RemoveAll<IHttpClientFactory>()
                .AddSingleton<IHttpClientFactory>(httpClientFactory)
                .AddSingleton(httpClientFactory)
                
                .RemoveAll<IConfiguration>()
                .AddSingleton(Config)
                
                .RemoveAll<IStorageAccountFactory>()
                .AddSingleton(storageAccountFactory.Object)
                
                .BuildServiceProvider();
        }

        protected T Get<T>()
        {
            return _provider.GetService<T>();
        }

        protected void SetupFileBlob(string blob, string contents)
        {
            var blockBlob = new Mock<CloudBlockBlob>(new Uri($"https://something.com/files/{blob}"));
            blockBlob.Setup(s => s.DownloadTextAsync())
                .ReturnsAsync(contents);
            
            _filesContainerMock.Setup(s => s.GetBlockBlobReference(blob))
                .Returns(blockBlob.Object);
        }

        protected void SetupFileBlob(string blob, byte[] bytes)
        {
            var blockBlob = new Mock<CloudBlockBlob>(new Uri($"https://something.com/files/{blob}"));
            blockBlob.Setup(s => s.DownloadToStreamAsync(It.IsAny<Stream>()))
                .Callback<Stream>(s => s.Write(bytes))
                .Returns(Task.CompletedTask);

            _filesContainerMock.Setup(s => s.GetBlockBlobReference(blob))
                .Returns(blockBlob.Object);
        }
    }
}