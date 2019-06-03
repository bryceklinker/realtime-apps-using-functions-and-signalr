using Microsoft.Extensions.Configuration;

namespace Realtime.Presenter.Function
{
    public static class ConfigurationExtensions
    {
        public static string GetStorageAccountConnectionString(this IConfiguration configuration)
        {
            return configuration["StorageAccountConnectionString"];
        }

        public static string GetSignalREndpoint(this IConfiguration configuration)
        {
            return configuration["SignalR:Endpoint"];
        }

        public static string GetSignalRKey(this IConfiguration configuration)
        {
            return configuration["SignalR:Key"];
        }
    }
}