using GameManager.UI.Managers;
using GameManager.UI.ViewModels;
using Microsoft.Extensions.DependencyInjection;
using OmniApp.Common.Logging;
using System.Windows;
using System.Windows.Controls.Primitives;
using System.Windows.Input;

namespace GameManager.UI.Windows;

public partial class GameManagerWindow
{
    private readonly MainGameWindowViewModel _viewModel;

    public GameManagerWindow(IServiceProvider sp)
    {
        Logger.Info(LogClass.GameMgrUi, "Starting GameManagerWindow");

        _viewModel = sp.GetRequiredService<MainGameWindowViewModel>();

        InitializeComponent();
        DataContext = _viewModel;

        MainWindowContextMenuManager contextMenuManager = new(this, _viewModel);
        contextMenuManager.PopulateDataGridContextMenu();

        InitializeAsync();
    }

    private async void InitializeAsync()
    {
        try
        {
            Logger.Info(LogClass.GameMgrUi, "Initializing GameManagerWindow");
            await _viewModel.InitializeAsync(GameDataGrid);
            RegisterKeyboardShortcuts();
        }
        catch (Exception ex)
        {
            Logger.Error(LogClass.GameMgrUi, $"Error initializing GameManagerWindow: {ex.Message}");
        }
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
