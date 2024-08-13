using GameManager.UI.ViewModels;
using Microsoft.Extensions.DependencyInjection;

namespace GameManager.UI.Windows;

public partial class AboutDialog
{
    private readonly AboutViewModel _viewModel;

    public AboutDialog(IServiceProvider sp)
    {
        InitializeComponent();

        _viewModel = sp.GetRequiredService<AboutViewModel>();
        DataContext = _viewModel;
    }
}

