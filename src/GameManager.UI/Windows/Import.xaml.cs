using System.Windows.Controls;

namespace GameManager.UI.Windows;

public partial class Import
{
    public Import()
    {
        InitializeComponent();

        ImportGameDataGrid.ColumnWidth = new DataGridLength(100);
        ImportGameDataGrid.MinColumnWidth = 50;
    }
}
