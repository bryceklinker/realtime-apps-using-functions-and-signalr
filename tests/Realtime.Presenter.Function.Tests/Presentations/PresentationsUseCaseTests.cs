using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.SignalRService;
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
        private readonly FakeAsyncCollector<SignalRMessage> _collector;

        public PresentationsUseCaseTests()
        {
            _collector = new FakeAsyncCollector<SignalRMessage>();
            _controller = Get<PresentationsController>();
        }

        [Fact]
        public async Task ShouldTriggerGoToNextSlide()
        {
            var result = await _controller.GoToNextSlide(new HttpRequestMessage(), _collector);
            
            result.Should().BeOfType<OkResult>();
            _collector.AddedItems.Should().HaveCount(1);
            _collector.AddedItems.Single().Target.Should().Be("nextSlide");
        }

        [Fact]
        public async Task ShouldTriggerGoToPreviousSlide()
        {
            var result = await _controller.GoToPreviousSlide(new HttpRequestMessage(), _collector);
            
            result.Should().BeOfType<OkResult>();
            _collector.AddedItems.Should().HaveCount(1);
            _collector.AddedItems.Single().Target.Should().Be("previousSlide");
        }
    }
}