using System;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Logging;
using Realtime.Presenter.Function.Common.Azure;
using Realtime.Presenter.Function.Tests.Fakes.Common.Azure;

namespace Realtime.Presenter.Function.Tests.Fakes
{
    public class UseCaseTests
    {
        private readonly IServiceProvider _provider;

        public FakeCloudStorageAccount StorageAccount { get; }
        
        public FakeLogger Logger { get; }
        
        public UseCaseTests()
        {
            var accountFactory = new FakeStorageAccountFactory();
            StorageAccount = accountFactory.Account;
            
            var loggerFactory = new FakeLoggerFactory();
            Logger = loggerFactory.Logger;
            
            _provider = new ServiceCollection()
                .AddRealTimePresenter(new ConfigurationBuilder().Build())
                .RemoveAll<IStorageAccountFactory>()
                .AddSingleton<IStorageAccountFactory>(accountFactory)
                .RemoveAll<ILoggerFactory>()
                .AddSingleton<ILoggerFactory>(loggerFactory)
                .BuildServiceProvider();
        }
        
        public T Create<T>()
        {
            return _provider.GetService<T>();
        }
    }
}