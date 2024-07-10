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
        GameManager.UI.App gameApp = new();
        gameApp.InitializeComponent();
        gameApp.Run();
    }
}
