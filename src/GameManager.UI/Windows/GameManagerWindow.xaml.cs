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
    private readonly MainGameWindowViewModel _viewModel;
    private readonly MainWindowContextMenuManager _contextMenuManager;
    private readonly GameFilterHelper _filterHelper;

    public GameManagerWindow(IServiceProvider sp)
    {
        InitializeComponent();

        _viewModel = sp.GetRequiredService<MainGameWindowViewModel>();

        DataContext = _viewModel;

        _contextMenuManager = new MainWindowContextMenuManager(this, _viewModel);
        _filterHelper = new GameFilterHelper(
            this,
            sp.GetRequiredService<MetadataAccessor<Game>>(),
            sp.GetRequiredService<MetadataAccessor<Developer>>(),
            sp.GetRequiredService<MetadataAccessor<Publisher>>(),
            sp.GetRequiredService<MetadataAccessor<Genre>>(),
            sp.GetRequiredService<MetadataAccessor<Platform>>(),
            sp.GetRequiredService<MetadataAccessor<Series>>()
            );

        Loaded += OnWindowLoaded;
    }

    private async void OnWindowLoaded(object sender, RoutedEventArgs e)
    {
        try
        {
            Logger.Info(LogClass.GameMgrUi, "Initializing GameManager Window");
            await InitializeAsync();
        }
        catch (Exception ex)
        {
            Logger.Error(LogClass.GameMgrUi, $"Error initializing GameManager Window: {ex.Message}");
        }
    }

    private async Task InitializeAsync()
    {
        await _viewModel.InitializeAsync(GameDataGrid);
        _contextMenuManager.PopulateDataGridContextMenu();
        _filterHelper.PopulateFilterMenus();
        RegisterKeyboardShortcuts();
    }

    private void RegisterKeyboardShortcuts()
    {
        InputBindings.Add(new KeyBinding(_viewModel.AddGameCommand, new KeyGesture(Key.N, ModifierKeys.Control)));
    }

    public void SetFilter(GameFilterHelper gameFilterHelper)
    {
        _viewModel.SetFilter(gameFilterHelper);
    }
}
