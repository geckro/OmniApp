using System.Windows;
using GameManager.Core.Data;
using GameManager.UI.Helpers;

namespace GameManager.UI.Windows;

public partial class AddGenre
{
    public AddGenre()
    {
        InitializeComponent();
    }

    private void AddNewGenre_Click(object sender, RoutedEventArgs e)
    {
        MetadataHelper.AddNewMetadata<Genre, GenreData>(GenreBox, new GenreData());
    }
}