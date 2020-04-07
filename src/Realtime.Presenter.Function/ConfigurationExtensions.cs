using Microsoft.Extensions.Configuration;

namespace Realtime.Presenter.Function
{
    public static class ConfigurationExtensions
    {
        public static string GetStorageAccountConnectionString(this IConfiguration configuration)
        {
            return configuration["StorageAccountConnectionString"];
        }
    }
}