using System;
using Xamarin.Forms;

namespace Realtime.Presenter.Mobile.Common.Messaging
{
    public interface IMessageBus
    {
        void Publish<T>(T args);
        void Subscribe<T>(Action<T> handler);
    }
    
    public class MessageBus : IMessageBus
    {
        private readonly IMessagingCenter _messagingCenter;

        public MessageBus(IMessagingCenter messagingCenter)
        {
            _messagingCenter = messagingCenter;
        }

        public void Publish<T>(T args)
        {
            _messagingCenter.Send<IMessageBus, T>(this, typeof(T).Name, args);
        }

        public void Subscribe<T>(Action<T> handler)
        {
            _messagingCenter.Subscribe<IMessageBus, T>(this, typeof(T).Name, (_, args) => handler(args));
        }
    }
}