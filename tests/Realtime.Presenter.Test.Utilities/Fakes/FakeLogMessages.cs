using System;
using Microsoft.Extensions.Logging;

namespace Realtime.Presenter.Test.Utilities.Fakes
{
    public class FakeLogMessages
    {
        public LogLevel LogLevel { get; }
        public EventId EventId { get; }
        public object State { get; }
        public Exception Exception { get; }
        public string Message { get; }

        public FakeLogMessages(LogLevel logLevel, EventId eventId, object state, Exception exception, string message)
        {
            LogLevel = logLevel;
            EventId = eventId;
            State = state;
            Exception = exception;
            Message = message;
        }
    }
}