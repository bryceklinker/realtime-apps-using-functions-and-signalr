using System;
using Microsoft.Extensions.DependencyInjection;

namespace Realtime.Presenter.Function.Files.Readers
{
    public interface IBlobReaderFactory
    {
        IBlobReader GetReader(string contentType);
    }
    
    public class BlobReaderFactory : IBlobReaderFactory
    {
        private readonly IServiceProvider _serviceProvider;

        public BlobReaderFactory(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public IBlobReader GetReader(string contentType)
        {
            return contentType.StartsWith("text")
                ? (IBlobReader) _serviceProvider.GetRequiredService<TextBlobReader>()
                : _serviceProvider.GetRequiredService<BinaryBlobReader>();
        }
    }
}