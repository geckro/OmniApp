using OmniApp.UI.Common.Helpers;

namespace DietManager.UI.ViewModels;

public class DietManagerViewModel
{
    private readonly WindowHelper _windowHelper;

    public DietManagerViewModel(WindowHelper windowHelper)
    {
        _windowHelper = windowHelper;

        InitializeCommands();
    }

    private void InitializeCommands()
    {
    }
}
