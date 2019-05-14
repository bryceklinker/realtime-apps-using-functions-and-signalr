using System;
using Microsoft.Extensions.DependencyInjection;

namespace Realtime.Presenter.Mobile.Common.DependencyInjection
{
    public static class ServiceProviderFactory
    {
        public static IServiceProvider CreateProvider(Action<IServiceCollection> configureServices)
        {
            var services = new ServiceCollection()
                .AddMobileServices();

            configureServices?.Invoke(services);

            return services.BuildServiceProvider();
        }
    }
}