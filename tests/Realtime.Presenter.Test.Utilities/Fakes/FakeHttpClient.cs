using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

namespace Realtime.Presenter.Test.Utilities.Fakes
{
    public class FakeHttpClient : HttpClient
    {
        private readonly FakeHttpMessageHandler _handler;

        public IEnumerable<HttpRequestMessage> Requests => _handler.Requests;
        
        public FakeHttpClient()
            : this(new FakeHttpMessageHandler())
        {
            
        }

        private FakeHttpClient(FakeHttpMessageHandler handler)
            : base(handler)
        {
            _handler = handler;
        }


        public void SetupGet<T>(string url, T data)
        {
            _handler.SetupGet(url, data);
        }

        public void SetupPost(string url, HttpStatusCode statusCode)
        {
            _handler.SetupPost(url, statusCode);
        }

        public Task WhenAllRequestsFinish()
        {
            return _handler.WhenAllRequestsFinish();
        }

        public async Task WhenAllRequestsFinishIgnoreExceptions()
        {
            try
            {
                await WhenAllRequestsFinish();
            }
            catch (Exception)
            {
            }
        }
    }
}