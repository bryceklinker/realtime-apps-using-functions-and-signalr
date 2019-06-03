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
            if (contentType.StartsWith("text"))
            {
                return _serviceProvider.GetRequiredService<TextBlobReader>();
            }

            return _serviceProvider.GetRequiredService<BinaryBlobReader>();
        }
    }
}