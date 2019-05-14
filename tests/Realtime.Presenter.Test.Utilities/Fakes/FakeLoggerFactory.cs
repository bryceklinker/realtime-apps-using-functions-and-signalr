using Microsoft.Extensions.Logging;

namespace Realtime.Presenter.Test.Utilities.Fakes
{
    public class FakeLoggerFactory : ILoggerFactory
    {
        public FakeLogger Logger { get; } = new FakeLogger();
        
        public void Dispose()
        {
            
        }

        public ILogger CreateLogger(string categoryName)
        {
            return Logger;
        }

        public void AddProvider(ILoggerProvider provider)
        {
            
        }
    }
}