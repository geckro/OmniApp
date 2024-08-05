﻿using GameManager.Core.Data;
using GameManager.Core.Data.MetadataConstructors;
using GameManager.UI.ViewModels;
using Microsoft.Extensions.DependencyInjection;
using System.Windows;
using System.Windows.Controls;

namespace GameManager.UI.Windows;

public partial class PickGameDialog
{
    private readonly MetadataAccessor<Game> _metadataAccessor;
    private readonly PickGameViewModel _viewModel;

    public PickGameDialog(IServiceProvider serviceProvider, MetadataAccessor<Game> metadataAccessor)
    {
        _metadataAccessor = metadataAccessor;
        InitializeComponent();

        _viewModel = serviceProvider.GetRequiredService<PickGameViewModel>();
        DataContext = _viewModel;
        _viewModel.SetCloseAction(Close);
        PopulateGameListBox();

        GameListBox.SelectionChanged += GameListBox_SelectionChanged;
    }

    private void PopulateGameListBox()
    {
        ICollection<Game> metadata = _metadataAccessor.LoadMetadataCollection();

        foreach (Game game in metadata)
        {
            string title = game.Title;
            if (game.ReleaseDateWw != null)
            {
                title = $"{title} ({game.ReleaseDateWw.Value.Year})";
            }

            ListBoxItem listBoxItem = new() { Tag = game.Id, Content = title, Margin = new Thickness(0), Padding = new Thickness(0) };

            GameListBox.Items.Add(listBoxItem);
        }
    }

    private void GameListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        _viewModel.ListBoxSelectedItems.Clear();
        foreach (ListBoxItem item in GameListBox.SelectedItems)
        {
            _viewModel.ListBoxSelectedItems.Add(item);
        }
    }
}