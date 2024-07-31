using GameManager.Core.Data;
using GameManager.Core.Data.MetadataConstructors;
using GameManager.UI.Helpers;
using GameManager.UI.Windows;
using OmniApp.Common.Logging;
using OmniApp.UiCommon;
using OmniApp.UiCommon.Helpers;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace GameManager.UI.ViewModels;

public class MainGameWindowViewModel
{
    private readonly DataGridHelper _dataGridHelper;
    private readonly MetadataAccessor<Game> _metadataAccessor;
    private readonly WindowHelper _windowHelper;

    public MainGameWindowViewModel(DataGridHelper dataGridHelper, MetadataAccessor<Game> metadataAccessor, FileHelper fileHelper, WindowHelper windowHelper)
    {
        _dataGridHelper = dataGridHelper;
        _metadataAccessor = metadataAccessor;
        _windowHelper = windowHelper;

        AddGameCommand = new RelayCommand<object>(_ => AddGame());
        RefreshDataGridCommand = new RelayCommand<object>(_ => dataGridHelper.RefreshGameDataGridAsync());
        OpenGamesJsonCommand = new RelayCommand<object>(_ => FileHelper.OpenFileWithDefaultProgram(@"Data\GameMgr\games.json"));
        OpenPreferencesCommand = new RelayCommand<object>(_ => Preferences());

        MarkAsPlayedCommand = new RelayCommand<Game>(async game => await MarkAsPlayed(game));
        MarkAsFinishedCommand = new RelayCommand<Game>(async game => await MarkAsFinished(game));
        MarkAsCompletedCommand = new RelayCommand<Game>(async game => await MarkAsCompleted(game));
        EditCommand = new RelayCommand<Game>(Edit);
        DeleteCommand = new RelayCommand<Game>(Delete);
    }

    public ICommand AddGameCommand { get; }
    public ICommand RefreshDataGridCommand { get; }
    public ICommand OpenGamesJsonCommand { get; }
    public ICommand OpenPreferencesCommand { get; }

    public ICommand MarkAsPlayedCommand { get; }
    public ICommand MarkAsFinishedCommand { get; }
    public ICommand MarkAsCompletedCommand { get; }
    public ICommand EditCommand { get; }
    public ICommand DeleteCommand { get; }

    public async Task InitializeAsync(DataGrid gameDataGrid)
    {
        await _dataGridHelper.PopulateGameDataGridAsync(gameDataGrid);
    }

    private void AddGame()
    {
        AddGame addGameWindow = _windowHelper.ShowWindow<AddGame>();

        addGameWindow.GameAdded += async (_, _) =>
        {
            await _dataGridHelper.RefreshGameDataGridAsync();
        };
    }

    private void Preferences()
    {
        _windowHelper.ShowWindow<Preferences>();
    }

    private async Task MarkAsPlayed(Game game)
    {
        game.HasPlayed = true;
        _metadataAccessor.UpdateItemAndSave(game.Id, "HasPlayed", true);
        await _dataGridHelper.RefreshGameDataGridAsync();
    }

    private async Task MarkAsFinished(Game game)
    {
        game.HasFinished = true;
        _metadataAccessor.UpdateItemAndSave(game.Id, "HasFinished", true);
        await _dataGridHelper.RefreshGameDataGridAsync();
    }

    private async Task MarkAsCompleted(Game game)
    {
        game.HasCompleted = true;
        _metadataAccessor.UpdateItemAndSave(game.Id, "HasCompleted", true);
        await _dataGridHelper.RefreshGameDataGridAsync();
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

    private void Delete(Game game)
    {
        MessageBox.Show("Delete");
    }
}
