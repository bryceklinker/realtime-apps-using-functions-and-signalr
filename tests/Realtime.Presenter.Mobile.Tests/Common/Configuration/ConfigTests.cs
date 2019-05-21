using FluentAssertions;
using Realtime.Presenter.Mobile.Common.Configuration;
using Xunit;

namespace Realtime.Presenter.Mobile.Tests.Common.Configuration
{
    public class ConfigTests
    {
        [Fact]
        public void ShouldReturnEndpointFromEmbededAppSettings()
        {
            var endpoint = new Config().Endpoint;
            endpoint.Should().NotBeEmpty();
            endpoint.Should().NotBeNull();
        }
    }
}
