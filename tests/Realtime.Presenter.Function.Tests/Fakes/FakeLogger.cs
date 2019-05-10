using System;
using System.Collections.Generic;
using Microsoft.Extensions.Logging;

namespace Realtime.Presenter.Function.Tests.Fakes
{
    public class FakeLogger : ILogger
    {
        private readonly List<FakeLogMessages> _messages = new List<FakeLogMessages>();

        public IEnumerable<FakeLogMessages> Messages => _messages;
        
        public void Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception exception, Func<TState, Exception, string> formatter)
        {
            var message = formatter(state, exception);
            _messages.Add(new FakeLogMessages(logLevel, eventId, state, exception, message));
        }

        public bool IsEnabled(LogLevel logLevel)
        {
            return true;
        }

        public IDisposable BeginScope<TState>(TState state)
        {
            return null;
        }
    }
}