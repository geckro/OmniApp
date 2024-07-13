using OmniApp.Common.Logging;

namespace OmniApp.UI;

public partial class App
{
    public App()
    {
        Logger.Debug(LogClass.OmniUi, "Starting Application");
    }
}
