using log4net;
using log4net.Config;
using System.IO;

namespace PageObject.Test
{
    public static class LogInitializer
    {
        private static bool _isInitialized = false;

        public static void Initialize()
        {
            if (_isInitialized) return;

            var logConfigFile = new FileInfo("log4net.config");
            if (logConfigFile.Exists)
            {
                XmlConfigurator.Configure(logConfigFile);
                LogManager.GetLogger(typeof(LogInitializer)).Info("Log4net configured globally.");
                _isInitialized = true;
            }
        }
    }
}