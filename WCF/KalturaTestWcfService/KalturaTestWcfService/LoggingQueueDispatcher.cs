using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Web;

namespace KalturaTestWcfService
{
    public class LoggingQueueDispatcher : IQueueDispatcher
    {
        private readonly BlockingCollection<LogMessage> _pendingMessages;
        private readonly IEnumerable<ILogListener> _listeners;

        public LoggingQueueDispatcher(BlockingCollection<LogMessage> pendingMessages, IEnumerable<ILogListener> listeners)
        {
            _pendingMessages = pendingMessages;
            _listeners = listeners;
        }

        public void Start()
        {
            var task = new Task(MessageLoop);
            task.Start();
        }

        private void MessageLoop()
        {
            var cancellationToken = new CancellationTokenSource();
            LogMessage message;

            while (_pendingMessages.TryTake(out message, Timeout.Infinite, cancellationToken.Token))
            {
                foreach (var listener in _listeners)
                {
                    listener.Log(message);
                }
            }

        }
    }
}