using GameManager.Core.Data;
using GameManager.Core.Data.MetadataConstructors;
using GameManager.UI.Helpers;
using GameManager.UI.Windows;
using OmniApp.Common.Logging;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace GameManager.UI.ViewModels;

public class MainGameWindowViewModel
{
    private readonly DataGridHelper _dataGridHelper;
    private readonly MetadataAccessor<Game> _metadataAccessor;
    private readonly WindowHelper _windowHelper;

    public MainGameWindowViewModel(DataGridHelper dataGridHelper, MetadataAccessor<Game> metadataAccessor, WindowHelper windowHelper)
    {
        _dataGridHelper = dataGridHelper;
        _metadataAccessor = metadataAccessor;
        _windowHelper = windowHelper;

        AddGameCommand = new RelayCommand<object>(_ => AddGame());
        RefreshDataGridCommand = new RelayCommand<object>(_ => dataGridHelper.RefreshGameDataGridAsync());

        MarkAsPlayedCommand = new RelayCommand<Game>(async game => await MarkAsPlayed(game));
        MarkAsFinishedCommand = new RelayCommand<Game>(async game => await MarkAsFinished(game));
        MarkAsCompletedCommand = new RelayCommand<Game>(async game => await MarkAsCompleted(game));
        EditCommand = new RelayCommand<Game>(Edit);
        DeleteCommand = new RelayCommand<Game>(Delete);
    }

    public ICommand AddGameCommand { get; }
    public ICommand RefreshDataGridCommand { get; }

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

    private void Edit(Game game)
    {
        // _windowHelper.ShowWindow<EditEntry>(game);
    }

    private void Delete(Game game)
    {
        MessageBox.Show("Delete");
    }
}

public class RelayCommand<T>(Action<T> execute, Func<T, bool>? canExecute = null) : ICommand
{
    private readonly Action<T> _execute = execute ?? throw new ArgumentNullException(nameof(execute));

    public event EventHandler? CanExecuteChanged
    {
        add => CommandManager.RequerySuggested += value;
        remove => CommandManager.RequerySuggested -= value;
    }

    public bool CanExecute(object? parameter)
    {
        bool result = canExecute == null
                      || (parameter == null && typeof(T) == typeof(object))
                      || (parameter is T arg && canExecute(arg));
        return result;
    }

    public void Execute(object? parameter)
    {
        switch (parameter)
        {
            case T arg:
                _execute(arg);
                break;
            case null when typeof(T) == typeof(object):
                _execute(default!);
                break;
            default:
                Logger.Error(LogClass.GameMgrUi, $"Parameter type mismatch. Expected {typeof(T).Name}, got {parameter?.GetType().Name ?? "null"}");
                break;
        }
    }
}
