using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Realtime.Presenter.Function.Tests.Fakes
{
    public class FakeHttpMessageHandler : HttpMessageHandler
    {
        private readonly List<FakedRequest> _fakedRequests;
        private readonly List<HttpRequestMessage> _requests;

        public IEnumerable<HttpRequestMessage> Requests => _requests;

        public FakeHttpMessageHandler()
        {
            _fakedRequests = new List<FakedRequest>();
            _requests = new List<HttpRequestMessage>();
        }
        
        protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            _requests.Add(request);
            var matchingRequest = _fakedRequests.FirstOrDefault(r => r.DoesMatch(request));
            if (matchingRequest == null)
                throw new InvalidOperationException($"No request setup with [{request.Method}] {request.RequestUri}.");

            return Task.FromResult(matchingRequest.Response);
        }

        public void SetupGet<T>(string url, T data)
        {
            var request = new HttpRequestMessage(HttpMethod.Get, url);
            var response = new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = new StringContent(Serialize(data), Encoding.UTF8, "application/json")
            };
            _fakedRequests.Add(new FakedRequest(request, response));
        }

        public void SetupPost(string url, HttpStatusCode statusCode)
        {
            var request = new HttpRequestMessage(HttpMethod.Post, url);
            var response = new HttpResponseMessage(statusCode);
            _fakedRequests.Add(new FakedRequest(request, response));
        }

        private string Serialize<T>(T data)
        {
            return JsonConvert.SerializeObject(data);
        }
    }
}