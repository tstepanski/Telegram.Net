using System;
using System.Threading.Tasks;
using TelegramNet.ExtraTypes;

namespace TelegramNet.Logging
{
    public static class Logger
    {
        public static event Func<LogMessage, Task> OnMessage;
        
        private static bool _useConsole;
        
        internal static void UseConsole(bool use)
        {
            _useConsole = use;
        }

        public static void Log(string message, LogSource source, string customSource = null)
        {
            OnMessage?.Invoke(new LogMessage()
            {
                Message = message,
                Source = source,
                CustomSource = customSource
            });

            if (!_useConsole) return;

            switch (source)
            {
                case LogSource.TelegramApiServer:
                    Console.ForegroundColor = ConsoleColor.Blue;
                    Console.Write($"[TELEGRAM API {DateTime.Now.ToShortTimeString()}]");
                    break;
                case LogSource.LocalServer:
                    Console.ForegroundColor = ConsoleColor.Cyan;
                    Console.Write($"[LOCAL {DateTime.Now.ToShortTimeString()}]");
                    break;
                case LogSource.Custom:
                    Console.ForegroundColor = ConsoleColor.Magenta;
                    Console.Write($"[{customSource?.ToUpper()} {DateTime.Now.ToShortTimeString()}]");
                    break;
                case LogSource.Info:
                    Console.ForegroundColor = ConsoleColor.Gray;
                    Console.Write($"[INFO] {DateTime.Now.ToShortTimeString()}");
                    break;
                case LogSource.Warn:
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.Write($"[WARN {DateTime.Now.ToShortTimeString()}]");
                    break;
                case LogSource.Error:
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.Write($"[ERR {DateTime.Now.ToShortTimeString()}]");
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(source), source, null);
            }
            Console.ResetColor();
            Console.WriteLine(" " + message);
        }
    }

    public class LogMessage
    {
        public string Message { get; init; }
        
        public LogSource Source { get; init; }
        
        public Optional<string> CustomSource { get; init; }
    }

    public enum LogSource
    {
        TelegramApiServer,
        LocalServer,
        Custom,
        Info,
        Warn, 
        Error
    }
}