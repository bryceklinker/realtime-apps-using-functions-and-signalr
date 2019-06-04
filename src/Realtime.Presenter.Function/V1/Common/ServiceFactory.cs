using System;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Realtime.Presenter.Function.V1.Common
{
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