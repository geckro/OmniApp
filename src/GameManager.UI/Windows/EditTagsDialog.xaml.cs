using GameManager.Core.Data;
using GameManager.Core.Data.MetadataConstructors;
using GameManager.UI.ViewModels;
using Microsoft.Extensions.DependencyInjection;
using OmniApp.Common.Logging;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace GameManager.UI.Windows;

public partial class EditTagsDialog
{
    private readonly EditTagsViewModel _viewModel;
    private Game? _gameData;

    public EditTagsDialog(IServiceProvider serviceProvider)
    {
        InitializeComponent();

        _viewModel = serviceProvider.GetRequiredService<EditTagsViewModel>();
        _viewModel.SetCloseAction(Close);
        DataContext = _viewModel;
        Loaded += (_, _) => SetListBoxPaddingAndMargin();
    }

    public void SetGame(Game? game)
    {
        _gameData = game;
        _viewModel.GameEntry = game;
        if (_gameData != null)
        {
            PopulateEditTagsWindow();
        }
    }

    private void PopulateEditTagsWindow()
    {
        if (_gameData == null)
        {
            Logger.Error(LogClass.GameMgrUi, "GameData is null in EditTagsDialog. Returning early...");
            return;
        }

        Title = $"Edit Entry Tags: {_gameData.Title}";

        SetHeader();

        Dictionary<Label, TextBlock>? tags = EditTagsViewModel.LoadTags(_gameData);

        if (tags != null)
        {
            foreach (KeyValuePair<Label, TextBlock> tagPair in tags)
            {
                StackPanel tagStackPanel = new()
                {
                    Orientation = Orientation.Horizontal
                };

                tagStackPanel.Children.Add(tagPair.Key);
                tagStackPanel.Children.Add(tagPair.Value);

                TagStackPanel.Children.Add(tagStackPanel);
            }
        }
        else
        {
            TagStackPanel.Children.Add(new TextBlock
            {
                Text = "No tags available for this game."
            });
        }
    }

    private void SetHeader()
    {
        GameTitle.Content = $"{_gameData?.Title} - Tags";
        GameTitle.FontSize = 24;
        GameTitle.FontWeight = FontWeights.Bold;
        GameTitle.FontFamily = new FontFamily("Century Gothic");
        GameTitle.FontStretch = FontStretches.Expanded;
        GameTitle.HorizontalAlignment = HorizontalAlignment.Center;
        GameTitle.Foreground = new SolidColorBrush(Color.FromArgb(255, 50, 25, 100));
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
