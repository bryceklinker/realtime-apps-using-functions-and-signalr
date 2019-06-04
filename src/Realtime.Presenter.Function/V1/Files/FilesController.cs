using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Realtime.Presenter.Function.Files.Services;
using Realtime.Presenter.Function.V1.Common;

namespace Realtime.Presenter.Function.V1.Files
{
    public static class FilesController
    {
        [FunctionName("V1GetFile")]
        public static async Task<HttpResponseMessage> GetFile(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "v1/files")]
            HttpRequestMessage request)
        {
            var filesService = ServiceFactory.GetService<IFilesService>();
            var contentType = filesService.GetContentTypeForUri(request.RequestUri);
            var bytes = await filesService.GetFileForUri(request.RequestUri);
            if (bytes == null)
                return new HttpResponseMessage(HttpStatusCode.NotFound);

            return new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = new ByteArrayContent(bytes)
                {
                    Headers   =
                    {
                        ContentType = new MediaTypeHeaderValue(contentType)
                    }
                }
            };
        }
    }
}