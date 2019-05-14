using System;
using System.IdentityModel.Tokens.Jwt;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;

namespace Realtime.Presenter.Function.Presentations
{
    public class PresentationsController
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IConfiguration _config;

        public PresentationsController(IHttpClientFactory httpClientFactory, IConfiguration config)
        {
            _httpClientFactory = httpClientFactory;
            _config = config;
        }

        public async Task<IActionResult> GoToNextSlide(HttpRequestMessage httpRequestMessage)
        {
            var client = _httpClientFactory.CreateClient();
            var request = CreateSignalRRequest();
            await client.SendAsync(request);
            return new OkResult();
        }

        private HttpRequestMessage CreateSignalRRequest()
        {
            var hubUrl = $"{_config["SignalR:Endpoint"]}/api/v1/hubs/presentation";
            var token = GenerateToken(hubUrl);
            var json = JsonConvert.SerializeObject(new {target = "next-slide", arguments = Array.Empty<object>()});

            return new HttpRequestMessage(HttpMethod.Post, hubUrl)
            {
                Content = new StringContent(json, Encoding.UTF8, "application/json"),
                Headers =
                {
                    Authorization = new AuthenticationHeaderValue("Bearer", token)
                }
            };
        }

        private string GenerateToken(string hubUrl)
        {
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["SignalR:Key"]));
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
    }
}