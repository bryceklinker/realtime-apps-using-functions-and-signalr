using System;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Realtime.Presenter.Function.Credentials.Dtos;

namespace Realtime.Presenter.Function.Common.SignalR
{
    public interface ISignalRService
    {
        Task SendAsync(string target);
        CredentialsDto GenerateClientCredentials();
    }
    
    public class SignalRService : ISignalRService
    {
        private const string HubName = "presenter";
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly ISignalRTokenGenerator _tokenGenerator;
        private readonly ISignalRUrlGenerator _urlGenerator;

        public SignalRService(IHttpClientFactory httpClientFactory, ISignalRTokenGenerator tokenGenerator, ISignalRUrlGenerator urlGenerator)
        {
            _httpClientFactory = httpClientFactory;
            _tokenGenerator = tokenGenerator;
            _urlGenerator = urlGenerator;
        }

        public async Task SendAsync(string target)
        {
            var hubUrl = _urlGenerator.ServerUrl(HubName);
            var token = _tokenGenerator.GenerateToken(hubUrl, DateTime.UtcNow.AddSeconds(10));
            var request = CreateHttpRequest(hubUrl, token, target);
            var client = _httpClientFactory.CreateClient();
            var response = await client.SendAsync(request);
            if (response.StatusCode != HttpStatusCode.Accepted)
                throw new HttpRequestException($"Request to SignalR failed: {HubName} [{response.StatusCode}]");
        }

        public CredentialsDto GenerateClientCredentials()
        {
            var hubUrl = _urlGenerator.ClientUrl(HubName);
            var token = _tokenGenerator.GenerateToken(hubUrl, DateTime.UtcNow.AddHours(4));
            return new CredentialsDto(hubUrl, token);
        }

        private static HttpRequestMessage CreateHttpRequest(string hubUrl, string token, string target)
        {
            var json = JsonConvert.SerializeObject(new {target, arguments = Array.Empty<object>()});
            return new HttpRequestMessage(HttpMethod.Post, hubUrl)
            {
                Content = new StringContent(json, Encoding.UTF8, "application/json"),
                Headers =
                {
                    Authorization = new AuthenticationHeaderValue("Bearer", token)
                }
            };
        }
    }
}