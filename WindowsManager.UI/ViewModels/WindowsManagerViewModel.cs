using OmniApp.UI.Common.Helpers;
using WindowsManager.Common;

namespace WindowsManager.UI.ViewModels;

public class WindowsManagerViewModel
{
    private readonly WindowHelper _windowHelper;

    public WindowsManagerViewModel(WindowHelper windowHelper)
    {
        _windowHelper = windowHelper;
    }

    public static string ComputerName => $"Computer name: {SysInfo.GetNetBiosComputerName()}";
    public static string UserName => $"User name: {SysInfo.GetCurrentUsername()}";
    public static string Ipv4Address => $"Ipv4 address: {SysInfo.GetIpv4Address()}";
    public static string RamAmount => $"Total RAM: {SysInfo.GetRamAmount()}";
}
