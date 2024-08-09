using GameManager.Core.Data;
using GameManager.Core.Data.MetadataConstructors;
using GameManager.UI.Helpers;
using GameManager.UI.Managers;
using GameManager.UI.ViewModels;
using Microsoft.Extensions.DependencyInjection;
using OmniApp.Common.Logging;
using System.Windows;
using System.Windows.Input;

namespace GameManager.UI.Windows;

public partial class GameManagerWindow
{
    private readonly GameManagerContextMenuManager _contextMenuManager;
    private readonly FilterHelper _filterHelper;
    private readonly GameMgrWindowViewModel _viewModel;

    public GameManagerWindow(IServiceProvider sp)
    {
        InitializeComponent();

        _viewModel = sp.GetRequiredService<GameMgrWindowViewModel>();

        DataContext = _viewModel;

        _contextMenuManager = new GameManagerContextMenuManager(this, _viewModel);
        _filterHelper = new FilterHelper(this, sp.GetRequiredService<MetadataAccessor<Game>>(),
                sp.GetRequiredService<MetadataAccessor<Developer>>(),
                sp.GetRequiredService<MetadataAccessor<Publisher>>(), sp.GetRequiredService<MetadataAccessor<Genre>>(),
                sp.GetRequiredService<MetadataAccessor<Platform>>(), sp.GetRequiredService<MetadataAccessor<Series>>());

        Loaded += OnWindowLoaded;
    }

    private async void OnWindowLoaded(object sender, RoutedEventArgs e)
    {
        try
        {
            Logger.Info(LogClass.GameMgrUiWindows, "Initializing GameManager Window");
            await InitializeAsync();
        }
        catch (Exception ex)
        {
            Logger.Error(LogClass.GameMgrUiWindows, $"Error initializing GameManager Window: {ex.Message}");
        }
    }

    private async Task InitializeAsync()
    {
        await _viewModel.InitializeAsync(GameDataGrid);
        _contextMenuManager.PopulateContextMenus();
        _filterHelper.PopulateFilterMenus();
        RegisterKeyboardShortcuts();
    }

    private void RegisterKeyboardShortcuts()
    {
        InputBindings.Add(new KeyBinding(_viewModel.AddGameCommand, new KeyGesture(Key.N, ModifierKeys.Control)));
        InputBindings.Add(
                new KeyBinding(_viewModel.RefreshDataGridCommand, new KeyGesture(Key.R, ModifierKeys.Control)));
        InputBindings.Add(new KeyBinding(_viewModel.RefreshDataGridCommand, new KeyGesture(Key.F5)));
        InputBindings.Add(new KeyBinding(_viewModel.PickRandomGameCommand,
                new KeyGesture(Key.T, ModifierKeys.Control)));
        InputBindings.Add(new KeyBinding(_viewModel.OpenPreferencesCommand,
                new KeyGesture(Key.OemPeriod, ModifierKeys.Control)));
    }

    public void SetFilter(FilterHelper filterHelper)
    {
        _viewModel.SetFilter(filterHelper);
    }
}
