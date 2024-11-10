using System.Diagnostics;
using System.Net;
using System.Net.Sockets;
using System.Security.Principal;

namespace WindowsManager.Common;

public static class SysInfo
{
    public static string GetNetBiosComputerName() => Environment.MachineName;
    public static string GetCurrentUsername() => WindowsIdentity.GetCurrent().Name;
    public static string GetIpv4Address()
    {
        IPHostEntry host = Dns.GetHostEntry(Dns.GetHostName());
        foreach (IPAddress ip in host.AddressList)
        {
            if (ip.AddressFamily == AddressFamily.InterNetwork)
            {
                return ip.ToString();
            }
        }

        return "no IPv4 address";
    }

    public static string GetRamAmount()
    {
        const string command = "Get-CimInstance Win32_ComputerSystem | foreach {[math]::round($_.TotalPhysicalMemory /1GB)}";

        ProcessStartInfo startInfo = new()
        {
                FileName = "powershell.exe",
                Arguments = $"-NoProfile -ExecutionPolicy Bypass -Command \"{command}\"",
                RedirectStandardOutput = true,
                UseShellExecute = false,
                CreateNoWindow = true
        };

        using Process process = Process.Start(startInfo);
        using StreamReader reader = process.StandardOutput;
        string result = reader.ReadToEnd();
        process.WaitForExit();
        return $"{result.Trim()} GB";
    }

    public static string GetCpuName()
    {
        const string command = "Get-CimInstance WIN32_Processor | Select-Object Name";

        ProcessStartInfo startInfo = new()
        {
                FileName = "powershell.exe",
                Arguments = $"-NoProfile -ExecutionPolicy Bypass -Command \"{command}\"",
                RedirectStandardOutput = true,
                UseShellExecute = false,
                CreateNoWindow = true
        };

        using Process process = Process.Start(startInfo);
        using StreamReader reader = process.StandardOutput;
        string result = reader.ReadToEnd();
        process.WaitForExit();
        return $"{result.Trim()}";
    }

    public static string GetCpu()
    {
        const string command = "Get-WmiObject WIN32_Processor -Property Name";

        ProcessStartInfo startInfo = new()
        {
                FileName = "powershell.exe",
                Arguments = $"-NoProfile -ExecutionPolicy Bypass -Command \"{command}\"",
                RedirectStandardOutput = true,
                UseShellExecute = false,
                CreateNoWindow = true
        };

        using Process process = Process.Start(startInfo);
        using StreamReader reader = process.StandardOutput;
        string result = reader.ReadToEnd();
        process.WaitForExit();
        return $"{result.Trim()}";
    }
}
