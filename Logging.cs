using System;
using System.IO;
using System.Drawing;

namespace SafeSharp
{
    /// <summary>
    /// Provides logging functionalities to write logs to files based on the current 1-hour window.
    /// </summary>
    public static class Logging
    {
        private static readonly object _lock = new object();

        /// <summary>
        /// Logs an error message to a file corresponding to the current 1-hour window.
        /// </summary>
        /// <param name="message">The error message to log.</param>
        public static void LogError(string message)
        {
            try
            {
                string logDirectory = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "SafeSharp");
                if (!Directory.Exists(logDirectory))
                {
                    Directory.CreateDirectory(logDirectory);
                }

                string logFileName = GetLogFileName();
                string logFilePath = Path.Combine(logDirectory, logFileName);

                string logMessage = $"{DateTime.Now:yyyy-MM-dd HH:mm:ss.fff} {message}";

                lock (_lock)
                {
                    File.AppendAllText(logFilePath, logMessage + Environment.NewLine);
                }
            }
            catch (Exception ex)
            {
                Aimsharp.PrintMessage("SafeSharp logging failure!", Color.IndianRed);
                Aimsharp.PrintMessage(ex.Message, Color.IndianRed);

            }
        }

        /// <summary>
        /// Gets the log file name based on the current 1-hour window.
        /// </summary>
        /// <returns>The log file name in the format "yyyy-MM-dd HH-HH.log".</returns>
        private static string GetLogFileName()
        {
            DateTime now = DateTime.Now;
            int hourStart = now.Hour;
            int hourEnd = (hourStart + 1) % 24;

            string datePart = now.ToString("yyyy-MM-dd");
            string fileName = $"{datePart} {hourStart:D2}-{hourEnd:D2}.log";

            return fileName;
        }
    }
}
