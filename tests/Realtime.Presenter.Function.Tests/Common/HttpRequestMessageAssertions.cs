using System.Net.Http;
using System.Threading.Tasks;
using FluentAssertions;
using FluentAssertions.Primitives;
using Newtonsoft.Json.Linq;
using Realtime.Presenter.Function.Tests.Fakes;

namespace Realtime.Presenter.Function.Tests.Common
{
    public static class HttpRequestMessageAssertionExtensions
    {
        public static HttpRequestMessageAssertions Should(this HttpRequestMessage request)
        {
            return new HttpRequestMessageAssertions(request);
        }
    }
    
    public class HttpRequestMessageAssertions : ReferenceTypeAssertions<HttpRequestMessage, HttpRequestMessageAssertions>
    {
        protected override string Identifier { get; } = typeof(HttpRequestMessage).Name;

        public HttpRequestMessageAssertions(HttpRequestMessage subject)
        {
            Subject = subject;
        }

        public async Task BeASignalRRequest(string hubName, string target)
        {
            var jObject = await Subject.Content.ReadAsAsync<JObject>();
            jObject.Value<string>("target").Should().Be(target);
            jObject.Value<JArray>("arguments").Should().HaveCount(0);

            var hubUrl = $"{ConfigurationFactory.SignalREndpoint}/api/v1/hubs/{hubName}";
            var key = ConfigurationFactory.SignalRKey;
            Subject.Headers.Authorization.Should().BeValid(hubUrl, key);
        }
    }
}