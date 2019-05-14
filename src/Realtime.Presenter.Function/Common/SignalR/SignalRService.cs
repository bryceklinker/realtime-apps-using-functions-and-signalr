using System;
using System.IdentityModel.Tokens.Jwt;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;

namespace Realtime.Presenter.Function.Common.SignalR
{
    public interface ISignalRService
    {
        Task SendAsync(string hubName, string target);
    }
    
    public class SignalRService : ISignalRService
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IConfiguration _config;

        private string Key => _config["SignalR:Key"];
        private string Endpoint => _config["SignalR:Endpoint"];
        
        public SignalRService(IConfiguration config, IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
            _config = config;
        }

        public async Task SendAsync(string hubName, string target)
        {
            var hubUrl = GetHubUrl(hubName);
            var token = GenerateJwtToken(hubUrl);
            var request = CreateHttpRequest(hubUrl, token, target);
            var client = _httpClientFactory.CreateClient();
            var response = await client.SendAsync(request);
            if (response.StatusCode != HttpStatusCode.Accepted)
                throw new HttpRequestException($"Request to SignalR failed: {hubName} [{response.StatusCode}]");
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
        
        private string GenerateJwtToken(string hubUrl)
        {
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Key));
            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var securityHandler = new JwtSecurityTokenHandler();
            var securityToken = securityHandler.CreateJwtSecurityToken(
                issuer: null,
                audience: hubUrl,
                subject: null,
                expires: DateTime.UtcNow.AddSeconds(10),
                signingCredentials: credentials
            );
            return securityHandler.WriteToken(securityToken);
        }

        private string GetHubUrl(string hubName)
        {
            return $"{Endpoint}/api/v1/hubs/{hubName}";
        }
    }
}