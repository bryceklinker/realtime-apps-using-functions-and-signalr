using System;
using System.Net.Http;

namespace Realtime.Presenter.Function.Files.Services
{
    public interface IBlobNameResolver
    {
        string GetBlobName(Uri uri);
    }
    
    public class BlobNameResolver : IBlobNameResolver
    {
        public string GetBlobName(Uri uri)
        {
            var file = uri.ParseQueryString()["file"];
            return string.IsNullOrWhiteSpace(file)
                ? "index.html"
                : file;
        }
    }
}