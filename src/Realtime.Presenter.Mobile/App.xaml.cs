using System;
using Microsoft.Extensions.DependencyInjection;
using Realtime.Presenter.Mobile.Common.DependencyInjection;
using Realtime.Presenter.Mobile.Common.Views;
using Realtime.Presenter.Mobile.Presentations.Views;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Realtime.Presenter.Mobile
{
    public partial class App : Application
    {
        private IViewProvider Provider { get; }

        public App(Action<IServiceCollection> configureServices = null)
        {
            InitializeComponent();

            Provider = ServiceProviderFactory.CreateProvider(configureServices)
                .GetRequiredService<IViewProvider>();
            
            MainPage = Provider.GetView<PresentationView>();
        }

        protected override void OnStart()
        {
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}
