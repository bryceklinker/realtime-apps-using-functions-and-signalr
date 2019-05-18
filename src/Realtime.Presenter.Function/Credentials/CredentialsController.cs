using System.Net.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
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

        [FunctionName("GetCredentials")]
        public IActionResult GetCredentials(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "credentials")] HttpRequestMessage httpRequestMessage)
        {
            var credentials = _signalRService.GenerateClientCredentials();
            return new OkObjectResult(credentials);
        }
    }
}