using GameManager.UI.ViewModels;
using Microsoft.Extensions.DependencyInjection;

namespace GameManager.UI.Windows;

public partial class PreferencesWindow
{
    private readonly PreferencesViewModel _viewModel;

    public PreferencesWindow(IServiceProvider sp)
    {
       _viewModel = sp.GetRequiredService<PreferencesViewModel>();
        DataContext = _viewModel;

        InitializeComponent();
    }
}
