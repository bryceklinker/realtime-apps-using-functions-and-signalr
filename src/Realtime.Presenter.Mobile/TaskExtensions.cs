using System;
using System.Threading.Tasks;
using Realtime.Presenter.Mobile.Common.ErrorHandling;

namespace Realtime.Presenter.Mobile
{
    public static class TaskExtensions
    {
        public static async void AsFireAndForget(this Task task, IErrorHandler errorHandler)
        {
            try
            {
                await task;
            }
            catch (Exception e)
            {
                errorHandler.Handle(e);
            }
        }
    }
}