using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Realtime.Presenter.Function.Common.SignalR;
using Realtime.Presenter.Function.V1.Common;

namespace Realtime.Presenter.Function.V1.Presentations
{
    public static class PresentationsController
    {
        [FunctionName("V1GoToNextSlide")]
        public static async Task<HttpResponseMessage> OldGoToNextSlide(
            [HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = "v1/nextSlide")]
            HttpRequestMessage request)
        {
            try
            {
                await ServiceFactory.GetService<ISignalRService>().SendAsync("nextSlide");
                return new HttpResponseMessage();
            }
            catch (Exception)
            {
                return new HttpResponseMessage(HttpStatusCode.BadRequest);
            }
        }

        [FunctionName("V1GoToPreviousSlide")]
        public static async Task<HttpResponseMessage> OldGoToPreviousSlide(
            [HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = "v1/previousSlide")]
            HttpRequestMessage request)
        {
            try
            {
                await ServiceFactory.GetService<ISignalRService>().SendAsync("previousSlide");
                return new HttpResponseMessage();
            }
            catch (Exception)
            {
                return new HttpResponseMessage(HttpStatusCode.BadRequest);
            }
        }
    }
}