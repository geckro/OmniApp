using OmniApp.Common;

namespace GameManager.UI.ViewModels;

public class AboutViewModel
{
    public static string ProgramVersion => AssemblyInfo.ProgramVersion;

    public AboutViewModel()
    {
    }
}
