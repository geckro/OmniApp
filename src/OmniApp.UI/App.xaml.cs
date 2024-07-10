using OmniApp.Common.Logging;

namespace OmniApp.UI;

/// <summary>
///     Interaction logic for App.xaml
/// </summary>
public partial class App
{
    public App()
    {
        Logger.Debug(LogClass.OmniUi, "Starting Application");
    }
}
