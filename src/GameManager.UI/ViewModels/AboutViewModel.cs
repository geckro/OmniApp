using OmniApp.Common;

namespace GameManager.UI.ViewModels;

public class AboutViewModel
{
    // ReSharper disable UnusedMember.Global
    public static string ProgramVersion => AssemblyInfo.ProgramVersion;
    // ReSharper restore UnusedMember.Global

    public AboutViewModel()
    {
    }
}
