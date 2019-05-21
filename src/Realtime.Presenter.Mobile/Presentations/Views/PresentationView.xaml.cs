using Realtime.Presenter.Mobile.Common.ViewModels;
using Realtime.Presenter.Mobile.Presentations.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Realtime.Presenter.Mobile.Presentations.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PresentationView : ContentPage
    {
        public PresentationView(IViewModelProvider viewModelProvider)
        {
            InitializeComponent();
            BindingContext = viewModelProvider.GetViewModel<PresentationViewModel>(Navigation);
        }
    }
}