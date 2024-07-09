﻿using GameManager.Core.Data;
using GameManager.UI.Helpers;
using System.Windows;

namespace GameManager.UI.Windows;

public partial class AddGenre
{
    public AddGenre()
    {
        InitializeComponent();
        ListBoxHelper listBoxHelper = new();
        listBoxHelper.UpdateGenreListBox(GenreListBox);
    }

    private void AddNewGenre_Click(object sender, RoutedEventArgs e)
    {
        MetadataHelper.AddNewMetadata<Genre, GenreData>(GenreBox, new GenreData());
    }
}
