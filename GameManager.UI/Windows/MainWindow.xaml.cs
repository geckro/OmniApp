using GameManager.Core.Data;
using GameManager.UI.Helpers;
using System.Data;
using System.Windows;
using System.Windows.Controls;

namespace GameManager.UI.Windows;

public partial class MainWindow
{
    public MainWindow()
    {
        InitializeComponent();

        Menus menuHelper = new();
        menuHelper.InitializePlatformsMenu(PlatformFilter, FilterDataGrid);
        menuHelper.InitializeGenresMenu(GenreFilter, FilterDataGrid);

        UpdateGameDataGrid();

        ViewFilter viewFilter = new();
        WindowHelper.LoadContent(ViewFilterArea, viewFilter);
        viewFilter.PopulateViewStackPanel();
    }

    private void AddButton_Click(object sender, RoutedEventArgs e)
    {
        WindowHelper.ShowWindow(new Add());
    }

    private void ImportButton_Click(object sender, RoutedEventArgs e)
    {
        WindowHelper.ShowWindow(new Import());
    }

    private void FilterDataGrid(object sender, RoutedEventArgs e)
    {
    }

    public void UpdateGameDataGrid()
    {
        DataGridHelper.UpdateGameDataGrid(GameDataGrid, new GameData());
    }
}
