using Microsoft.Extensions.DependencyInjection;
using OmniApp.Common.Logging;
using OmniApp.UI.ViewModels;
using System.Windows;

namespace OmniApp.UI.Windows;

public partial class MainWindow
{
    public MainWindow()
    {
        Logger.Info(LogClass.OmniUi, "Starting MainWindow");
        InitializeComponent();
    }

    public MainWindow(IServiceProvider sp) : this()
    {
        MainWindowViewModel viewModel = sp.GetRequiredService<MainWindowViewModel>();
        DataContext = viewModel;
    }
}
