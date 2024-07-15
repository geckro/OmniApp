using GameManager.Core.Data.Enums;
using System.Text.Json.Serialization;

namespace GameManager.Core.Data.MetadataConstructors;

public class Platform : IMetadata
{
    public required Guid Id { get; set; }

    public string Name { get; set; }

    public string? Company { get; set; }

    public Uri? Logo { get; set; }

    public PlatformType? Type { get; set; }

    [JsonIgnore] public string JsonFile => "platforms.json";
}
