using FinancialManager.UI.ViewModels;
using Microsoft.Extensions.DependencyInjection;

namespace FinancialManager.UI.Windows;

public partial class FinanceManagerWindow
{
    private FinanceManagerViewModel _viewModel;

    public FinanceManagerWindow(IServiceProvider sp)
    {
        InitializeComponent();

        _viewModel = sp.GetRequiredService<FinanceManagerViewModel>();
        DataContext = _viewModel;
    }
}
