using System.Text.Json.Serialization;

namespace GameManager.Core.Data.MetadataConstructors;

/// <summary>
///     A series that the game is linked to.
/// </summary>
public class Series : IMetadata
{
    public Dictionary<string, ICollection<string>>? Tags { get; set; }
    public required Guid Id { get; init; }
    public string Name { get; init; } = null!;
    [JsonIgnore] public string JsonFile => "series.json";
}
