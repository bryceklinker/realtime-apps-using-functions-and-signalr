using System;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Realtime.Presenter.Function.V1.Common
{
    /// <summary>
    /// This might be a good way to achieve DI similar to ASP.NET Core and updated azure functions.
    /// If you are stuck using V1 of Azure Functions. The reason the services are exposed is for testing purposes.
    /// </summary>
    public static class ServiceFactory
    {
        private static readonly Lazy<IServiceProvider> LazyProvider = new Lazy<IServiceProvider>(CreateServiceProvider);

        public static IServiceCollection Services { get; set; } = new ServiceCollection()
            .AddRealTimePresenter(
                new ConfigurationBuilder()
                    .AddEnvironmentVariables()
                    .Build()
            );

        public static T GetService<T>()
        {
            return LazyProvider.Value.GetRequiredService<T>();
        }

        private static IServiceProvider CreateServiceProvider()
        {
            return Services.BuildServiceProvider();
        }
    }
}