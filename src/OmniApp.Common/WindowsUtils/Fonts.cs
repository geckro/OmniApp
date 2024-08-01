using System.Drawing.Text;

namespace OmniApp.Common.WindowsUtils;

public class Fonts
{
    public static List<string> GetInstalledFonts()
    {
        List<string> fonts = [];
        InstalledFontCollection installedFonts = new();
        fonts.AddRange(installedFonts.Families.Select(font => font.Name));

        return fonts;
    }
}
