using System.Windows;

namespace OmniApp.UI;

public partial class MainWindow
{
    public MainWindow()
    {
        InitializeComponent();
    }

    private void GameManager_OnClick(object sender, RoutedEventArgs e)
    {
        GameManager.UI.Windows.MainWindow gameWindow = new();
        gameWindow.Show();
    }
}
