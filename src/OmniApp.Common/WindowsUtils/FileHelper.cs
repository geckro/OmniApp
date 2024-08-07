using OmniApp.Common.Logging;
using System.Diagnostics;

namespace OmniApp.Common.WindowsUtils;

public class FileHelper
{
    public static void OpenFileWithDefaultProgram(string fileToOpen)
    {
        Logger.Info(LogClass.OmniCommonWindowsUtils, $"Current working directory: {Directory.GetCurrentDirectory()}");
        if (!File.Exists(fileToOpen))
        {
            Logger.Error(LogClass.OmniCommonWindowsUtils, $"File path {fileToOpen} does not exist.");
            return;
        }

        using Process? process =
                Process.Start(new ProcessStartInfo { FileName = "explorer", Arguments = $"{fileToOpen}" });

        process?.WaitForExit();
    }
}
