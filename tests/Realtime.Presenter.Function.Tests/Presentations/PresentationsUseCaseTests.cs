using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Realtime.Presenter.Function.Presentations;
using Realtime.Presenter.Function.Tests.Common;
using Realtime.Presenter.Function.Tests.Fakes;
using Xunit;

namespace Realtime.Presenter.Function.Tests.Presentations
{
    public class PresentationsUseCaseTests : UseCaseTests
    {
        private const string HubName = "presenter";
        private readonly PresentationsController _controller;

        public PresentationsUseCaseTests()
        {
            _controller = Get<PresentationsController>();
        }

        [Fact]
        public async Task ShouldTriggerGoToNextSlide()
        {
            HttpClient.SetupPost($"{ConfigurationFactory.SignalREndpoint}/api/v1/hubs/{HubName}", HttpStatusCode.Accepted);
            
            var result = await _controller.GoToNextSlide(new HttpRequestMessage());
            result.Should().BeOfType<OkResult>();
            await HttpClient.Requests.First().Should().BeASignalRRequest(HubName, "nextSlide");
        }

        [Fact]
        public async Task ShouldTriggerGoToPreviousSlide()
        {
            HttpClient.SetupPost($"{ConfigurationFactory.SignalREndpoint}/api/v1/hubs/{HubName}", HttpStatusCode.Accepted);
            
            var result = await _controller.GoToPreviousSlide(new HttpRequestMessage());
            result.Should().BeOfType<OkResult>();
            await HttpClient.Requests.First().Should().BeASignalRRequest(HubName, "previousSlide");
        }

        [Fact]
        public async Task ShouldFailWhenSignalRDoesNotAcceptNextSlide()
        {
            HttpClient.SetupPost($"{ConfigurationFactory.SignalREndpoint}/api/v1/hubs/{HubName}", HttpStatusCode.BadRequest);
            
            var result = await _controller.GoToNextSlide(new HttpRequestMessage());
            result.Should().BeOfType<BadRequestResult>();
        }
        
        [Fact]
        public async Task ShouldFailWhenSignalRDoesNotAcceptPreviousSlide()
        {
            HttpClient.SetupPost($"{ConfigurationFactory.SignalREndpoint}/api/v1/hubs/{HubName}", HttpStatusCode.BadRequest);
            
            var result = await _controller.GoToPreviousSlide(new HttpRequestMessage());
            result.Should().BeOfType<BadRequestResult>();
        }
    }
}