using GameManager.UI.ViewModels;
using Microsoft.Extensions.DependencyInjection;

namespace GameManager.UI.Windows;

public partial class ModifyMetadataWindow
{
    private readonly ModifyMetadataViewModel _viewModel;

    public ModifyMetadataWindow(IServiceProvider sp)
    {
        InitializeComponent();

        _viewModel = sp.GetRequiredService<ModifyMetadataViewModel>();
        DataContext = _viewModel;
    }
}

