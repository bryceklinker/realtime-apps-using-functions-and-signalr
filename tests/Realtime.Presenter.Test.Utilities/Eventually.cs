using System;
using System.Threading.Tasks;

namespace Realtime.Presenter.Test.Utilities
{
    public static class Eventually
    {
        private const int DefaultTimeout = 10000;
        private const int DefaultDelay = 100;
        
        public static async Task Should(Action assertion, int timeoutMs = DefaultTimeout, int delayMs = DefaultDelay)
        {
            var endTime = DateTime.UtcNow.AddMilliseconds(timeoutMs);
            Exception exception = null;
            while (DateTime.UtcNow <= endTime)
            {
                try
                {
                    assertion();
                    break;
                }
                catch (Exception e)
                {
                    exception = e;
                    await Task.Delay(delayMs);
                }
            }

            if (exception != null)
                throw exception;
        }
    }
}