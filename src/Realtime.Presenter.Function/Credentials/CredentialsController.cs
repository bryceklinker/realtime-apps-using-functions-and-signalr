using System.Net.Http;
using Microsoft.AspNetCore.Mvc;
using Realtime.Presenter.Function.Common.SignalR;

namespace Realtime.Presenter.Function.Credentials
{
    public class CredentialsController
    {
        private readonly ISignalRService _signalRService;

        public CredentialsController(ISignalRService signalRService)
        {
            _signalRService = signalRService;
        }

        public IActionResult GetCredentials(HttpRequestMessage httpRequestMessage)
        {
            var credentials = _signalRService.GenerateClientCredentials();
            return new OkObjectResult(credentials);
        }
    }
}