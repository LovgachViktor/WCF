using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace KalturaTestWcfService
{
    public class Logger : ILogger
    {
        private readonly BlockingCollection<LogMessage> _pendingMessages;
        private readonly IThreadAdapter _threadAdapter;

        public Logger(BlockingCollection<LogMessage> pendingMessages, IThreadAdapter threadAdapter)
        {
            _pendingMessages = pendingMessages;
            _threadAdapter = threadAdapter;
        }

        public void Error(string message)
        {
            Push(LoggingLevel.Error, message);
        }

        public void Info(string message)
        {
            Push(LoggingLevel.Info, message);
        }

        public void Debug(string message)
        {
            Push(LoggingLevel.Debug, message);
        }

        private void Push(LoggingLevel importance, string message)
        {
            // since we do not know when our log entry will be written to disk, remember current time
            var timestamp = DateTime.Now;
            var threadId = _threadAdapter.GetCurrentThreadId();

            // adds message to the queue in lock-free manner and immediately returns control to caller
            _pendingMessages.Add(LogMessage.Create(timestamp, importance, message, threadId));
        }
    }
}