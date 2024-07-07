using System.Windows;

namespace GameManager.UI.Windows;

public partial class Add
{
    public Add()
    {
        InitializeComponent();
    }

    private void AddGame_Click(object sender, RoutedEventArgs e)
    {
        AddGame addGameWindow = new();
        addGameWindow.Show();
    }

    private void AddGenre_Click(object sender, RoutedEventArgs e)
    {
        AddGenre addGenreWindow = new();
        addGenreWindow.Show();
    }

    private void AddPlatform_Click(object sender, RoutedEventArgs e)
    {
        AddPlatform addPlatformWindow = new();
        addPlatformWindow.Show();
    }
}