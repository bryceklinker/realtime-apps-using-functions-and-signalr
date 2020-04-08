using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Realtime.Presenter.Function.Common.SignalR;
using Realtime.Presenter.Function.Common.Storage;
using Realtime.Presenter.Function.Credentials;
using Realtime.Presenter.Function.Files;
using Realtime.Presenter.Function.Files.Readers;
using Realtime.Presenter.Function.Files.Services;
using Realtime.Presenter.Function.Files.Storage;
using Realtime.Presenter.Function.Presentations;

namespace Realtime.Presenter.Function
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddRealTimePresenter(this IServiceCollection services, IConfiguration config)
        {
            return services
                .AddLogging(b => b
                    .SetMinimumLevel(LogLevel.Debug)
                    .AddAzureWebAppDiagnostics()
                    .AddFilter(l => true)
                )
                .AddTransient<PresentationsController>()
                .AddTransient<CredentialsController>()
                .AddTransient<FilesController>()
                .AddTransient<TextBlobReader>()
                .AddTransient<BinaryBlobReader>()
                .AddTransient<IStorageAccountFactory, StorageAccountFactory>()
                .AddTransient<IBlobReaderFactory, BlobReaderFactory>()
                .AddTransient<IFilesStorageService, FilesStorageService>()
                .AddTransient<IFilesService, FilesService>()
                .AddTransient<IContentTypeResolver, ContentTypeResolver>()
                .AddTransient<IBlobNameResolver, BlobNameResolver>()
                .AddHttpClient()
                .AddSingleton(config);
        }
    }
}