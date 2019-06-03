using System.Text;
using System.Threading.Tasks;
using Realtime.Presenter.Function.Files.Storage;

namespace Realtime.Presenter.Function.Files.Readers
{
    public class TextBlobReader : IBlobReader
    {
        private readonly IFilesStorageService _filesStorageService;

        public TextBlobReader(IFilesStorageService filesStorageService)
        {
            _filesStorageService = filesStorageService;
        }
        
        public async Task<byte[]> Read(string blobName)
        {
            var blob = _filesStorageService.GetBlob(blobName);
            if (!await blob.ExistsAsync())
                return null;
            
            var text = await blob.DownloadTextAsync();
            return Encoding.UTF8.GetBytes(text);
        }
    }
}