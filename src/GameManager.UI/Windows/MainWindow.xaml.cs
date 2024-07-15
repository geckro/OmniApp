using GameManager.Core.Data;
using GameManager.Core.Data.MetadataConstructors;
using GameManager.UI.Helpers;
using GameManager.UI.Managers;
using OmniApp.Common.Logging;
using System.Windows;
using System.Windows.Controls.Primitives;
using System.Windows.Input;

namespace GameManager.UI.Windows;

/// <summary>
///     Logic for MainWindow.xaml
/// </summary>
public partial class MainWindow
{
    private readonly DataGridHelper _dataGridHelper = new();
    private readonly IMetadataAccessor<Game> _metadataAccessor;
    private readonly IMetadataPersistence _metadataPersistence = new MetadataPersistence();

    /// <summary>
    ///     Initializes a new instance of the MainWindow class.
    /// </summary>
    public MainWindow()
    {
        Logger.Info(LogClass.GameMgrUi, "Starting MainWindow");

        MetadataAccessorFactory metadataAccessorFactory = new(_metadataPersistence);
        _metadataAccessor = metadataAccessorFactory.CreateMetadataAccessor<Game>();

        InitializeComponent();
        _dataGridHelper.PopulateGameDataGrid(GameDataGrid, _metadataAccessor);
        new MainWindowContextMenuManager(this, _dataGridHelper, _metadataAccessor).PopulateDataGridContextMenu();
        RegisterKeyboardShortcuts();
    }

    /// <summary>
    ///     Handles click event of the Add... Button
    /// </summary>
    /// <param name="sender">The source of the event</param>
    /// <param name="e">RoutedEventArgs event data</param>
    private void AddButton_Click(object sender, RoutedEventArgs e)
    {
        WindowHelper.ShowWindow(new AddGame());
    }

    /// <summary>
    ///     Handles click event of the Import... Button
    /// </summary>
    /// <param name="sender">The source of the event</param>
    /// <param name="e">RoutedEventArgs event data</param>
    private void ImportButton_Click(object sender, RoutedEventArgs e)
    {
        WindowHelper.ShowWindow(new Import());
    }

    private void RegisterKeyboardShortcuts()
    {
        RoutedCommand command = new();
        command.InputGestures.Add(new KeyGesture(Key.N, ModifierKeys.Control));
        CommandBinding binding = new(command, (_, _) => AddButton.RaiseEvent(new RoutedEventArgs(ButtonBase.ClickEvent)));
        CommandBindings.Add(binding);
        InputBindings.Add(new InputBinding(command, new KeyGesture(Key.N, ModifierKeys.Control)));
    }
}
