using OmniApp.Common.Logging;
using System.Windows;

namespace OmniApp.UI.Windows;

public partial class MainWindow
{
    public MainWindow()
    {
        Logger.Info(LogClass.OmniUi, "Starting MainWindow");
        InitializeComponent();
    }

    private void GameManager_OnClick(object sender, RoutedEventArgs e)
    {
        GameManager.UI.Windows.MainWindow gameWindow = new();
        gameWindow.Show();
    }

    private void FinanceManager_OnClick(object sender, RoutedEventArgs e)
    {
        throw new NotImplementedException();
    }

    private void OtherButton_Click(object sender, RoutedEventArgs e)
    {
        throw new NotSupportedException();
    }
}
