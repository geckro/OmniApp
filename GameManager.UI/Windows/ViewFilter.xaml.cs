using GameManager.UI.Helpers;
using System.Windows.Controls;

namespace GameManager.UI.Windows;

public partial class ViewFilter
{
    public ViewFilter()
    {
        InitializeComponent();
    }

    private static void PopulateStackPanel(StackPanel stackPanel, List<string> headers)
    {
        foreach (string header in headers)
        {
            CheckBox checkBox = new() { Content = header, Name = header };
            stackPanel.Children.Add(checkBox);
        }
    }

    public void PopulateViewStackPanel()
    {
        List<string> headers = DataGridHelper.AutoHeaders;

        Console.WriteLine(string.Join(", ", headers));
        PopulateStackPanel(ViewStackPanel, headers);
    }
}

