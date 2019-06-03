using System.IO;
using System.Threading.Tasks;
using Realtime.Presenter.Function.Files.Storage;

namespace Realtime.Presenter.Function.Files.Readers
{
    public class BinaryBlobReader : IBlobReader
    {
        private readonly IFilesStorageService _filesStorageService;

        public BinaryBlobReader(IFilesStorageService filesStorageService)
        {
            _filesStorageService = filesStorageService;
        }

        public async Task<byte[]> Read(string blobName)
        {
            var blob = _filesStorageService.GetBlob(blobName);
            using (var memoryStream = new MemoryStream())
            {
                await blob.DownloadToStreamAsync(memoryStream);
                return memoryStream.ToArray();
            }
        }
    }
}