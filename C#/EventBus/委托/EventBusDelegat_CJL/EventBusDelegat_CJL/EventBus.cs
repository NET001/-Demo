using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Linq;

namespace EventBusDelegat_CJL
{
    public static class EventBus<T>
    {
        private static readonly ReaderWriterLockSlim readWriteLock = new ReaderWriterLockSlim(LockRecursionPolicy.SupportsRecursion);
        private static readonly List<Action<T>> events = new List<Action<T>>();
        public static async Task PublishAsync(T data)
        {
            readWriteLock.EnterReadLock();
            try
            {
                await Task.WhenAny(events.Select(action => Task.Factory.StartNew(() =>
                {
                    action.Invoke(data);
                })));
            }
            finally
            {
                readWriteLock.ExitReadLock();
            }
        }
        public static void Subscriber(Action<T> action)
        {
            readWriteLock.EnterWriteLock();
            try
            {
                events.Add(action);
            }
            finally
            {
                if (readWriteLock.IsWriteLockHeld)
                {
                    readWriteLock.ExitWriteLock();
                }
            }
        }
        public static void UnSubscriber(Action<T> action)
        {

            readWriteLock.EnterWriteLock();
            try
            {
                events.Remove(action);
            }
            finally
            {
                if (readWriteLock.IsWriteLockHeld)
                {
                    readWriteLock.ExitWriteLock();
                }
            }
        }
    }
}
