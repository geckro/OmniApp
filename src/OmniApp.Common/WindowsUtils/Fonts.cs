using System.Drawing.Text;

namespace OmniApp.Common.WindowsUtils;

public class Fonts
{
    public static List<string> GetInstalledFonts()
    {
        List<string> fonts = [];
        #pragma warning disable CA1416
        InstalledFontCollection installedFonts = new();
        fonts.AddRange(installedFonts.Families.Select(font => font.Name));
        #pragma warning restore CA1416

        return fonts;
    }
}
