using System.Collections.Generic;
using Microsoft.Extensions.Configuration;

namespace Realtime.Presenter.Function.Tests.Fakes
{
    public class ConfigurationFactory
    {
        public const string SignalREndpoint = "https://signalr.com/";
        public const string SignalRKey = "aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa=";

        public IConfiguration Create()
        {
            return new ConfigurationBuilder()
                .AddInMemoryCollection(new []
                {
                    new KeyValuePair<string, string>("SignalR:Endpoint", SignalREndpoint), 
                     new KeyValuePair<string, string>("SignalR:Key", SignalRKey), 
                })
                .Build();
        }
    }
}