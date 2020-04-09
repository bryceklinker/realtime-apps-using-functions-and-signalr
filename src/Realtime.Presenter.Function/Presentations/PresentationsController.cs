using System;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Azure.WebJobs.Extensions.SignalRService;

namespace Realtime.Presenter.Function.Presentations
{
    public class PresentationsController
    {
        [FunctionName("GoToNextSlide")]
        public async Task<IActionResult> GoToNextSlide(
            [HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = "nextSlide")] HttpRequestMessage httpRequestMessage,
            [SignalR(HubName = "presenter")] IAsyncCollector<SignalRMessage> signalR)
        {
            await signalR.AddAsync(new SignalRMessage
            {
                Target = "nextSlide",
                Arguments = Array.Empty<object>()
            }, CancellationToken.None);
            return new OkResult();
        }

        [FunctionName("GoToPreviousSlide")]
        public async Task<IActionResult> GoToPreviousSlide(
            [HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = "previousSlide")] HttpRequestMessage httpRequestMessage,
            [SignalR(HubName = "presenter")] IAsyncCollector<SignalRMessage> signalR)
        {
            await signalR.AddAsync(new SignalRMessage
            {
                Target = "previousSlide",
                Arguments = Array.Empty<object>()
            }, CancellationToken.None);
            return new OkResult();
        }
    }
}