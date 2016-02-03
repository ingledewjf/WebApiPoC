namespace UnityApiPoc.Helpers
{
    using System;
    using System.Collections.Generic;
    using System.Net.Http;
    using System.Text;
    using System.Web.Http.Tracing;

    using NLog;

    public sealed class NLogger : ITraceWriter
    {
        private static readonly Logger ClassLogger = LogManager.GetCurrentClassLogger();
        private static readonly Lazy<Dictionary<TraceLevel, Action<string>>>  LoggingMap = new Lazy<Dictionary<TraceLevel, Action<string>>>(() => new Dictionary<TraceLevel, Action<string>> { { TraceLevel.Info, ClassLogger.Info }, { TraceLevel.Debug, ClassLogger.Debug }, { TraceLevel.Error, ClassLogger.Error }, { TraceLevel.Fatal, ClassLogger.Fatal }, { TraceLevel.Warn, ClassLogger.Warn } });

        private Dictionary<TraceLevel, Action<string>> Logger
        {
            get
            {
                return LoggingMap.Value;
            }
        }

        public void Trace(HttpRequestMessage message, string category, TraceLevel level, Action<TraceRecord> traceAction)
        {
            if (level != TraceLevel.Off)
            {
                if (traceAction != null && traceAction.Target != null)
                {
                    category = category + Environment.NewLine + "Action Parameters : " + traceAction.Target;
                }

                var record = new TraceRecord(message, category, level);
                if (traceAction != null)
                {
                    traceAction(record);
                }

                Log(record);
            }
        }

        private void Log(TraceRecord record)
        {
            var message = new StringBuilder();

            if (!string.IsNullOrWhiteSpace(record.Message))
            {
                message.Append(string.Empty).Append(record.Message + Environment.NewLine);
            }

            if (record.Request != null)
            {
                if (record.Request.Method != null)
                {
                    message.Append("Method: " + record.Request.Method + Environment.NewLine);
                }

                if (record.Request.RequestUri != null)
                {
                    message.Append(string.Empty).Append("URL: " + record.Request.RequestUri + Environment.NewLine);
                }
            }

            if (!string.IsNullOrWhiteSpace(record.Category))
            {
                message.Append(string.Empty).Append(record.Category + Environment.NewLine);
            }

            if (!string.IsNullOrWhiteSpace(record.Operator))
            {
                message.Append(" ").Append(record.Operator).Append(" ").Append(record.Operation + Environment.NewLine);
            }

            if (record.Exception != null && !string.IsNullOrWhiteSpace(record.Exception.GetBaseException().Message))
            {
                // todo
                // var exceptionType = record.Exception.GetType();
                message.Append(string.Empty)
                    .Append("Error: " + record.Exception.GetBaseException().Message + Environment.NewLine);
            }

            Logger[record.Level](Convert.ToString(message) + Environment.NewLine);
        }
    }
}