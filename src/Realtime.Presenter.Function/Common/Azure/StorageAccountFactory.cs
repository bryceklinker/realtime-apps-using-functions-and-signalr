using Microsoft.Extensions.Configuration;
using Microsoft.WindowsAzure.Storage;

namespace Realtime.Presenter.Function.Common.Azure
{
    public interface IStorageAccountFactory
    {
        ICloudStorageAccount Get();
    }

    public class StorageAccountFactory : IStorageAccountFactory
    {
        private readonly IConfiguration _config;

        private string StorageAccountConnectionString => _config["StorageAccountConnectionString"];

        public StorageAccountFactory(IConfiguration config)
        {
            _config = config;
        }

        public ICloudStorageAccount Get()
        {
            var account = CloudStorageAccount.Parse(StorageAccountConnectionString);
            return new CloudStorageAccountWrapper(account);
        }
    }
}