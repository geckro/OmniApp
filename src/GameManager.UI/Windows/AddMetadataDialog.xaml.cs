using GameManager.Core.Data;
using GameManager.Core.Data.MetadataConstructors;
using GameManager.UI.ViewModels;
using Microsoft.Extensions.DependencyInjection;
using System.Windows;

namespace GameManager.UI.Windows;

public partial class AddMetadataDialog
{
    private readonly MetadataAccessor<Game> _gameAcc;
    private readonly AddMetadataViewModel _viewModel;
    private Game? _game;
    private string _category;

    public AddMetadataDialog(IServiceProvider sp, MetadataAccessor<Game> gameAcc)
    {
        _gameAcc = gameAcc;

        _viewModel = sp.GetRequiredService<AddMetadataViewModel>();
        DataContext = _viewModel;
        InitializeComponent();

        InitializeMetadata();
    }

    private void InitializeMetadata()
    {
        Title = $"Add new {_category}";

        CurrentMetadataLabel.Content = $"Current {_category}";
        NewMetadataLabel.Content = $"New {_category}";
    }

    public bool MetadataAddedToGame { get; private set; }

    public void SetCurrentGame(Game? game)
    {
        _game = game;
        _viewModel.Game = game;
    }

    public void SetCurrentCategory(string category)
    {
        _category = category;
        _viewModel.Category = category;

        _viewModel.PopulateMetadataListBoxes(CurrentMetadataListBox, NewMetadataListBox);
    }

    private void AddNewMetadataToGame_OnClick(object sender,
            RoutedEventArgs e)
    {
    }
}
