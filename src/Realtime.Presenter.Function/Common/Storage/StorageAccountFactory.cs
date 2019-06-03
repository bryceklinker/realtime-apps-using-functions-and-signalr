using Microsoft.Extensions.Configuration;
using Microsoft.WindowsAzure.Storage;

namespace Realtime.Presenter.Function.Common.Storage
{
    public interface IStorageAccountFactory
    {
        CloudStorageAccount Get();
    }

    public class StorageAccountFactory : IStorageAccountFactory
    {
        private readonly IConfiguration _configuration;

        public StorageAccountFactory(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public CloudStorageAccount Get()
        {
            return CloudStorageAccount.Parse(_configuration.GetStorageAccountConnectionString());
        }
    }
}