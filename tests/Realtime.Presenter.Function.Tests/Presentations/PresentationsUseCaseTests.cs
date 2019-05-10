using System.Net.Http;
using System.Threading.Tasks;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Realtime.Presenter.Function.Presentations;
using Realtime.Presenter.Function.Presentations.Dtos;
using Realtime.Presenter.Function.Tests.Fakes;
using Xunit;

namespace Realtime.Presenter.Function.Tests.Presentations
{
    public class PresentationsUseCaseTests : UseCaseTests
    {
        private readonly PresentationsController _controller;

        public PresentationsUseCaseTests()
        {
            _controller = Create<PresentationsController>();
        }
        
        [Fact]
        public async Task GivenAPresentationWhenAddedThenPresentationIsAdded()
        {
            var result = (OkObjectResult) await _controller.AddAsync(new PresentationDto {Title = "This is a title"});
            ((PresentationDto) result.Value).Id.Should().NotBeEmpty();
            ((PresentationDto) result.Value).Title.Should().Be("This is a title");
        }

        [Fact]
        public async Task GivenPresentationsExistWhenIGetAllPresentationsThenIShouldSeeAllPresentations()
        {
            await _controller.AddAsync(new PresentationDto());
            await _controller.AddAsync(new PresentationDto());
            await _controller.AddAsync(new PresentationDto());
            
            var result = (OkObjectResult) await _controller.GetAllAsync(new HttpRequestMessage());
            ((PresentationDto[]) result.Value).Should().HaveCount(3);
        }
    }
}