using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Realtime.Presenter.Function.Common.Azure;
using Realtime.Presenter.Function.Presentations;
using Realtime.Presenter.Function.Presentations.Services;

namespace Realtime.Presenter.Function
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddRealTimePresenter(this IServiceCollection services, IConfiguration config)
        {
            return services
                .AddLogging(b => b.SetMinimumLevel(LogLevel.Debug).AddAzureWebAppDiagnostics())
                .AddTransient<IStorageAccountFactory, StorageAccountFactory>()
                .AddTransient<IPresentationsService, PresentationsService>()
                .AddTransient<PresentationsController>()
                .AddSingleton(config);
        }
    }
}