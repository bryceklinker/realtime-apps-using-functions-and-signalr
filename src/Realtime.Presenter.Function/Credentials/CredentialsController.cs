using System.Net.Http;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Azure.WebJobs.Extensions.SignalRService;

namespace Realtime.Presenter.Function.Credentials
{
    public class CredentialsController
    {
        [FunctionName("negotiate")]
        public SignalRConnectionInfo GetCredentials(
            [HttpTrigger(AuthorizationLevel.Anonymous)] HttpRequestMessage httpRequestMessage,
            [SignalRConnectionInfo(HubName = "presenter")] SignalRConnectionInfo connectionInfo)
        {
            return connectionInfo;
        }
    }
}