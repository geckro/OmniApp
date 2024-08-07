namespace OmniApp.Common.Logging;

public static class Logger
{
    private const string LogFormat = "{0}-{1}-{2}: {3}";
    private static readonly object LockObject = new();

    private static string CurrentTime;

    public static void Info(LogClass logClass, string message)
    {
        Log(CurrentTime = DateTime.Now.ToString("yyyy-MM-dd_HH:mm:ss"), LogLevel.Info, logClass, message);
    }

    public static void Warning(LogClass logClass, string message)
    {
        Log(CurrentTime = DateTime.Now.ToString("yyyy-MM-dd_HH:mm:ss"), LogLevel.Warning, logClass, message);
    }

    public static void Error(LogClass logClass, string message)
    {
        Log(CurrentTime = DateTime.Now.ToString("yyyy-MM-dd_HH:mm:ss"), LogLevel.Error, logClass, message);
    }

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
    OmniUiWindows,
}

public enum LogLevel
{
    Info,
    Warning,
    Error,
    Debug
}
