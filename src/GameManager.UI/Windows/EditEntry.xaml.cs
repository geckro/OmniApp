using GameManager.Core.Data;
using GameManager.Core.Data.MetadataConstructors;
using GameManager.UI.Helpers;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace GameManager.UI.Windows;

public partial class EditEntry
{
    private readonly Game _gameData;
    private readonly IMetadataAccessor<Game> _gameMetadataAccessor;
    private readonly DataGridHelper _dataGridHelper;

    public EditEntry(Game gameData, IMetadataAccessor<Game> gameMetadataAccessor, DataGridHelper dataGridHelper)
    {
        InitializeComponent();

        _gameData = gameData;
        _gameMetadataAccessor = gameMetadataAccessor;
        _dataGridHelper = dataGridHelper;

        PopulateEditWindow();

        AddCheckBoxEventHandlers();

        Loaded += (_, _) => SetListBoxPaddingAndMargin();
    }

    private void PopulateEditWindow()
    {
        Title = $"Edit Entry: {_gameData.Title}";

        GameTitle.Content = _gameData.Title;
        GameTitle.FontSize = 24;
        GameTitle.FontWeight = FontWeights.Bold;
        GameTitle.FontFamily = new FontFamily("Century Gothic");
        GameTitle.FontStretch = FontStretches.Expanded;
        GameTitle.HorizontalAlignment = HorizontalAlignment.Center;
        GameTitle.Foreground = new SolidColorBrush(Color.FromArgb(255, 25, 50, 100));

        GenreListBox.ItemsSource = _gameData.Genres?.Select(g => g.Name).ToList() ?? [];
        PlatformListBox.ItemsSource = _gameData.Platforms?.Select(p => p.Name).ToList() ?? [];
        DeveloperListBox.ItemsSource = _gameData.Developers?.Select(d => d.Name).ToList() ?? [];
        PublisherListBox.ItemsSource = _gameData.Publishers?.Select(p => p.Name).ToList() ?? [];

        ReleaseDatePicker.SelectedDate = _gameData.ReleaseDateWw;
        OtherTitlesListBox.ItemsSource = _gameData.OtherTitles ?? new List<string>();
        SeriesListBox.ItemsSource = _gameData.Series?.Select(s => s.Name).ToList() ?? [];

        IsRemakeCheckBox.IsChecked = _gameData.IsRemake;
        IsRemasterCheckBox.IsChecked = _gameData.IsRemaster;
        IsPortCheckBox.IsChecked = _gameData.IsPort;
        IsSequelCheckBox.IsChecked = _gameData.IsSequel;
        IsPrequelCheckBox.IsChecked = _gameData.IsPrequel;
        IsDlcCheckBox.IsChecked = _gameData.IsDlc;
        IsModCheckBox.IsChecked = _gameData.IsMod;
        IsHackCheckBox.IsChecked = _gameData.IsHack;

        WebsiteTextBox.Text = _gameData.Website?.ToString() ?? string.Empty;
    }

    private void AddCheckBoxEventHandlers()
    {
        IsRemakeCheckBox.Click += (_, _) => UpdateGameCheckBoxProperty("IsRemake", IsRemakeCheckBox.IsChecked ?? false);
        IsRemasterCheckBox.Click += (_, _) => UpdateGameCheckBoxProperty("IsRemaster", IsRemasterCheckBox.IsChecked ?? false);
        IsPortCheckBox.Click += (_, _) => UpdateGameCheckBoxProperty("IsPort", IsPortCheckBox.IsChecked ?? false);
        IsSequelCheckBox.Click += (_, _) => UpdateGameCheckBoxProperty("IsSequel", IsSequelCheckBox.IsChecked ?? false);
        IsPrequelCheckBox.Click += (_, _) => UpdateGameCheckBoxProperty("IsPrequel", IsPrequelCheckBox.IsChecked ?? false);
        IsDlcCheckBox.Click += (_, _) => UpdateGameCheckBoxProperty("IsDLC", IsDlcCheckBox.IsChecked ?? false);
        IsModCheckBox.Click += (_, _) => UpdateGameCheckBoxProperty("IsMod", IsModCheckBox.IsChecked ?? false);
        IsHackCheckBox.Click += (_, _) => UpdateGameCheckBoxProperty("IsHack", IsHackCheckBox.IsChecked ?? false);
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
            DependencyObject? child = VisualTreeHelper.GetChild(depObj, i);
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

    private void UpdateGameCheckBoxProperty(string checkBoxType, bool isChecked)
    {
        typeof(Game).GetProperty(checkBoxType)?.SetValue(_gameData, isChecked);
        _gameMetadataAccessor.UpdateItemAndSave(_gameData.Id, checkBoxType, isChecked);
        _dataGridHelper.RefreshGameDataGrid(_gameMetadataAccessor);
    }
}

