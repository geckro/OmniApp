﻿using GameManager.Core.Data;
using System.Windows;
using System.Windows.Controls;

namespace GameManager.UI.Helpers;

public class Menus
{
    private static void InitializeMenu<T>(
        MenuItem menu,
        IEnumerable<T> metadataList,
        RoutedEventHandler? clickHandler = null,
        bool menuStaysOpen = false
    )
        where T : IMetadata
    {
        menu.Items.Clear();
        foreach (T metadata in metadataList)
        {
            MenuItem menuItem = new() { Header = metadata.Name, Margin = new Thickness(0), IsCheckable = true };

            if (clickHandler != null)
            {
                menuItem.Click += clickHandler;
            }

            if (menuStaysOpen)
            {
                menuItem.StaysOpenOnClick = true;
            }

            menu.Items.Add(menuItem);
        }

        if (menuStaysOpen)
        {
            menu.StaysOpenOnClick = true;
        }
    }

    public void InitializePlatformsMenu(MenuItem menu, RoutedEventHandler? clickHandler = null,
        bool menuStaysOpen = false)
    {
        InitializeMenu(menu, new DataManagerFactory().CreateData<Platform>().ReadFromJson(), clickHandler, menuStaysOpen);
    }

    public void InitializeGenresMenu(MenuItem menu, RoutedEventHandler? clickHandler = null, bool menuStaysOpen = false)
    {
        InitializeMenu(menu, new DataManagerFactory().CreateData<Genre>().ReadFromJson(), clickHandler, menuStaysOpen);
    }

    public void InitializeDevelopersMenu(MenuItem menu, RoutedEventHandler? clickHandler = null,
        bool menuStaysOpen = false)
    {
        InitializeMenu(menu, new DataManagerFactory().CreateData<Developer>().ReadFromJson(), clickHandler, menuStaysOpen);
    }

    public void InitializePublishersMenu(MenuItem menu, RoutedEventHandler? clickHandler = null,
        bool menuStaysOpen = false)
    {
        InitializeMenu(menu, new DataManagerFactory().CreateData<Publisher>().ReadFromJson(), clickHandler, menuStaysOpen);
    }

    public void InitializeSeriesMenu(MenuItem menu, RoutedEventHandler? clickHandler = null, bool menuStaysOpen = false)
    {
        InitializeMenu(menu, new DataManagerFactory().CreateData<Series>().ReadFromJson(), clickHandler, menuStaysOpen);
    }
}
