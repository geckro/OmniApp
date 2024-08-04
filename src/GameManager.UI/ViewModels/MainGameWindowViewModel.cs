using GameManager.Core.Data;
using GameManager.Core.Data.MetadataConstructors;
using GameManager.UI.Helpers;
using GameManager.UI.Managers;
using GameManager.UI.Windows;
using OmniApp.Common.Logging;
using OmniApp.UI.Common;
using OmniApp.UI.Common.Helpers;
using System.Reflection;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace GameManager.UI.ViewModels;

public class MainGameWindowViewModel : ViewModelBase
{
    private readonly GameTableHelper _gameTableHelper;
    private readonly MetadataAccessor<Game> _metadataAccessor;
    private readonly WindowHelper _windowHelper;
    private readonly Refresh _refresh;
    private GameFilterHelper _filterHelper = null!;

    public MainGameWindowViewModel(GameTableHelper gameTableHelper, MetadataAccessor<Game> metadataAccessor, WindowHelper windowHelper)
    {
        _gameTableHelper = gameTableHelper;
        _metadataAccessor = metadataAccessor;
        _windowHelper = windowHelper;

        _refresh = new Refresh(_gameTableHelper);

        InitializeCommands();
    }

    public ICommand AddGameCommand { get; private set; } = null!;
    public ICommand RefreshDataGridCommand { get; private set; } = null!;
    public ICommand OpenGamesJsonCommand { get; private set; } = null!;
    public ICommand OpenPreferencesCommand { get; private set; } = null!;
    public ICommand PickRandomGameCommand { get; private set; } = null!;
    public ICommand MarkAsPlayedCommand { get; private set; } = null!;
    public ICommand MarkAsFinishedCommand { get; private set; } = null!;
    public ICommand MarkAsCompletedCommand { get; private set; } = null!;
    public ICommand EditCommand { get; private set; } = null!;
    public ICommand EditTagsCommand { get; private set; } = null!;
    public ICommand DeleteCommand { get; private set; } = null!;
    public ICommand FilterGameTableCommand { get; private set; } = null!;

    public void SetFilter(GameFilterHelper filterHelper)
    {
        _filterHelper = filterHelper;
        _filterHelper.SetViewModel(this);
    }

    private void InitializeCommands()
    {
        AddGameCommand = new RelayCommand<object>(_ => AddGame());
        RefreshDataGridCommand = new RelayCommand<object>(async _ => await _refresh.RefreshControls(RefreshOptions.DataGrid));
        OpenGamesJsonCommand = new RelayCommand<object>(_ => FileHelper.OpenFileWithDefaultProgram(@"Data\GameMgr\games.json"));
        OpenPreferencesCommand = new RelayCommand<object>(_ => Preferences());
        PickRandomGameCommand = new RelayCommand<object>(_ => PickRandomGame());

        MarkAsPlayedCommand = new RelayCommand<Game>(async game => await MarkAsTrueFalse(game, "HasPlayed"));
        MarkAsFinishedCommand = new RelayCommand<Game>(async game => await MarkAsTrueFalse(game, "HasFinished"));
        MarkAsCompletedCommand = new RelayCommand<Game>(async game => await MarkAsTrueFalse(game, "HasCompleted"));
        EditCommand = new RelayCommand<Game>(Edit);
        EditTagsCommand = new RelayCommand<Game>(EditTags);
        DeleteCommand = new RelayCommand<Game>(async game => await Delete(game));

        FilterGameTableCommand = new RelayCommand<object>(async _ => await _gameTableHelper.FilterGameTableAsync(_filterHelper));
    }

    public async Task InitializeAsync(DataGrid gameDataGrid)
    {
        await _gameTableHelper.PopulateGameTableAsync(gameDataGrid);
    }

    private void AddGame()
    {
        AddGame addGameWindow = _windowHelper.ShowWindow<AddGame>();
        addGameWindow.GameAdded += async (_, _) => await _refresh.RefreshControls(RefreshOptions.DataGrid);
    }

    private void PickRandomGame()
    {
        List<string> visibleGames = _gameTableHelper.GetAllVisibleDataGridRowTitle().ToList();

        if (visibleGames.Count == 0)
        {
            Logger.Error(LogClass.GameMgrUi, "There are no games in the games table.");
            MessageBox.Show("There are no visible games in the table to choose from.", "Game Picker", MessageBoxButton.OK, MessageBoxImage.Warning);
            return;
        }

        Random random = new();
        string randomGame = visibleGames[random.Next(visibleGames.Count)];

        MessageBox.Show($"Random game picked: {randomGame}", "Random Game", MessageBoxButton.OK, MessageBoxImage.Information);
    }

    private void Preferences()
    {
        _windowHelper.ShowWindow<GameManagerPreferences>();
    }

    private async Task MarkAsTrueFalse(Game game, string key)
    {
        PropertyInfo property = game.GetType().GetProperty(key) ?? throw new InvalidOperationException();
        bool? currentValue = (bool?)property.GetValue(game);
        bool newValue = !currentValue ?? true;
        property.SetValue(game, newValue);

        _metadataAccessor.UpdateItemAndSave(game.Id, key, newValue);
        await _refresh.RefreshControls(RefreshOptions.DataGrid);
    }

    private void Edit(Game? game)
    {
        if (game == null)
        {
            Logger.Warning(LogClass.GameMgrUi, "Attempted to edit a null game in Edit");
            return;
        }

        _windowHelper.ShowWindow<EditEntry>(window =>
        {
            if (window is EditEntry)
            {
                window.SetGame(game);
            }
            else
            {
                Logger.Error(LogClass.GameMgrUi, $"Expected EditEntry window, got {window.GetType().Name}");
            }
        });
    }

    private void EditTags(Game? game)
    {
        if (game == null)
        {
            Logger.Warning(LogClass.GameMgrUi, "Attempted to edit a null game in EditTags");
            return;
        }

        _windowHelper.ShowWindow<EditEntryTags>(window =>
        {
            if (window is EditEntryTags)
            {
                window.SetGame(game);
            }
            else
            {
                Logger.Error(LogClass.GameMgrUi, $"Expected EditEntryTags window, got {window.GetType().Name}");
            }
        });
    }

    private async Task Delete(Game game)
    {
        MessageBoxResult result = MessageBox.Show($"Are you sure you want to delete '{game.Title}'?", "Confirm Delete", MessageBoxButton.YesNo, MessageBoxImage.Warning);
        if (result == MessageBoxResult.Yes)
        {
            _metadataAccessor.RemoveItem(game);
            await _refresh.RefreshControls(RefreshOptions.DataGrid);
        }
    }
}
