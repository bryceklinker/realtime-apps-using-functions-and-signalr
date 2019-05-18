using System;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Realtime.Presenter.Function.Common.SignalR;

namespace Realtime.Presenter.Function.Presentations
{
    public class PresentationsController
    {
        private readonly ISignalRService _signalRService;

        public PresentationsController(ISignalRService signalRService)
        {
            _signalRService = signalRService;
        }

        [FunctionName("GoToNextSlide")]
        public async Task<IActionResult> GoToNextSlide(
            [HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = "nextSlide")] HttpRequestMessage httpRequestMessage)
        {
            try
            {
                await _signalRService.SendAsync("nextSlide");
                return new OkResult();
            }
            catch (Exception)
            {
                return new BadRequestResult();
            }
        }

        [FunctionName("GoToPreviousSlide")]
        public async Task<IActionResult> GoToPreviousSlide(
            [HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = "previousSlide")] HttpRequestMessage httpRequestMessage)
        {
            try
            {
                await _signalRService.SendAsync("previousSlide");
                return new OkResult();
            }
            catch (Exception)
            {
                return new BadRequestResult();
            }
        }
    }
}