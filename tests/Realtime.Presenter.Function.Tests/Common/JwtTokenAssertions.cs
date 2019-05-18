using System.IdentityModel.Tokens.Jwt;
using System.Net.Http.Headers;
using System.Text;
using FluentAssertions.Primitives;
using Microsoft.IdentityModel.Tokens;

namespace Realtime.Presenter.Function.Tests.Common
{
    public static class JwtTokenAssertionExtensions
    {
        public static JwtTokenAssertions Should(this AuthenticationHeaderValue header)
        {
            return new JwtTokenAssertions(header);
        }

        public static JwtTokenAssertions JwtShould(this string token)
        {
            return new JwtTokenAssertions(token);
        }
    }
    
    public class JwtTokenAssertions : ReferenceTypeAssertions<AuthenticationHeaderValue, JwtTokenAssertions>
    {
        protected override string Identifier { get; } = typeof(AuthenticationHeaderValue).Name;

        private JwtSecurityTokenHandler JwtHandler { get; }

        public JwtTokenAssertions(string token)
            : this(new AuthenticationHeaderValue("Bearer", token))
        {
            
        }
        
        public JwtTokenAssertions(AuthenticationHeaderValue subject)
        {
            Subject = subject;
            JwtHandler = new JwtSecurityTokenHandler();
        }
        
        public void BeValid(string audience, string key)
        {
            var signingKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key));
            var parameters = new TokenValidationParameters
            {
                IssuerSigningKey = signingKey,
                ValidateIssuer = false,
                ValidateLifetime = true,
                ValidateAudience = true,
                ValidAudiences = new []
                {
                    audience
                }
            };
            JwtHandler.ValidateToken(Subject.Parameter, parameters, out _);
        }
    }
}