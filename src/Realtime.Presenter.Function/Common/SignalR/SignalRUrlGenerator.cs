using Microsoft.Extensions.Configuration;

namespace Realtime.Presenter.Function.Common.SignalR
{
    public interface ISignalRUrlGenerator
    {
        string ServerUrl(string hubName);
        string ClientUrl(string hubName);
    }

    public class SignalRUrlGenerator : ISignalRUrlGenerator
    {
        private readonly IConfiguration _config;

        private string Endpoint => _config["SignalR:Endpoint"];

        public SignalRUrlGenerator(IConfiguration config)
        {
            _config = config;
        }

        public string ServerUrl(string hubName)
        {
            return $"{Endpoint}/api/v1/hubs/{hubName}";
        }

        public string ClientUrl(string hubName)
        {
            return $"{Endpoint}/client/?hub={hubName}";
        }
    }
}