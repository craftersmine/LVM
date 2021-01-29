using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace craftersmine.LVM.Core
{
    /// <summary>
    /// Represents logging methods. This class cannot be inherited
    /// </summary>
    public sealed class Logger
    {
        private DateTime creationTime;
        private string lineCtorString = "{date} [{prefix}] {contents}";

        /// <summary>
        /// Gets log file full path
        /// </summary>
        public string FullPath { get; private set; }
        
        /// <summary>
        /// Creates new instance of logger with specified log name and log directory
        /// </summary>
        /// <param name="logDir">Log file directory path</param>
        /// <param name="logName">Log file name prefix</param>
        public Logger(string logDir, string logName)
        {
            creationTime = DateTime.Now;

            string filename = logName + "_" + creationTime.ToString("s") + ".log";
            filename = filename.Replace(":", "_");

            FullPath = Path.Combine(logDir, filename);

            if (!Directory.Exists(logDir))
                Directory.CreateDirectory(logDir);

            File.WriteAllText(FullPath, "");
        }

        private void AppendTextToFile(string data)
        {
            if (!data.EndsWith("\r\n"))
                data += "\r\n";
            File.AppendAllText(FullPath, data);
        }

        private string GetCurrentDateTimeString()
        {
            return DateTime.Now.ToString("s");
        }

        /// <summary>
        /// Logs line with specified prefix, contents. If <paramref name="consoleOnly"/> is false, puts line in file
        /// </summary>
        /// <param name="prefix">Log line prefix</param>
        /// <param name="contents">Log line contents</param>
        /// <param name="consoleOnly">If <paramref name="consoleOnly"/> is false, puts line in file</param>
        public void Log(string prefix, string contents, bool consoleOnly)
        {
            if (!Settings.EnableLogging)
                return;

            string line = lineCtorString.Replace("{date}", GetCurrentDateTimeString()).Replace("{prefix}", prefix).Replace("{contents}", contents);
            Console.WriteLine(line);
            if (!consoleOnly)
                AppendTextToFile(line);
        }

        /// <summary>
        /// Logs line with specified prefix, contents to file
        /// </summary>
        /// <param name="prefix">Log line prefix</param>
        /// <param name="contents">Log line contents</param>
        public void Log(string prefix, string contents)
        {
            Log(prefix, contents, false);
        }

        /// <summary>
        /// Logs line with specified prefix, contents. If <paramref name="consoleOnly"/> is false, puts line in file
        /// </summary>
        /// <param name="prefix">Log line prefix from defined list of prefixes</param>
        /// <param name="contents">Log line contents</param>
        /// <param name="additionalPrefix">Additional log line prefix. Will be put in [PREFIX/ADDITIONAL_PREFIX]</param>
        /// <param name="consoleOnly">If <paramref name="consoleOnly"/> is false, puts line in file</param>
        public void Log(LogEntryType prefix, string additionalPrefix, string contents, bool consoleOnly)
        {
            Log(prefix.ToString().ToUpper() + "/" + additionalPrefix.ToUpper(), contents, consoleOnly);
        }

        /// <summary>
        /// Logs line with specified prefix, contents.
        /// </summary>
        /// <param name="prefix">Log line prefix from defined list of prefixes</param>
        /// <param name="additionalPrefix">Additional log line prefix. Will be put in [PREFIX/ADDITIONAL_PREFIX]</param>
        /// <param name="contents">Log line contents</param>
        public void Log(LogEntryType prefix, string additionalPrefix, string contents)
        {
            Log(prefix, additionalPrefix, contents, false);
        }

        /// <summary>
        /// Logs line with specified prefix, contents. If <paramref name="consoleOnly"/> is false, puts line in file
        /// </summary>
        /// <param name="prefix">Log line prefix from defined list of prefixes</param>
        /// <param name="contents">Log line contents</param>
        /// <param name="consoleOnly">If <paramref name="consoleOnly"/> is false, puts line in file</param>
        public void Log(LogEntryType prefix, string contents, bool consoleOnly)
        {
            Log(prefix.ToString().ToUpper(), contents, consoleOnly);
        }

        /// <summary>
        /// Logs line with specified prefix, contents.
        /// </summary>
        /// <param name="prefix">Log line prefix from defined list of prefixes</param>
        /// <param name="contents">Log line contents</param>
        public void Log(LogEntryType prefix, string contents)
        {
            Log(prefix, contents, false);
        }

        /// <summary>
        /// Logs exception in log file with specified prefix. If <paramref name="consoleOnly"/> is false, puts line in file
        /// </summary>
        /// <param name="prefix">Log lines prefix</param>
        /// <param name="ex">Exception to be logged</param>
        /// <param name="consoleOnly">If <paramref name="consoleOnly"/> is false, puts line in file</param>
        public void LogException(string prefix, Exception ex, bool consoleOnly)
        {
            if (!Settings.EnableLogging)
                return;

            Log(prefix, "An exception has occured!");
            if (ex != null)
            {
                Log(prefix, "Exception message: " + ex.Message);
                Log(prefix, "Exception type: " + ex.GetType().ToString());
                if (ex.StackTrace != null)
                {
                    string[] stacktrace = ex.StackTrace.Split(new string[] { "\r\n", "\r", "\n" }, StringSplitOptions.RemoveEmptyEntries);
                    Log(prefix, "Begin of exception stacktrace");
                    foreach (var line in stacktrace)
                    {
                        Log(prefix, line);
                    }
                    Log(prefix, "End of exception stacktrace");
                }
                else Log(prefix, "No stacktrace available!");

                if (ex.InnerException != null)
                {
                    Log(prefix, "Inner exception beginning");
                    LogException(prefix, ex.InnerException, consoleOnly);
                }
            }
            else Log(prefix, "Unable to get additional exception information!");
        }

        /// <summary>
        /// Logs exception in log file with specified prefix.
        /// </summary>
        /// <param name="prefix">Log lines prefix</param>
        /// <param name="ex">Exception to be logged</param>
        public void LogException(string prefix, Exception ex)
        {
            LogException(prefix, ex, false);
        }

        /// <summary>
        /// Logs exception in log file with specified prefix. If <paramref name="consoleOnly"/> is false, puts line in file
        /// </summary>
        /// <param name="prefix">Log lines prefix</param>
        /// <param name="additionalPrefix">Additional log line prefix. Will be put in [PREFIX/ADDITIONAL_PREFIX]</param>
        /// <param name="ex">Exception to be logged</param>
        /// <param name="consoleOnly">If <paramref name="consoleOnly"/> is false, puts line in file</param>
        public void LogException(LogEntryType prefix, string additionalPrefix, Exception ex, bool consoleOnly)
        {
            LogException(prefix.ToString().ToUpper() + "/" + additionalPrefix.ToUpper(), ex, consoleOnly);
        }

        /// <summary>
        /// Logs exception in log file with specified prefix. If <paramref name="consoleOnly"/> is false, puts line in file
        /// </summary>
        /// <param name="prefix">Log lines prefix</param>
        /// <param name="ex">Exception to be logged</param>
        /// <param name="consoleOnly">If <paramref name="consoleOnly"/> is false, puts line in file</param>
        public void LogException(LogEntryType prefix, Exception ex, bool consoleOnly)
        {
            LogException(prefix.ToString().ToUpper(), ex, consoleOnly);
        }

        /// <summary>
        /// Logs exception in log file with specified prefix.
        /// </summary>
        /// <param name="prefix">Log lines prefix</param>
        /// <param name="additionalPrefix">Additional log line prefix. Will be put in [PREFIX/ADDITIONAL_PREFIX]</param>
        /// <param name="ex">Exception to be logged</param>
        public void LogException(LogEntryType prefix, string additionalPrefix, Exception ex)
        {
            LogException(prefix.ToString().ToUpper() + "/" + additionalPrefix.ToUpper(), ex, false);
        }

        /// <summary>
        /// Logs exception in log file with specified prefix.
        /// </summary>
        /// <param name="prefix">Log lines prefix</param>
        /// <param name="ex">Exception to be logged</param>
        public void LogException(LogEntryType prefix, Exception ex)
        {
            LogException(prefix.ToString().ToUpper(), ex, false);
        }
    }

    /// <summary>
    /// Log entry prefixes
    /// </summary>
    public enum LogEntryType
    {
        /// <summary>
        /// Error message
        /// </summary>
        Error = 1,
        /// <summary>
        /// Info message
        /// </summary>
        Info = 0,
        /// <summary>
        /// Warning message
        /// </summary>
        Warning = 2,
        /// <summary>
        /// Critical message
        /// </summary>
        Critical = 5,
        /// <summary>
        /// Network connection info, error, warning message
        /// </summary>
        Connection = 7,
        /// <summary>
        /// Successful operation message
        /// </summary>
        Success = 6,
        /// <summary>
        /// Operation done message
        /// </summary>
        Done = 3,
        /// <summary>
        /// Debug message
        /// </summary>
        Debug = 10,
        /// <summary>
        /// Stacktrace message
        /// </summary>
        Stacktrace = 4,
        /// <summary>
        /// Unknown message
        /// </summary>
        Unknown = 9,
        /// <summary>
        /// Application crash message
        /// </summary>
        Crash = 8
    }
}
