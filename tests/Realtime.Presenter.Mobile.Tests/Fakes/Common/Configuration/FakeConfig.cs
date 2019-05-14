using Realtime.Presenter.Mobile.Common.Configuration;

namespace Realtime.Presenter.Mobile.Tests.Fakes.Common.Configuration
{
    public class FakeConfig : IConfig
    {
        public const string EndpointSetting = "https://something.com";

        public string Endpoint => EndpointSetting;
    }
}