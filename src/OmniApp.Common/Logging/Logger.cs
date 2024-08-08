namespace OmniApp.Common.Logging;

/// <summary>
///     Provides static methods for logging messages to the console.
/// </summary>
public static class Logger
{
    private const string LogFormat = "{0}-{1}-{2}: {3}";
    private static readonly object LockObject = new();

    private static string CurrentTime;

    /// <summary>
    ///     Logs an information message.
    /// </summary>
    /// <remarks>
    ///     Use this method when the information would be useful for the average user.
    /// </remarks>
    /// <param name="logClass">The category of the log message</param>
    /// <param name="message">The message to be logged.</param>
    public static void Info(LogClass logClass, string message)
    {
        Log(CurrentTime = DateTime.Now.ToString("yyyy-MM-dd_HH:mm:ss"), LogLevel.Info, logClass, message);
    }

    /// <summary>
    ///     Logs a warning message.
    /// </summary>
    /// <remarks>
    ///     Use this method when there is information that may hinder a specific function of the application.
    /// </remarks>
    /// <param name="logClass">The category of the log message</param>
    /// <param name="message">The message to be logged.</param>
    public static void Warning(LogClass logClass, string message)
    {
        Log(CurrentTime = DateTime.Now.ToString("yyyy-MM-dd_HH:mm:ss"), LogLevel.Warning, logClass, message);
    }

    /// <summary>
    ///     Logs an error message.
    /// </summary>
    /// <remarks>
    ///     Use this method when there is information would greatly hinder the function of the application.
    /// </remarks>
    /// <param name="logClass">The category of the log message</param>
    /// <param name="message">The message to be logged.</param>
    public static void Error(LogClass logClass, string message)
    {
        Log(CurrentTime = DateTime.Now.ToString("yyyy-MM-dd_HH:mm:ss"), LogLevel.Error, logClass, message);
    }

    /// <summary>
    ///     Logs a debugging or verbose message.
    /// </summary>
    /// <remarks>
    ///     Use this method if it will help with debugging or finding issues with the code of the program.
    /// </remarks>
    /// <param name="logClass">The category of the log message</param>
    /// <param name="message">The message to be logged.</param>
    public static void Debug(LogClass logClass, string message)
    {
        Log(CurrentTime = DateTime.Now.ToString("yyyy-MM-dd_HH:mm:ss"), LogLevel.Debug, logClass, message);
    }

    private static void Log(string currentTime, LogLevel logLevel, LogClass logClass, string message)
    {
        if (logLevel != LogLevel.Debug)
        {
            lock (LockObject)
            {
                Console.WriteLine(LogFormat, currentTime, logLevel.ToString().ToUpper(), logClass.ToString(), message);
                return;
            }
        }

        if (Arguments.VerboseLogging)
        {
            lock (LockObject)
            {
                Console.WriteLine(LogFormat, currentTime, logLevel.ToString().ToUpper(), logClass.ToString(), message);
            }
        }
    }
}

/// <summary>
///     Defines the various categories for log messages.
/// </summary>
public enum LogClass
{
    FinanceMgrCoreTransactions,
    FinanceMgrUiViewModels,
    FinanceMgrUiWindows,
    GameMgrCoreConstructors,
    GameMgrCoreMtdAccessor,
    GameMgrCoreMtdAccessorFactory,
    GameMgrCoreMtdPersistence,
    GameMgrCoreScrapersWikimedia,
    GameMgrUiHelpers,
    GameMgrUiManagers,
    GameMgrUiViewModels,
    GameMgrUiWindows,
    OmniCommonArgs,
    OmniCommonData,
    OmniCommonLogging,
    OmniCommonWindowsUtils,
    OmniUiCommonHelpers,
    OmniUiCommonRelayCommand,
    OmniUiDependencyInj,
    OmniUiViewModels,
    OmniUiWindows
}

internal enum LogLevel
{
    Info,
    Warning,
    Error,
    Debug
}
