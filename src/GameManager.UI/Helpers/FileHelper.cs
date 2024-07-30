using OmniApp.Common.Logging;
using System.Diagnostics;
using System.IO;

namespace GameManager.UI.Helpers;

public class FileHelper
{
    public static void OpenFileWithDefaultProgram(string fileToOpen)
    {
        Logger.Info(LogClass.GameMgrUi, $"Current working directory: {Directory.GetCurrentDirectory()}");
        if (!File.Exists(fileToOpen))
        {
            Logger.Error(LogClass.GameMgrUi, $"File path {fileToOpen} does not exist.");
            return;
        }

        using Process? process = Process.Start(new ProcessStartInfo()
        {
            FileName = "explorer",
            Arguments = $"{fileToOpen}"
        });

        process?.WaitForExit();

    }
}
