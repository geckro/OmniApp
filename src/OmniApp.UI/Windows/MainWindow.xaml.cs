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
        new GameManager.UI.Windows.MainWindow().Show();
    }

    private void FinanceManager_OnClick(object sender, RoutedEventArgs e)
    {
        new FinancialManager.UI.Windows.MainWindow().Show();
    }

    private void OtherButton_Click(object sender, RoutedEventArgs e)
    {
        throw new NotSupportedException();
    }
}
