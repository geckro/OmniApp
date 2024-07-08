using System.Windows;

namespace GameManager.UI.Windows;

public partial class Add
{
    public Add()
    {
        InitializeComponent();
    }

    private void LoadContent(Window window)
    {
        AddContentArea.Content = window.Content;
    }

    private void AddGame_Click(object sender, RoutedEventArgs e)
    {
        LoadContent(new AddGame());
    }

    private void AddGenre_Click(object sender, RoutedEventArgs e)
    {
        LoadContent(new AddGenre());
    }

    private void AddPlatform_Click(object sender, RoutedEventArgs e)
    {
        LoadContent(new AddPlatform());
    }
}