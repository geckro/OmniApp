using DietManager.UI.ViewModels;
using Microsoft.Extensions.DependencyInjection;

namespace DietManager.UI.Windows;

/// <summary>
///     Interaction logic for DietManagerWindow.xaml
/// </summary>
public partial class DietManagerWindow
{
    private readonly DietManagerViewModel _viewModel;

    public DietManagerWindow(IServiceProvider sp)
    {
        InitializeComponent();

        _viewModel = sp.GetRequiredService<DietManagerViewModel>();
        DataContext = _viewModel;
    }
}
