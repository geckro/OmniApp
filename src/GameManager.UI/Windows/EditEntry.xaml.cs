using GameManager.Core.Data;
using GameManager.Core.Data.MetadataConstructors;
using GameManager.UI.Helpers;
using GameManager.UI.ViewModels;
using Microsoft.Extensions.DependencyInjection;
using OmniApp.Common.Logging;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace GameManager.UI.Windows;

public partial class EditEntry
{
    private readonly DataGridHelper _dataGridHelper;
    private readonly MetadataAccessor<Developer> _developerMetadataAccessor;
    private readonly MetadataAccessor<Game> _gameMetadataAccessor;
    private readonly MetadataAccessor<Genre> _genreMetadataAccessor;
    private readonly MetadataAccessor<Platform> _platformMetadataAccessor;
    private readonly MetadataAccessor<Publisher> _publisherMetadataAccessor;
    private readonly MetadataAccessor<Series> _seriesMetadataAccessor;
    private readonly EditEntryViewModel _viewModel;
    private Game? _gameData;

    public EditEntry(MetadataAccessor<Game> gameMetadataAccessor,
        MetadataAccessor<Genre> genreMetadataAccessor,
        MetadataAccessor<Platform> platformMetadataAccessor,
        MetadataAccessor<Developer> developerMetadataAccessor,
        MetadataAccessor<Publisher> publisherMetadataAccessor,
        MetadataAccessor<Series> seriesMetadataAccessor,
        DataGridHelper dataGridHelper,
        IServiceProvider serviceProvider)
    {
        InitializeComponent();

        _gameMetadataAccessor = gameMetadataAccessor;
        _genreMetadataAccessor = genreMetadataAccessor;
        _platformMetadataAccessor = platformMetadataAccessor;
        _developerMetadataAccessor = developerMetadataAccessor;
        _publisherMetadataAccessor = publisherMetadataAccessor;
        _seriesMetadataAccessor = seriesMetadataAccessor;
        _dataGridHelper = dataGridHelper;

        _viewModel = serviceProvider.GetRequiredService<EditEntryViewModel>();
        _viewModel.SetCloseAction(Close);
        DataContext = _viewModel;
        Loaded += (_, _) => SetListBoxPaddingAndMargin();
    }

    public void SetGame(Game? game)
    {
        _gameData = game;
        if (_gameData != null)
        {
            PopulateEditWindow();
        }
    }

    private void PopulateEditWindow()
    {
        if (_gameData == null)
        {
            Logger.Error(LogClass.GameMgrUi, "GameData is null in EditEntry. Returning early...");
            return;
        }

        Title = $"Edit Entry: {_gameData.Title}";

        SetHeader();

        GenreListBox.ItemsSource = GetMetadata(_gameData.Genres, _genreMetadataAccessor);
        PlatformListBox.ItemsSource = GetMetadata(_gameData.Platforms, _platformMetadataAccessor);
        DeveloperListBox.ItemsSource = GetMetadata(_gameData.Developers, _developerMetadataAccessor);
        PublisherListBox.ItemsSource = GetMetadata(_gameData.Publishers, _publisherMetadataAccessor);
        ReleaseDatePicker.SelectedDate = _gameData.ReleaseDateWw;
        SeriesListBox.ItemsSource = GetMetadata(_gameData.Series, _seriesMetadataAccessor);
    }

    private void SetHeader()
    {
        GameTitle.Content = _gameData?.Title;
        GameTitle.FontSize = 24;
        GameTitle.FontWeight = FontWeights.Bold;
        GameTitle.FontFamily = new FontFamily("Century Gothic");
        GameTitle.FontStretch = FontStretches.Expanded;
        GameTitle.HorizontalAlignment = HorizontalAlignment.Center;
        GameTitle.Foreground = new SolidColorBrush(Color.FromArgb(255, 25, 50, 100));
    }

    private static List<string> GetMetadata<T>(IEnumerable<T>? entities, MetadataAccessor<T> accessor) where T : IMetadata
    {
        return entities?
            .Select(e => accessor.GetItemById(e.Id)?.Name ?? "Unknown")
            .ToList() ?? [];
    }

    private void SetListBoxPaddingAndMargin()
    {
        Thickness listBoxThickness = new(2);
        foreach (ListBox listBox in FindVisualChildren<ListBox>(this))
        {
            listBox.Padding = listBoxThickness;
            listBox.Margin = listBoxThickness;

            Style style = new(typeof(ListBoxItem));
            style.Setters.Add(new Setter(PaddingProperty, listBoxThickness));
            style.Setters.Add(new Setter(MarginProperty, listBoxThickness));
            listBox.ItemContainerStyle = style;
        }
    }

    private static IEnumerable<T> FindVisualChildren<T>(DependencyObject depObj) where T : DependencyObject
    {
        for (int i = 0; i < VisualTreeHelper.GetChildrenCount(depObj); i++)
        {
            DependencyObject child = VisualTreeHelper.GetChild(depObj, i);
            if (child is T t)
            {
                yield return t;
            }

            foreach (T childOfChild in FindVisualChildren<T>(child))
            {
                yield return childOfChild;
            }
        }
    }

    private void GameTitle_OnMouseDoubleClick(object sender, MouseButtonEventArgs e)
    {
        _viewModel.RenameTitleCommand.Execute(_gameData);
    }
}
