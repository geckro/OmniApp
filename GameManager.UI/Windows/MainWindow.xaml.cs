using System.Windows;

namespace GameManager.UI.Windows;

public partial class MainWindow
{
    public MainWindow()
    {
        InitializeComponent();
    }

    private void AddGameButton_Click(object sender, RoutedEventArgs e)
    {
        AddGame addGameWindow = new();
        addGameWindow.Show();
    }
}