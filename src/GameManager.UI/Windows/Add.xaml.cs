using GameManager.UI.Helpers;
using System.Windows;

namespace GameManager.UI.Windows;

/// <summary>
///     Logic for Add.xaml
/// </summary>
public partial class Add
{
    /// <summary>
    ///     Initializes a new instance of the Add class.
    /// </summary>
    public Add()
    {
        InitializeComponent();
    }

    /// <summary>
    ///     Loads the AddGame ContentControl
    /// </summary>
    /// <param name="sender">The source of the event</param>
    /// <param name="e">RoutedEventArgs event data</param>
    private void AddGame_Click(object sender, RoutedEventArgs e)
    {
        WindowHelper.LoadContent(AddContentArea, new AddGame());
    }
}
