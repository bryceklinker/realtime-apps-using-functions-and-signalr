using System.Net;
using System.Net.Http;
using System.Text;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Newtonsoft.Json;
using Realtime.Presenter.Function.Common.SignalR;
using Realtime.Presenter.Function.V1.Common;

namespace Realtime.Presenter.Function.V1.Credentials
{
    public static class CredentialsController
    {
        [FunctionName("V1GetCredentials")]
        public static HttpResponseMessage GetCredentials(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "v1/credentials")]
            HttpRequestMessage request)
        {
            var credentials = ServiceFactory.GetService<ISignalRService>().GenerateClientCredentials();
            return new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = new StringContent(JsonConvert.SerializeObject(credentials), Encoding.UTF8, "application/json")
            };
        }
    }
}