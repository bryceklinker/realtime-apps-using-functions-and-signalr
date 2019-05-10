using System.Threading.Tasks;
using FluentAssertions;
using Newtonsoft.Json;
using Realtime.Presenter.Function.Presentations.Dtos;
using Realtime.Presenter.Function.Presentations.Services;
using Realtime.Presenter.Function.Tests.Fakes.Common.Azure;
using Xunit;

namespace Realtime.Presenter.Function.Tests.Presentations.Services
{
    public class PresentationServiceTests
    {
        private readonly FakeCloudStorageAccount _storageAccount;
        private readonly PresentationsService _service;

        public PresentationServiceTests()
        {
            var factory = new FakeStorageAccountFactory();
            _storageAccount = factory.Account;
            
            _service = new PresentationsService(factory);
        }

        [Fact]
        public async Task GivenPresentationDtoWhenAddThenPresentationsContainerShouldBeCreated()
        {
            await _service.AddAsync(new PresentationDto());
            _storageAccount.DoesContainerExist("presentations").Should().BeTrue();
        }

        [Fact]
        public async Task GivenPresentationDtoWhenAddThenIdShouldBeGenerated()
        {
            var dto = await _service.AddAsync(new PresentationDto());
            dto.Id.Should().NotBeEmpty();
        }
        
        [Fact]
        public async Task GivenPresentationDtoWhenAddThenHasPresentations()
        {
            var dto = await _service.AddAsync(new PresentationDto {Title = "This is my title"});
            var presentations = ReadPresentationsBlob();
            presentations.Should().HaveCount(1);
            presentations[0].Id.Should().Be(dto.Id);
            presentations[0].Title.Should().Be(dto.Title);
        }

        [Fact]
        public async Task GivenPresentationsAlreadyExistWhenAddThenPresentationAppended()
        {
            await _service.AddAsync(new PresentationDto {Title = "IDK"});
            await _service.AddAsync(new PresentationDto {Title = "Second"});
            ReadPresentationsBlob().Should().HaveCount(2);
        }

        [Fact]
        public async Task GivenPresentationsExistWhenGetAllThenReturnsAllPresentations()
        {
            await _service.AddAsync(new PresentationDto {Title = "Bob"});
            await _service.AddAsync(new PresentationDto {Title = "New"});
            await _service.AddAsync(new PresentationDto {Title = "Hotness"});

            var dtos = await _service.GetAllAsync();
            dtos.Should().HaveCount(3);
        }

        private PresentationDto[] ReadPresentationsBlob()
        {
            var blob = _storageAccount.GetBlob("presentations", "presentations.json");
            return JsonConvert.DeserializeObject<PresentationDto[]>(blob.Contents);
        }
    }
}