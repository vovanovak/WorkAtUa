using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using log4net;

namespace WorkAtUa.Tools.Logger
{
    public class Log4netLogger : ILogger
    {
        ILog log = null;

        #region ILog
        public void Debug(object message)
        {
            Log.Debug(message);
        }

        public void Debug(object message, Exception exception)
        {
            Log.Debug(message, exception);
        }

        public void Error(object message)
        {
            Log.Error(message);
        }

        public void Error(object message, Exception exception)
        {
            Log.Error(message, exception);
        }

        public void Fatal(object message)
        {
            Log.Fatal(message);
        }

        public void Fatal(object message, Exception exception)
        {
            Log.Fatal(message, exception);
        }

        public void Info(object message)
        {
            Log.Info(message);
        }

        public void Info(object message, Exception exception)
        {
            Log.Info(message, exception);
        }

        public void Warn(object message)
        {
            Log.Warn(message);
        }

        public void Warn(object message, Exception exception)
        {
            Log.Warn(message, exception);
        }
        #endregion

        #region Helpers
        private ILog Log
        {
            get
            {
                if (log == null)
                {
                    log = CreateLogger();
                }
                return log;
            }
        }

        private ILog CreateLogger()
        {
            log4net.Config.XmlConfigurator.Configure();
            return LogManager.GetLogger(this.GetType());
        }
        #endregion

    }
}
