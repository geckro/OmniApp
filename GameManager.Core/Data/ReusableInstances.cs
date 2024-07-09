// ReSharper disable InconsistentNaming
// ReSharper disable IdentifierTypo
// ReSharper disable StringLiteralTypo

using static GameManager.Core.Data.Overloads;

namespace GameManager.Core.Data;

public static class ReusableInstances
{
    // Reusable instances

    // Reusable developer instances
    public static class Developers
    {
        public static readonly Developer FromSoftware = Dev("FromSoftware");
        public static readonly Developer QLOC = Dev("QLOC");
        public static readonly Developer Nintendo_EAD = Dev("Nintendo EAD");
        public static readonly Developer Nintendo_EPD = Dev("Nintendo EPD");
    }

    // Reusable publisher instances
    public static class Publishers
    {
        public static readonly Publisher Bandai_Namco = P("Bandai Namco");
        public static readonly Publisher Nintendo = P("Nintendo");
    }

    // Reusable series instances
    public static class Series
    {
        public static readonly Data.Series Souls = S("Souls");
    }

    // Reusable genre instances
    public static class Genres
    {
        public static readonly Genre Action_RPG = G("Action RPG");
    }

    // Reusable platform instances
    public static class Platforms
    {
        private static readonly PlatformData platformData = new();
        private static Platform GetPlatform(string name)
        {
            List<Platform> platforms = platformData.GetAllPlatforms();
            return platforms.Find(p => p.Name == name) ?? throw new InvalidOperationException("Cannot find Platform.");
        }

        public static readonly Platform PS3 = GetPlatform("PlayStation 3");
        public static readonly Platform PS4 = GetPlatform("PlayStation 4");
        public static readonly Platform PS5 = GetPlatform("PlayStation 5");

        public static readonly Platform Xbox360 = GetPlatform("Xbox 360");
        public static readonly Platform Xbox1 = GetPlatform("Xbox One");
        public static readonly Platform XboxSX = GetPlatform("Xbox Series X/S");

        public static readonly Platform Windows = GetPlatform("Windows");

        public static readonly Platform Switch = GetPlatform("Switch");
    }
}