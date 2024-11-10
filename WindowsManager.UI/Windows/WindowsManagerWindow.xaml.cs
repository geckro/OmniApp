using Microsoft.Extensions.DependencyInjection;
using WindowsManager.UI.ViewModels;

namespace WindowsManager.UI.Windows;

public partial class WindowsManagerWindow
{
    private readonly WindowsManagerViewModel _viewModel;

    public WindowsManagerWindow(IServiceProvider sp)
    {
        InitializeComponent();

        _viewModel = sp.GetRequiredService<WindowsManagerViewModel>();
        DataContext = _viewModel;
    }
}
