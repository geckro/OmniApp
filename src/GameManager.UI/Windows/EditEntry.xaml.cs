using GameManager.Core.Data.MetadataConstructors;
using System.Windows;
using System.Windows.Media;

namespace GameManager.UI.Windows;

public partial class EditEntry
{
    public EditEntry(Game gameData)
    {
        InitializeComponent();

        Title = $"Edit Entry: {gameData.Title}";

        GameTitle.Content = gameData.Title;
        GameTitle.FontSize = 24;
        GameTitle.FontWeight = FontWeights.Bold;
        GameTitle.FontFamily = new FontFamily("Century Gothic");
        GameTitle.FontStretch = FontStretches.Expanded;
        GameTitle.HorizontalAlignment = HorizontalAlignment.Center;
        GameTitle.Foreground = new SolidColorBrush(Color.FromArgb(255, 25, 50, 100));

        List<string> genreNames = [];
        genreNames.AddRange(gameData.Genres.Select(g => g.Name));
        GenreListBox.ItemsSource = genreNames;

        List<string> platformNames = [];
        platformNames.AddRange((gameData.Platforms).Select(p => p.Name));
        PlatformListBox.ItemsSource = platformNames;
    }
}

