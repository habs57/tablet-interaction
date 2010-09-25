using System;
using System.Text;

namespace TablectionServer
{
    public enum LogType
    {
        Normal,
        Error
    }

    public sealed class Logger
    {
        public Action<string> LogHandler;

        public void CreateLog(LogType type, string handler, string data)
        {
            if (this.LogHandler != null)
            {
                StringBuilder logBuilder = new StringBuilder();
                logBuilder.AppendFormat("[{0}]", handler);
                logBuilder.AppendFormat("- {0}", data);
                this.CreateLog(type, logBuilder.ToString());
            }
        }

        public void CreateLog(LogType type, string data)
        {
            if (this.LogHandler != null)
            {
                StringBuilder logBuilder = new StringBuilder();
                if (type == LogType.Error)
                {
                    logBuilder.Append("[ERROR]");
                }
                logBuilder.AppendFormat("[{0}]", DateTime.Now);
                logBuilder.AppendFormat("{0}", data);
                this.LogHandler(logBuilder.ToString());
            }
        }

    }
}
