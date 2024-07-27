using System.Text.Json.Serialization;

namespace GameManager.Core.Data.MetadataConstructors;

/// <summary>
///     A series that the game is linked to.
/// </summary>
public class Series : IMetadata
{
    public Dictionary<string, object>? Tags { get; init; }
    public required Guid Id { get; init; }
    public string Name { get; init; } = null!;
    [JsonIgnore] public string JsonFile => "series.json";
}
