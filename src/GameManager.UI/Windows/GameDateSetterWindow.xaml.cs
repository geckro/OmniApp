using GameManager.UI.ViewModels;
using Microsoft.Extensions.DependencyInjection;
using System.Windows;

namespace GameManager.UI.Windows;

public partial class GameDateSetterWindow
{
    private readonly GameDateSetterViewModel _viewModel;

    public GameDateSetterWindow(IServiceProvider sp)
    {
        _viewModel = sp.GetRequiredService<GameDateSetterViewModel>();
        DataContext = _viewModel;

        InitializeComponent();
    }

    private void GameDateSetterWindow_OnLoaded(object sender, RoutedEventArgs e)
    {
        Window window = GetWindow(this) ?? throw new InvalidOperationException();
        window.KeyDown += _viewModel.HandleKeyDown;
        window.KeyUp += _viewModel.HandleKeyUp;
    }
}

