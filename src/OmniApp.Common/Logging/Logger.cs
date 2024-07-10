namespace OmniApp.Common.Logging;

public static class Logger
{
    private static readonly object LockObject = new();

    private const string LogFormat = "{0} - {1}: {2}";

    public static void Info(LogClass logClass, string message)
    {
        Log(LogLevel.Info, logClass, message);
    }

    public static void Warning(LogClass logClass, string message)
    {
        Log(LogLevel.Warning, logClass, message);
    }

    public static void Error(LogClass logClass, string message)
    {
        Log(LogLevel.Error, logClass, message);
    }

    public static void Debug(LogClass logClass, string message)
    {
        Log(LogLevel.Debug, logClass, message);
    }

    private static void Log(LogLevel logLevel, LogClass logClass, string message)
    {
        if (logLevel != LogLevel.Debug)
        {
            lock (LockObject)
            {
                Console.WriteLine(
                    LogFormat,
                    logLevel.ToString().ToUpper(),
                    logClass.ToString(),
                    message
                );
                return;
            }
        }
        lock (LockObject)
        {
            System.Diagnostics.Debug.WriteLine(
                LogFormat,
                logLevel.ToString().ToUpper(),
                logClass.ToString(),
                message
            );
        }
    }
}

public enum LogClass
{
    OmniCommon,
    OmniUi,
    GameMgrCore,
    GameMgrUi
}

public enum LogLevel
{
    Info,
    Warning,
    Error,
    Debug
}
