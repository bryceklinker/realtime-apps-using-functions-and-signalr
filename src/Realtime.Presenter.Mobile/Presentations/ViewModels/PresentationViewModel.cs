using System.Windows.Input;
using Realtime.Presenter.Mobile.Common.ErrorHandling;
using Realtime.Presenter.Mobile.Common.Messaging;
using Realtime.Presenter.Mobile.Common.ViewModels;
using Realtime.Presenter.Mobile.Presentations.Services;
using Xamarin.Forms;

namespace Realtime.Presenter.Mobile.Presentations.ViewModels
{
    public class PresentationViewModel : ViewModelBase
    {
        private readonly IPresentationService _presentationService;
        private readonly IErrorHandler _errorHandler;

        public ICommand NextCommand { get; }
        public ICommand PreviousCommand { get; }
        public int ErrorCount => _errorHandler.ErrorCount;

        public PresentationViewModel(INavigation navigation, IPresentationService presentationService, IErrorHandler errorHandler, IMessageBus messageBus)
        {
            _presentationService = presentationService;
            _errorHandler = errorHandler;

            NextCommand = new Command(ExecuteNext);
            PreviousCommand = new Command(ExecutePrevious);
            messageBus.Subscribe<int>(OnErrorOccurred);
        }

        private void ExecuteNext()
        {
            _presentationService.GoToNextSlide();
        }

        private void ExecutePrevious()
        {
            _presentationService.GoToPreviousSlide();
        }

        private void OnErrorOccurred(int count)
        {
            RaisePropertyChanged(() => ErrorCount);
        }
    }
}