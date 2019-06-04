using System;
using System.Threading.Tasks;
using Realtime.Presenter.Function.Files.Readers;

namespace Realtime.Presenter.Function.Files.Services
{
    public interface IFilesService
    {
        Task<byte[]> GetFileForUri(Uri uri);
        string GetContentTypeForUri(Uri uri);
    }
    
    public class FilesService : IFilesService
    {
        private readonly IBlobReaderFactory _blobReaderFactory;
        private readonly IBlobNameResolver _blobNameResolver;
        private readonly IContentTypeResolver _contentTypeResolver;

        public FilesService(
            IBlobReaderFactory blobReaderFactory, 
            IBlobNameResolver blobNameResolver, 
            IContentTypeResolver contentTypeResolver)
        {
            _blobReaderFactory = blobReaderFactory;
            _blobNameResolver = blobNameResolver;
            _contentTypeResolver = contentTypeResolver;
        }

        public async Task<byte[]> GetFileForUri(Uri uri)
        {
            var blobName = _blobNameResolver.GetBlobName(uri);
            var contentType = _contentTypeResolver.GetContentType(blobName);
            var reader = _blobReaderFactory.GetReader(contentType);
            return await reader.Read(blobName);
        }

        public string GetContentTypeForUri(Uri uri)
        {
            var blobName = _blobNameResolver.GetBlobName(uri);
            return _contentTypeResolver.GetContentType(blobName);
        }
    }
}