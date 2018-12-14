using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Web;

namespace KalturaTestWcfService
{
    public class LogMessage
    {
        public DateTime Timestamp { get; private set; }
        public LoggingLevel Importance { get; private set; }
        public string Message { get; private set; }
        public int ThreadId { get; private set; }

        private LogMessage(DateTime timestamp, LoggingLevel importance, string message, int threadId)
        {
            Timestamp = timestamp;
            Message = message;
            ThreadId = threadId;
            Importance = importance;
        }

        public static LogMessage Create(DateTime timestamp, LoggingLevel importance, string message, int threadId)
        {
            return new LogMessage(timestamp, importance, message, threadId);
        }

        public override string ToString()
        {
            return string.Format("{0}  [TID:{3}] {1:h:mm:ss} \t{2}", Importance, Timestamp, Message, ThreadId);
        }
    }

    public class LoggerFactory : ILoggerFactory
    {
        private readonly BlockingCollection<LogMessage> _pendingMessages;
        private readonly IThreadAdapter _threadAdapter;

        private readonly ConcurrentDictionary<Type, ILogger> _loggersCache = new ConcurrentDictionary<Type, ILogger>();


        public LoggerFactory(BlockingCollection<LogMessage> pendingMessages, IThreadAdapter threadAdapter)
        {
            _pendingMessages = pendingMessages;
            _threadAdapter = threadAdapter;
        }

        public ILogger For(Type loggerFor)
        {
            return _loggersCache.GetOrAdd(loggerFor, new Logger(_pendingMessages, _threadAdapter));
        }
    }

    public class ThreadAdapter : IThreadAdapter
    {
        public int GetCurrentThreadId()
        {
            return Thread.CurrentThread.ManagedThreadId;
        }
    }

    public class SimpleTextFileLogger : ILogListener
    {
        private FileStream _fileStream;

        public void Start()
        {
            _fileStream = new FileStream(AppDomain.CurrentDomain.BaseDirectory + @"\" + "templog.txt", FileMode.Append);
        }

        public void Stop()
        {
            if (_fileStream != null)
            {
                _fileStream.Dispose();
            }
        }

        public void Log(LogMessage message)
        {
            var bytes = Encoding.UTF8.GetBytes(message.ToString() + Environment.NewLine);
            _fileStream.Write(bytes, 0, bytes.Length);
        }
    }

    public interface ILoggerFactory
    {
        ILogger For(Type loggerFor);
    }

    public interface ILogListener
    {
        void Log(LogMessage message);
    }

    public interface IThreadAdapter
    {
        int GetCurrentThreadId();
    }

    public interface IQueueDispatcher
    {
        void Start();
    }
}