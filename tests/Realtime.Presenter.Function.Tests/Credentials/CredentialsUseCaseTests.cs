using System.Net.Http;
using FluentAssertions;
using Realtime.Presenter.Function.Credentials;
using Realtime.Presenter.Function.Credentials.Dtos;
using Realtime.Presenter.Function.Tests.Common;
using Realtime.Presenter.Function.Tests.Fakes;
using Xunit;

namespace Realtime.Presenter.Function.Tests.Credentials
{
    public class CredentialsUseCaseTests : UseCaseTests
    {
        private static readonly string ClientPresentationHubUrl = $"{ConfigurationFactory.SignalREndpoint}/client/?hub=presenter";
        private const string SignalRKey = ConfigurationFactory.SignalRKey;
        private readonly CredentialsController _controller;

        public CredentialsUseCaseTests()
        {
            _controller = Get<CredentialsController>();
        }
        
        [Fact]
        public void ShouldReturnCredentialsWithUrlAndTokenForSignalRHub()
        {
            var result = _controller.GetCredentials(new HttpRequestMessage());
            result.Should().BeOk()
                .And.HaveContent<CredentialsDto>(content =>
                {
                    content.SignalRUrl.Should().Be(ClientPresentationHubUrl);
                    content.SignalRToken.JwtShould().BeValid(ClientPresentationHubUrl, SignalRKey);
                });
        }
    }
}