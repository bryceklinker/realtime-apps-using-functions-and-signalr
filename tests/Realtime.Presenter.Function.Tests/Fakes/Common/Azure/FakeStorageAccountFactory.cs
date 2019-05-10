using Realtime.Presenter.Function.Common.Azure;

namespace Realtime.Presenter.Function.Tests.Fakes.Common.Azure
{
    public class FakeStorageAccountFactory : IStorageAccountFactory
    {
        public FakeCloudStorageAccount Account { get; } = new FakeCloudStorageAccount();

        public ICloudStorageAccount Get()
        {
            return Account;
        }
    }
}