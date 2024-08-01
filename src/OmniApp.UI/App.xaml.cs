using Microsoft.Extensions.DependencyInjection;
using OmniApp.Common.Logging;
using OmniApp.UI.Windows;
using System.Windows;

namespace OmniApp.UI;

public partial class App
{
    public App()
    {
        Logger.Debug(LogClass.OmniUi, "Starting Application");
    }

    private static IServiceProvider ServiceProvider { get; set; } = null!;

    protected override void OnStartup(StartupEventArgs e)
    {
        base.OnStartup(e);
        ServiceCollection serviceCollection = [];
        Services.ConfigureDepInjServices(serviceCollection);
        ServiceProvider = serviceCollection.BuildServiceProvider();

        MainWindow mainWindow = ServiceProvider.GetRequiredService<MainWindow>();
        mainWindow.Show();
    }
}
