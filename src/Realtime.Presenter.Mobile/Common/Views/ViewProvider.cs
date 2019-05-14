using System;
using Microsoft.Extensions.DependencyInjection;

namespace Realtime.Presenter.Mobile.Common.Views
{
    public interface IViewProvider
    {
        T GetView<T>();
    }
    
    public class ViewProvider : IViewProvider
    {
        private readonly IServiceProvider _serviceProvider;

        public ViewProvider(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public T GetView<T>()
        {
            return ActivatorUtilities.CreateInstance<T>(_serviceProvider);
        }
    }
}