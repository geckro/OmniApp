using System.Windows.Controls;

namespace GameManager.UI.Windows;

/// <summary>
///     Logic for Import.xaml
/// </summary>
public partial class Import
{
    /// <summary>
    ///     Initializes a new instance of the Import class.
    /// </summary>
    public Import()
    {
        InitializeComponent();

        ImportGameDataGrid.ColumnWidth = new DataGridLength(100);
        ImportGameDataGrid.MinColumnWidth = 50;
    }
}
