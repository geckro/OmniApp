using System.Windows;
using GameManager.Core.Data;

namespace GameManager.UI.Windows;

public partial class AddGenre
{
    public AddGenre()
    {
        InitializeComponent();
    }

    private void AddNewGenre_Click(object sender, RoutedEventArgs e)
    {
        string genreToAdd = GenreBox.Text;

        if (string.IsNullOrWhiteSpace(genreToAdd)) return;

        Genre newGenre = new(genreToAdd);
        GenreData genreData = new();

        genreData.Add(newGenre);
    }
}