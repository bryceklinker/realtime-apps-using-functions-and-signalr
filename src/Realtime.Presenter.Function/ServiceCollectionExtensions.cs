using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Realtime.Presenter.Function.Presentations;

namespace Realtime.Presenter.Function
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddRealTimePresenter(this IServiceCollection services, IConfiguration config)
        {
            return services
                .AddLogging(b => b.SetMinimumLevel(LogLevel.Debug).AddAzureWebAppDiagnostics())
                .AddTransient<PresentationsController>()
                .AddHttpClient()
                .AddSingleton(config);
        }
    }
}