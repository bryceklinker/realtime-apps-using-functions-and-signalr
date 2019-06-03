using System;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Realtime.Presenter.Function.Files.Readers;

namespace Realtime.Presenter.Function.Files
{
    public class FilesController
    {
        private readonly IBlobReaderFactory _blobReaderFactory;

        public FilesController(IBlobReaderFactory blobReaderFactory)
        {
            _blobReaderFactory = blobReaderFactory;
        }

        [FunctionName("GetFile")]
        public async Task<IActionResult> GetFile(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "files")] HttpRequestMessage request)
        {
            var blobName = GetBlobName(request.RequestUri);
            var contentType = GetContentType(blobName);
            var reader = _blobReaderFactory.GetReader(contentType);
            var bytes = await reader.Read(blobName);
            return new FileContentResult(bytes, contentType);
        }
        
        private static string GetBlobName(Uri uri)
        {
            return uri.ParseQueryString()["file"];
        }

        private static string GetContentType(string blobName)
        {
            if (blobName.EndsWith(".html"))
                return "text/html";

            if (blobName.EndsWith(".js")) 
                return "text/javascript";

            if (blobName.EndsWith(".png"))
                return "image/png";
            
            return "idk";
        }
    }
}