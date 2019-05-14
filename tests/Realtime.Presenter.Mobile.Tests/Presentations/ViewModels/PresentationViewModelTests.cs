using System.Net;
using System.Threading.Tasks;
using FluentAssertions;
using Realtime.Presenter.Mobile.Common.ErrorHandling;
using Realtime.Presenter.Mobile.Common.Messaging;
using Realtime.Presenter.Mobile.Presentations.Services;
using Realtime.Presenter.Mobile.Presentations.ViewModels;
using Realtime.Presenter.Mobile.Tests.Fakes.Common.Configuration;
using Realtime.Presenter.Test.Utilities;
using Realtime.Presenter.Test.Utilities.Fakes;
using Xamarin.Forms;
using Xunit;

namespace Realtime.Presenter.Mobile.Tests.Presentations.ViewModels
{
    public class PresentationViewModelTests
    {
        private readonly FakeHttpClient _httpClient;
        private readonly ErrorHandler _errorHandler;
        private readonly PresentationViewModel _viewModel;

        public PresentationViewModelTests()
        {
            var factory = new FakeHttpClientFactory();
            _httpClient = factory.Client;

            var messageBus = new MessageBus(new MessagingCenter());
            _errorHandler = new ErrorHandler(messageBus);
            var service = new PresentationsService(new FakeConfig(), factory, _errorHandler);
            _viewModel = new PresentationViewModel(service, _errorHandler, messageBus);
        }

        [Fact]
        public async Task ShouldGoToTheNextSlide()
        {
            _httpClient.SetupPost($"{FakeConfig.EndpointSetting}/api/nextSlide", HttpStatusCode.OK);
            
            _viewModel.NextCommand.Execute(null);
            await _httpClient.WhenAllRequestsFinish();
            
            _httpClient.Requests.Should().HaveCount(1);
        }

        [Fact]
        public async Task ShouldGoToThePreviousSlide()
        {
            _httpClient.SetupPost($"{FakeConfig.EndpointSetting}/api/previousSlide", HttpStatusCode.OK);
            
            _viewModel.PreviousCommand.Execute(null);
            await _httpClient.WhenAllRequestsFinish();
            
            _httpClient.Requests.Should().HaveCount(1);
        }

        [Fact]
        public async Task ShouldNotifyThatAnErrorOccurredWhenGoingToTheNextSlide()
        {
            _viewModel.NextCommand.Execute(null);
            await _httpClient.WhenAllRequestsFinishIgnoreExceptions();

            _viewModel.ErrorCount.Should().Be(1);
            _errorHandler.ErrorCount.Should().Be(1);
        }
        
        [Fact]
        public async Task ShouldNotifyThatAnErrorOccurredWhenGoingToThePreviousSlide()
        {
            _viewModel.PreviousCommand.Execute(null);
            await _httpClient.WhenAllRequestsFinishIgnoreExceptions();
            
            _viewModel.ErrorCount.Should().Be(1);
            _errorHandler.ErrorCount.Should().Be(1);
        }

        [Fact]
        public async Task ShouldNotifyThatErrorCountUpdated()
        {
            var changeCount = 0;
            _viewModel.PropertyChanged += (_, args) => changeCount += args.PropertyName == "ErrorCount" ? 1 : 0;
            
            _viewModel.NextCommand.Execute(null);
            await _httpClient.WhenAllRequestsFinishIgnoreExceptions();

            await Eventually.Should(() =>
            {
                changeCount.Should().Be(1);
            });
        }
    }
}