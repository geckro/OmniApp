using System.Text.Json.Serialization;

namespace GameManager.Core.Data;

public class Platform : IMetadata
{
    /// <summary>
    ///     The company that owns the platform.
    /// </summary>
    public string? Company { get; set; }

    /// <summary>
    ///     The logo of the game platform.
    /// </summary>
    public Uri? Logo { get; set; }

    /// <summary>
    ///     The type of the platform.
    /// </summary>
    public PlatformType? Type { get; set; }

    /// <summary>
    ///     The name of the platform.
    /// </summary>
    public string Name { get; set; }

    [JsonIgnore] public string JsonFile => "platforms.json";
}

public enum PlatformType : byte
{
    Home = 0,
    Handheld = 1,
    Hybrid = 2,
    Computer = 3,
    Other = 4
}
