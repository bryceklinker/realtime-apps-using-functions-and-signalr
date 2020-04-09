using System;
using System.IO;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Realtime.Presenter.Mobile.Common.Configuration
{
    public interface IConfig
    {
        string Endpoint { get; }
    }
    
    public class Config : IConfig
    {
        private readonly Lazy<JObject> _lazySettings = new Lazy<JObject>(GetSettings);

        public string Endpoint => _lazySettings.Value.Value<string>("Endpoint");

        private static JObject GetSettings()
        {
            var applicationType = typeof(App);
            using var stream = applicationType.Assembly.GetManifestResourceStream($"{applicationType.Namespace}.appsettings.json");
            using var reader = new StreamReader(stream);
            return JObject.Load(new JsonTextReader(reader));
        }
    }
}