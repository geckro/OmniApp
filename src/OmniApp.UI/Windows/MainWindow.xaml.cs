using Microsoft.Extensions.DependencyInjection;
using OmniApp.Common.Logging;
using OmniApp.UI.ViewModels;

namespace OmniApp.UI.Windows;

public partial class MainWindow
{
    public MainWindow()
    {
        Logger.Info(LogClass.OmniUiWindows, "Starting MainWindow");
        InitializeComponent();
    }

    public MainWindow(IServiceProvider sp) : this()
    {
        MainWindowViewModel viewModel = sp.GetRequiredService<MainWindowViewModel>();
        DataContext = viewModel;
    }
}
