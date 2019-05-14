using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json.Linq;
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
            await AssertSignalRMessageSent();
        }

        private async Task AssertSignalRMessageSent()
        {
            HttpClient.Requests.Should().HaveCount(1);
            var request = HttpClient.Requests.Single();
            var jObject = await request.Content.ReadAsAsync<JObject>();
            jObject.Value<string>("target").Should().Be("next-slide");
            jObject.Value<JArray>("arguments").Should().HaveCount(0);

            AssertValidJwt(request);
        }

        private static void AssertValidJwt(HttpRequestMessage request)
        {
            var token = request.Headers.Authorization.Parameter;
            var tokenHandler = new JwtSecurityTokenHandler();
            tokenHandler.ValidateToken(token, new TokenValidationParameters
            {
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(ConfigurationFactory.SignalRKey)),
                ValidateIssuer = false,
                ValidateLifetime = true,
                ValidateAudience = true,
                ValidAudiences = new[]
                {
                    $"{ConfigurationFactory.SignalREndpoint}/api/v1/hubs/presentation"
                }
            }, out var _);
        }
    }
}