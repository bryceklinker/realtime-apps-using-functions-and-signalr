using System;
using System.Net.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Logging;
using Realtime.Presenter.Function.Tests.Fakes;
using Realtime.Presenter.Test.Utilities.Fakes;

namespace Realtime.Presenter.Function.Tests.Common
{
    public class UseCaseTests
    {
        private readonly IServiceProvider _provider;

        public FakeLogger Logger { get; }
        
        public FakeHttpClient HttpClient { get; }
        
        public IConfiguration Config { get; }
        
        public UseCaseTests()
        {
            var loggerFactory = new FakeLoggerFactory();
            Logger = loggerFactory.Logger;
            
            var httpClientFactory = new FakeHttpClientFactory();
            HttpClient = httpClientFactory.Client;
            
            var configurationFactory = new ConfigurationFactory();
            Config = configurationFactory.Create();
            
            _provider = new ServiceCollection()
                .AddRealTimePresenter(Config)
                
                .RemoveAll<ILoggerFactory>()
                .AddSingleton<ILoggerFactory>(loggerFactory)
                
                .RemoveAll<IHttpClientFactory>()
                .AddSingleton<IHttpClientFactory>(httpClientFactory)
                .AddSingleton(httpClientFactory)
                
                .RemoveAll<IConfiguration>()
                .AddSingleton(Config)
                
                .BuildServiceProvider();
        }
        
        public T Get<T>()
        {
            return _provider.GetService<T>();
        }

        public T GetObjectResult<T>(IActionResult result)
        {
            var okResult = (OkObjectResult) result;
            return (T) okResult.Value;
        }
    }
}