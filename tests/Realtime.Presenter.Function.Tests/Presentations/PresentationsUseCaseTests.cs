using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Realtime.Presenter.Function.Presentations;
using Realtime.Presenter.Function.Tests.Common;
using Realtime.Presenter.Function.Tests.Fakes;
using Xunit;

namespace Realtime.Presenter.Function.Tests.Presentations
{
    public class PresentationsUseCaseTests : UseCaseTests
    {
        private readonly PresentationsController _controller;

        public PresentationsUseCaseTests()
        {
            _controller = Get<PresentationsController>();
        }

        [Fact]
        public async Task ShouldTriggerGoToNextSlide()
        {
            HttpClient.SetupPost($"{ConfigurationFactory.SignalREndpoint}/api/v1/hubs/presentation", HttpStatusCode.Accepted);
            
            var response = await _controller.GoToNextSlide(new HttpRequestMessage());
            response.Should().BeOfType<OkResult>();
            await HttpClient.Requests.First().Should().BeASignalRRequest("presentation", "nextSlide");
        }

        [Fact]
        public async Task ShouldTriggerGoToPreviousSlide()
        {
            HttpClient.SetupPost($"{ConfigurationFactory.SignalREndpoint}/api/v1/hubs/presentation", HttpStatusCode.Accepted);
            
            var response = await _controller.GoToPreviousSlide(new HttpRequestMessage());
            response.Should().BeOfType<OkResult>();
            await HttpClient.Requests.First().Should().BeASignalRRequest("presentation", "previousSlide");
        }
    }
}