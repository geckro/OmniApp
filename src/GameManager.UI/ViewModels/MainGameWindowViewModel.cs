using GameManager.Core.Data;
using GameManager.Core.Data.MetadataConstructors;
using GameManager.UI.Helpers;
using GameManager.UI.Managers;
using GameManager.UI.Windows;
using OmniApp.Common.Logging;
using OmniApp.UiCommon;
using OmniApp.UiCommon.Helpers;
using System.Reflection;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace GameManager.UI.ViewModels;

public class MainGameWindowViewModel
{
    private readonly GameTableHelper _gameTableHelper;
    private readonly MetadataAccessor<Game> _metadataAccessor;
    private readonly WindowHelper _windowHelper;
    private readonly Refresh _refresh;

    public MainGameWindowViewModel(GameTableHelper gameTableHelper, MetadataAccessor<Game> metadataAccessor, WindowHelper windowHelper)
    {
        _gameTableHelper = gameTableHelper;
        _metadataAccessor = metadataAccessor;
        _windowHelper = windowHelper;

        _refresh = new Refresh(_gameTableHelper);

        AddGameCommand = new RelayCommand<object>(_ => AddGame());
        RefreshDataGridCommand = new RelayCommand<object>(async _ => await _refresh.RefreshControls(RefreshOptions.DataGrid));
        OpenGamesJsonCommand = new RelayCommand<object>(_ => FileHelper.OpenFileWithDefaultProgram(@"Data\GameMgr\games.json"));
        OpenPreferencesCommand = new RelayCommand<object>(_ => Preferences());
        PickRandomGameCommand = new RelayCommand<object>(_ => PickRandomGame());

        MarkAsPlayedCommand = new RelayCommand<Game>(async game => await MarkAsPlayed(game));
        MarkAsFinishedCommand = new RelayCommand<Game>(async game => await MarkAsFinished(game));
        MarkAsCompletedCommand = new RelayCommand<Game>(async game => await MarkAsCompleted(game));
        EditCommand = new RelayCommand<Game>(Edit);
        DeleteCommand = new RelayCommand<Game>(async game => await Delete(game));
    }

    public ICommand AddGameCommand { get; }
    public ICommand RefreshDataGridCommand { get; }
    public ICommand OpenGamesJsonCommand { get; }
    public ICommand OpenPreferencesCommand { get; }
    public ICommand PickRandomGameCommand { get; }

    public ICommand MarkAsPlayedCommand { get; }
    public ICommand MarkAsFinishedCommand { get; }
    public ICommand MarkAsCompletedCommand { get; }
    public ICommand EditCommand { get; }
    public ICommand DeleteCommand { get; }

    public async Task InitializeAsync(DataGrid gameDataGrid)
    {
        await _gameTableHelper.PopulateGameTableAsync(gameDataGrid);
    }

    private void AddGame()
    {
        AddGame addGameWindow = _windowHelper.ShowWindow<AddGame>();

        addGameWindow.GameAdded += async (_, _) =>
        {
            await _refresh.RefreshControls(RefreshOptions.DataGrid);
        };
    }

    private void PickRandomGame()
    {
        ICollection<string> visibleGames = _gameTableHelper.GetAllVisibleDataGridRowTitle();

        if (visibleGames.Count == 0)
        {
            Logger.Error(LogClass.GameMgrUi, "There are no games in the games table.");
            return;
        }
        List<string> visibleGamesList = visibleGames.ToList();

        Random random = new();
        string randomGame = visibleGamesList[random.Next(visibleGames.Count)];

        MessageBox.Show(randomGame);
    }

    private void Preferences()
    {
        _windowHelper.ShowWindow<GameManagerPreferences>();
    }

    private async Task MarkAsPlayed(Game game)
    {
        await MarkAsTrueFalse(game, "HasPlayed");
    }

    private async Task MarkAsFinished(Game game)
    {
        await MarkAsTrueFalse(game, "HasFinished");
    }

    private async Task MarkAsCompleted(Game game)
    {
        await MarkAsTrueFalse(game, "HasCompleted");
    }

    private async Task MarkAsTrueFalse(Game game, string key)
    {
        PropertyInfo property = game.GetType().GetProperty(key) ?? throw new InvalidOperationException();
        bool trueOrFalse = (bool?)property.GetValue(game) switch
        {
            true => false,
            false or null => true
        };
        property.SetValue(game, trueOrFalse);

        _metadataAccessor.UpdateItemAndSave(game.Id, key, trueOrFalse);
        await _refresh.RefreshControls(RefreshOptions.DataGrid);
    }

    private void Edit(Game? game)
    {
        if (game == null)
        {
            Logger.Warning(LogClass.GameMgrUi, "Attempted to edit a null game");
            return;
        }

        _windowHelper.ShowWindow<EditEntry>(window =>
        {
            if (window is EditEntry editEntry)
            {
                editEntry.SetGame(game);
            }
            else
            {
                Logger.Error(LogClass.GameMgrUi, $"Expected EditEntry window, got {window.GetType().Name}");
            }
        });
    }

    private async Task Delete(Game game)
    {
        _metadataAccessor.RemoveItemById(game.Id);
        await _refresh.RefreshControls(RefreshOptions.DataGrid);
    }
}
