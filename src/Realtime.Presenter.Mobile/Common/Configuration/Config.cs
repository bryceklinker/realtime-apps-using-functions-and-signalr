using System.IO;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Realtime.Presenter.Mobile.Common.Configuration
{
    public interface IConfig
    {
        string Endpoint { get; }
    }
    
    public class Config
    {
        private readonly JObject _settings;

        public string Endpoint => _settings.Value<string>("Endpoint");

        public Config()
        {
            _settings = GetSettings();
        }

        private static JObject GetSettings()
        {
            var applicationType = typeof(App);
            using (var stream = applicationType.Assembly.GetManifestResourceStream($"{applicationType.Namespace}"))
            using (var reader = new StreamReader(stream))
            {
                return JObject.Load(new JsonTextReader(reader));
            }
        }
    }
}