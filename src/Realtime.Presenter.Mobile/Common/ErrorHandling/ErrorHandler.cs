using System;
using System.Collections.Generic;
using Realtime.Presenter.Mobile.Common.Messaging;
using Xamarin.Forms;

namespace Realtime.Presenter.Mobile.Common.ErrorHandling
{
    public interface IErrorHandler
    {
        int ErrorCount { get; }
        void Handle(Exception exception);
    }
    
    public class ErrorHandler : IErrorHandler
    {
        private readonly IMessageBus _messageBus;
        private readonly List<Exception> _exceptions;
        public int ErrorCount => _exceptions.Count;

        public ErrorHandler(IMessageBus messageBus)
        {
            _messageBus = messageBus;
            _exceptions = new List<Exception>();
        }

        public void Handle(Exception exception)
        {
            _exceptions.Add(exception);
            _messageBus.Publish(ErrorCount);
        }
    }
}