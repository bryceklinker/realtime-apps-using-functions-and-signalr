namespace Realtime.Presenter.Function.Files.Services
{
    public interface IContentTypeResolver
    {
        string GetContentType(string blobName);
    }
    
    public class ContentTypeResolver : IContentTypeResolver
    {
        public string GetContentType(string blobName)
        {
            if (blobName.EndsWith(".html"))
                return "text/html";

            if (blobName.EndsWith(".js")) 
                return "text/javascript";

            if (blobName.EndsWith(".png"))
                return "image/png";

            if (blobName.EndsWith(".svg"))
                return "image/svg+xml";

            if (blobName.EndsWith(".jpeg"))
                return "image/jpeg";
            
            if (blobName.EndsWith(".gif"))
                return "image/gif";

            return "idk";
        }
    }
}