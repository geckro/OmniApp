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
        AddContentArea.Content = addGameWindow.Content;
    }

    private void AddGenre_Click(object sender, RoutedEventArgs e)
    {
        AddGenre addGenreWindow = new();
        AddContentArea.Content = addGenreWindow.Content;
    }

    private void AddPlatform_Click(object sender, RoutedEventArgs e)
    {
        AddPlatform addPlatformWindow = new();
        AddContentArea.Content = addPlatformWindow.Content;
    }
}