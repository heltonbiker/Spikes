using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using log4net;
using log4net.Config;
using System.IO;
using System.Reflection;

namespace Miotec.Vert3d.DomainModel
{
    
    public static class Logger {

        private static bool isInitialized;


        // CONSTRUTOR
        static Logger() {
            isInitialized = false;
        }



        public static void Initialize() {
            Initialize(null);
        }

        public static void Initialize(string configFile) {
            if (!isInitialized) {
                if (!String.IsNullOrEmpty(configFile))
                    XmlConfigurator.ConfigureAndWatch(new FileInfo(configFile));
                else
                    XmlConfigurator.Configure();
                isInitialized = true;
            }
            else
                throw new LoggingInitializationException("Logging has already been initialized.");
        }





        //public static void Log(LoggingLevel loggingLevel, string message) {
        //    Log(loggingLevel, message, null, null);
        //}

        //public static void Log(LoggingLevel loggingLevel, string message, object loggingProperties) {
        //    Log(loggingLevel, message, loggingProperties, null);
        //}




        /// <summary>
        /// Loga em todos os loggers ativos.
        /// </summary>
        public static void Log(LoggingLevel loggingLevel, string message, object loggingProperties = null, Exception exception = null) {
            foreach (ILog log in GetLeafLoggers())
                LogBase(log, loggingLevel, message, loggingProperties, exception);
        }




        //public static void Log(string logName, LoggingLevel loggingLevel, string message) {
        //    Log(logName, loggingLevel, message, null, null);
        //}
   
        //public static void Log(string logName, LoggingLevel loggingLevel, string message, object loggingProperties) {
        //    Log(logName, loggingLevel, message, loggingProperties, null);
        //}




        /// <summary>
        /// Loga somente no arquivo especificado.
        /// </summary>
        /// <param name="logName">Caminho do arquivo de log.</param>
        public static void Log(string logName, LoggingLevel loggingLevel, string message, object loggingProperties = null, Exception exception = null) {
            ILog log = LogManager.GetLogger(logName);
            if (log != null)
                LogBase(log, loggingLevel, message, loggingProperties, exception);
            else
                throw new InvalidLogException("The log \"" + logName + "\" does not exist or is invalid.", logName);
        }


        private static void LogBase(ILog log, LoggingLevel loggingLevel, string message, object loggingProperties, Exception exception) {
            if (ShouldLog(log, loggingLevel)) {
                PushLoggingProperties(loggingProperties);
                switch (loggingLevel) {
                    case LoggingLevel.Debug: log.Debug(message, exception); break;
                    case LoggingLevel.Info: log.Info(message, exception); break;
                    case LoggingLevel.Warning: log.Warn(message, exception); break;
                    case LoggingLevel.Error: log.Error(message, exception); break;
                    case LoggingLevel.Fatal: log.Fatal(message, exception); break;
                }
                PopLoggingProperties(loggingProperties);
            }
        }


        private static IEnumerable<ILog> GetLeafLoggers()
        {
            ILog[] allLogs = LogManager.GetCurrentLoggers();
            IList<ILog> leafLogs = new List<ILog>();
            for (int i = 0; i < allLogs.Length; i++) {
                bool isParent = false;
                for (int j = 0; j < allLogs.Length; j++) {
                    if (i != j && allLogs[j].Logger.Name.StartsWith(string.Format("{0}.", allLogs[i].Logger.Name))) {
                        isParent = true;
                        break;
                    }
                }
                if (!isParent)
                    leafLogs.Add(allLogs[i]);
            }
            return leafLogs;
        }


        private static void PushLoggingProperties(object loggingProperties) {
            if (loggingProperties != null) {
                Type attrType = loggingProperties.GetType();
                PropertyInfo[] properties = attrType.GetProperties(BindingFlags.Public | BindingFlags.Instance);
                for (int i = 0; i < properties.Length; i++) {
                    object value = properties[i].GetValue(loggingProperties, null);
                    if (value != null)
                        ThreadContext.Stacks[properties[i].Name].Push(value.ToString());
                }
            }
        }

        private static void PopLoggingProperties(object loggingProperties) {
            if (loggingProperties != null) {
                Type attrType = loggingProperties.GetType();
                PropertyInfo[] properties = attrType.GetProperties(BindingFlags.Public | BindingFlags.Instance);
                for (int i = properties.Length - 1; i >= 0; i--) {
                    object value = properties[i].GetValue(loggingProperties, null);
                    if (value != null)
                        ThreadContext.Stacks[properties[i].Name].Pop();
                }
            }
        }

        private static bool ShouldLog(ILog log, LoggingLevel loggingLevel) {
            switch (loggingLevel) {
                case LoggingLevel.Debug: return log.IsDebugEnabled;
                case LoggingLevel.Info: return log.IsInfoEnabled;
                case LoggingLevel.Warning: return log.IsWarnEnabled;
                case LoggingLevel.Error: return log.IsErrorEnabled;
                case LoggingLevel.Fatal: return log.IsFatalEnabled;
                default: return false;
            }
        }
    }
}
