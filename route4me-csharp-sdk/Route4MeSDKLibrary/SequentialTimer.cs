using System;
using System.Threading;

namespace Route4MeSDKLibrary
{
    /// <summary>
    ///     Wraps <see cref="System.Threading.Timer" /> and guarantees FIFO for timer callbacks (without race conditions).
    /// </summary>
    internal class SequentialTimer : IDisposable
    {
        private readonly Action _callback;
        private readonly TimeSpan _timeout;
        private readonly Timer _timer;

        public SequentialTimer(Action callback, TimeSpan timeout)
        {
            _callback = callback;
            _timeout = timeout;
            _timer = new Timer(OnTick, null, _timeout, Timeout.InfiniteTimeSpan);
        }

        public void Dispose()
        {
            _timer.Dispose();
        }

        private void OnTick(object state)
        {
            try
            {
                _callback();
            }
            catch (Exception)
            {
                //do nothing
            }
            finally
            {
                try
                {
                    _timer.Change(_timeout, Timeout.InfiniteTimeSpan);
                }
                catch (ObjectDisposedException)
                {
                    // catch exception in case of execution callback from disposed timer
                }
            }
        }
    }
}