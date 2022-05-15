using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Storage;
using Serilog;

namespace PlexPlaylistImport
{
    internal static class Logger
    {
        private static bool isInitialized = false;
        public static void Information(string message)
        {
            if (!isInitialized)
            {
                Initialize();
            }
            Log.Information(message);
        }
        public static void Debug(string message)
        {
            if (!isInitialized)
            {
                Initialize();
            }
            Log.Debug(message);
        }
        public static void Warning(string message)
        {
            if (!isInitialized)
            {
                Initialize();
            }
            Log.Warning(message);
        }
        public static void Error(string message)
        {
            if (!isInitialized)
            {
                Initialize();
            }
            Log.Error(message);
        }
        private static void Initialize()
        {
            string logFilePath = Path.Combine(ApplicationData.Current.LocalFolder.Path, "logs\\log.txt");
            Log.Logger = new LoggerConfiguration()
                .MinimumLevel.Debug()
                .WriteTo.File(logFilePath, rollingInterval: RollingInterval.Day)
                .CreateLogger();
            isInitialized = true;
            Log.Debug("Initialized Serilog");
        }
    }
}
