using System;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace Realtime.Presenter.Function.Common.SignalR
{
    public interface ISignalRTokenGenerator
    {
        string GenerateToken(string hubUrl, DateTime expirationDate);
    }

    public class SignalRTokenGenerator : ISignalRTokenGenerator
    {
        private readonly IConfiguration _config;

        private string Key => _config["SignalR:Key"];
        private string Endpoint => _config["SignalR:Endpoint"];

        public SignalRTokenGenerator(IConfiguration config)
        {
            _config = config;
        }

        public string GenerateToken(string hubUrl, DateTime expirationDate)
        {
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Key));
            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var securityHandler = new JwtSecurityTokenHandler();
            var securityToken = securityHandler.CreateJwtSecurityToken(
                issuer: null,
                audience: hubUrl,
                subject: null,
                expires: expirationDate,
                signingCredentials: credentials
            );
            return securityHandler.WriteToken(securityToken);
        }
    }
}