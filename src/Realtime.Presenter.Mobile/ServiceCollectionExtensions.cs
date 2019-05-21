using Microsoft.Extensions.DependencyInjection;
using Realtime.Presenter.Mobile.Common.Configuration;
using Realtime.Presenter.Mobile.Common.ErrorHandling;
using Realtime.Presenter.Mobile.Common.Messaging;
using Realtime.Presenter.Mobile.Common.ViewModels;
using Realtime.Presenter.Mobile.Common.Views;
using Realtime.Presenter.Mobile.Presentations.Services;
using Realtime.Presenter.Mobile.Presentations.ViewModels;
using Realtime.Presenter.Mobile.Presentations.Views;
using Xamarin.Forms;

namespace Realtime.Presenter.Mobile
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddMobileServices(this IServiceCollection services)
        {
            return services.AddHttpClient()
                .AddSingleton<IErrorHandler, ErrorHandler>()
                .AddSingleton<IViewModelProvider, ViewModelProvider>()
                .AddSingleton<IViewProvider, ViewProvider>()
                .AddSingleton<IMessageBus, MessageBus>()
                .AddTransient<IPresentationService, PresentationsService>()
                .AddTransient<IConfig, Config>()
                .AddSingleton(MessagingCenter.Instance)
                .AddTransient<PresentationView>()
                .AddTransient<PresentationViewModel>();
        }
    }
}