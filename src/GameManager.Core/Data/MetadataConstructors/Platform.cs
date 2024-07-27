using System.Text.Json.Serialization;

namespace GameManager.Core.Data.MetadataConstructors;

public class Platform : IMetadata
{
    public Dictionary<string, object>? Tags { get; init; }
    public required Guid Id { get; init; }
    public string Name { get; init; } = null!;
    [JsonIgnore] public string JsonFile => "platforms.json";
}
