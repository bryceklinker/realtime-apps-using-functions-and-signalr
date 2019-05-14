using System.Net.Http;

namespace Realtime.Presenter.Function.Tests.Fakes
{
    public class FakeHttpClientFactory : IHttpClientFactory
    {
        public FakeHttpClient Client { get; } = new FakeHttpClient();
        
        public HttpClient CreateClient(string name)
        {
            return Client;
        }

        public void SetupGet<T>(string url, T data)
        {
            Client.SetupGet(url, data);
        }
    }
}