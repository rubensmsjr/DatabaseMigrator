using System;
using System.IO;
using log4net;

namespace DatabaseMigrator.Logger
{
    public class Log4NetLogger:ILogger
    {
        private bool configIsTrue = false;
        private ILog logger;

        public Log4NetLogger()
        {
            try
            {
                using (FileStream file = new FileStream(Path.Combine(AppDomain.CurrentDomain.BaseDirectory,"ConfigLog.xml"), FileMode.Open))
                {
                    log4net.Config.XmlConfigurator.Configure(file);
                    logger = log4net.LogManager.GetLogger("DBMigratorLogger");
                }
                configIsTrue = true;
            }
            catch
            {
                configIsTrue = false;
            }

        }

        public void Debug(object message)
        {
            if (configIsTrue)
            {
                logger.Debug(message);
            }
        }

        public void Error(object message)
        {
            if (configIsTrue)
            {
                logger.Error(message);
            }
        }

        public void Info(object message)
        {
            if (configIsTrue)
            {
                logger.Info(message);
            }
        }

        public void Fatal(object message)
        {
            if (configIsTrue)
            {
                logger.Fatal(message);
            }
        }

        public void Warn(object message)
        {
            if (configIsTrue)
            {
                logger.Warn(message);
            }
        }
    }
}
