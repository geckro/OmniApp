using GameManager.UI.Helpers;
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
        WindowHelper.LoadContent(AddContentArea, new AddGame());
    }

    private void AddGenre_Click(object sender, RoutedEventArgs e)
    {
        WindowHelper.LoadContent(AddContentArea, new AddGenre());
    }

    private void AddPlatform_Click(object sender, RoutedEventArgs e)
    {
        WindowHelper.LoadContent(AddContentArea, new AddPlatform());
    }
}
