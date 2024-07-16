using GameManager.Core.Data;
using GameManager.Core.Data.MetadataConstructors;
using GameManager.UI.Helpers;
using GameManager.UI.Windows;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace GameManager.UI.ViewModels;

public class MainGameWindowViewModel
{
    private readonly IDataGridHelper _dataGridHelper;
    private readonly IMetadataAccessor<Game> _metadataAccessor;
    private readonly IWindowHelper _windowHelper;

    public MainGameWindowViewModel(IDataGridHelper dataGridHelper, IMetadataAccessor<Game> metadataAccessor, IWindowHelper windowHelper)
    {
        _dataGridHelper = dataGridHelper;
        _metadataAccessor = metadataAccessor;
        _windowHelper = windowHelper;

        AddGameCommand = new RelayCommand<object>(_ => AddGame());

        MarkAsPlayedCommand = new RelayCommand<Game>(async game => await MarkAsPlayed(game));
        MarkAsFinishedCommand = new RelayCommand<Game>(async game => await MarkAsFinished(game));
        MarkAsCompletedCommand = new RelayCommand<Game>(async game => await MarkAsCompleted(game));
        EditCommand = new RelayCommand<Game>(Edit);
        DeleteCommand = new RelayCommand<Game>(Delete);
    }

    public ICommand AddGameCommand { get; }

    public ICommand MarkAsPlayedCommand { get; }
    public ICommand MarkAsFinishedCommand { get; }
    public ICommand MarkAsCompletedCommand { get; }
    public ICommand EditCommand { get; }
    public ICommand DeleteCommand { get; }

    public async Task InitializeAsync(DataGrid gameDataGrid)
    {
        await _dataGridHelper.PopulateGameDataGridAsync(gameDataGrid, _metadataAccessor);
    }

    private void AddGame()
    {
        _windowHelper.ShowWindow<AddGame>();
    }

    private async Task MarkAsPlayed(Game game)
    {
        game.HasPlayed = true;
        _metadataAccessor.UpdateItemAndSave(game.Id, "HasPlayed", true);
        await _dataGridHelper.RefreshGameDataGridAsync(_metadataAccessor);
    }

    private async Task MarkAsFinished(Game game)
    {
        game.HasFinished = true;
        _metadataAccessor.UpdateItemAndSave(game.Id, "HasFinished", true);
        await _dataGridHelper.RefreshGameDataGridAsync(_metadataAccessor);
    }

    private async Task MarkAsCompleted(Game game)
    {
        game.HasCompleted = true;
        _metadataAccessor.UpdateItemAndSave(game.Id, "HasCompleted", true);
        await _dataGridHelper.RefreshGameDataGridAsync(_metadataAccessor);
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

    public bool CanExecute(object? parameter) => parameter is T arg && (canExecute == null || canExecute(arg));
    public void Execute(object? parameter)
    {
        if (parameter is T arg)
        {
            _execute(arg);
        }
    }
}
