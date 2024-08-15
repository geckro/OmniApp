using Hardcodet.Wpf.TaskbarNotification;
using Microsoft.Extensions.DependencyInjection;
using OmniApp.Common;
using OmniApp.Common.Logging;
using OmniApp.UI.Windows;
using System.Windows;
using System.Windows.Media.Imaging;

namespace OmniApp.UI;

public partial class App
{
    public App()
    {
        Logger.Debug(LogClass.OmniUiWindows, "Starting Application");
    }

    private static IServiceProvider ServiceProvider { get; set; } = null!;

    protected override void OnStartup(StartupEventArgs e)
    {
        base.OnStartup(e);

        for (int i = 0; i != e.Args.Length; ++i)
        {
            if (e.Args[i] == "/Verbose" || e.Args[i] == "/V" || e.Args[i] == "/Debug")
            {
                Arguments.VerboseLogging = true;
            }
        }

        ServiceCollection serviceCollection = [];
        Services.ConfigureDepInjServices(serviceCollection);
        ServiceProvider = serviceCollection.BuildServiceProvider();

        MainWindow mainWindow = ServiceProvider.GetRequiredService<MainWindow>();
        mainWindow.Show();

        TaskbarIcon _ = new()
        {
                IconSource = new BitmapImage(new Uri("pack://application:,,,/OmniApp.UI.Common;component/Images/IconOmniApp.ico")),
                Visibility = Visibility.Visible
        };
    }
}
