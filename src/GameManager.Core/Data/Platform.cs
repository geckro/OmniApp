using System.Text.Json.Serialization;

namespace GameManager.Core.Data;

public class Platform : IMetadata
{
    public required Guid Id { get; set; }

    public string? Company { get; set; }

    public Uri? Logo { get; set; }

    public PlatformType? Type { get; set; }

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
