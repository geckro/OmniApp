using GameManager.Core.Data;
using GameManager.UI.Helpers;
using System.Windows;
using System.Windows.Controls;

namespace GameManager.UI.Windows;

public partial class MainWindow
{
    private readonly DataGridHelper _dataGridHelper = DataGridHelper.Instance;

    public MainWindow()
    {
        InitializeComponent();

        UpdateGameDataGrid();
        PopulateViewStackPanel();
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
        Console.WriteLine("Running UpdateGameDataGrid");
        _dataGridHelper.UpdateGameDataGrid(GameDataGrid, new GameData());
    }

    private static void PopulateStackPanel(StackPanel stackPanel, IEnumerable<string> headers)
    {
        Console.WriteLine("Running PopulateStackPanel");
        foreach (string header in headers)
        {
            CheckBox checkBox = new() { Content = header, Name = header };
            stackPanel.Children.Add(checkBox);
        }
    }

    private void PopulateViewStackPanel()
    {
        Console.WriteLine("Running PopulateViewStackPanel");
        ICollection<string> headers = _dataGridHelper.AutoHeaders;

        Console.WriteLine($"Headers: {string.Join(", ", headers)}");

        PopulateStackPanel(ViewStackPanel, headers);
    }
}
