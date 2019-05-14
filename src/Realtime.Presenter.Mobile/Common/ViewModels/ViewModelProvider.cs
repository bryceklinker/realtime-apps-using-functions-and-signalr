using System;
using Microsoft.Extensions.DependencyInjection;
using Xamarin.Forms;

namespace Realtime.Presenter.Mobile.Common.ViewModels
{
    public interface IViewModelProvider
    {
        T GetViewModel<T>(INavigation navigation);
    }
    
    public class ViewModelProvider : IViewModelProvider
    {
        private readonly IServiceProvider _serviceProvider;

        public ViewModelProvider(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public T GetViewModel<T>(INavigation navigation)
        {
            return ActivatorUtilities.CreateInstance<T>(_serviceProvider, navigation);
        }
    }
}