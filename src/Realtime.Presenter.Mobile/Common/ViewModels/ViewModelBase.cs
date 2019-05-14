using System;
using System.Linq.Expressions;
using Xamarin.Forms;

namespace Realtime.Presenter.Mobile.Common.ViewModels
{
    public class ViewModelBase : BindableObject
    {
        protected void RaisePropertyChanged<T>(Expression<Func<T>> property)
        {
            OnPropertyChanged(property.GetPropertyName());
        }
    }
}