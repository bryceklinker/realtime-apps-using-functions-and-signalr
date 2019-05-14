using System.Net.Http;

namespace Realtime.Presenter.Test.Utilities.Fakes
{
    public class FakedRequest
    {
        private readonly HttpRequestMessage _request;

        public HttpResponseMessage Response { get; }

        public FakedRequest(HttpRequestMessage request, HttpResponseMessage response)
        {
            _request = request;
            Response = response;
        }

        public bool DoesMatch(HttpRequestMessage request)
        {
            return request.Method == _request.Method
                   && request.RequestUri == _request.RequestUri;
        }
    }
}