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

    public MainWindow(IServiceProvider serviceProvider) : this()
    {
        MainWindowViewModel viewModel = serviceProvider.GetRequiredService<MainWindowViewModel>();
        DataContext = viewModel;
    }

    private void OtherButton_Click(object sender, RoutedEventArgs e)
    {
        throw new NotSupportedException();
    }

    private void CSharpTesting_OnClick(object sender, RoutedEventArgs e)
    {
        new CSharpReferences.UI.MainWindow().Show();
    }
}
