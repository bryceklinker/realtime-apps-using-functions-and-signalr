using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Realtime.Presenter.Function.Files.Services;

namespace Realtime.Presenter.Function.Files
{
    public class FilesController
    {
        private readonly IFilesService _filesService;

        public FilesController(IFilesService filesService)
        {
            _filesService = filesService;
        }

        [FunctionName("GetFile")]
        public async Task<IActionResult> GetFile(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "files")] HttpRequestMessage request)
        {
            var contentType = _filesService.GetContentTypeForUri(request.RequestUri);
            var bytes = await _filesService.GetFileForUri(request.RequestUri);
            if (bytes == null)
                return new NotFoundResult();
            
            return new FileContentResult(bytes, contentType);
        }
    }
}