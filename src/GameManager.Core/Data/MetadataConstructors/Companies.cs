using System.Text.Json.Serialization;

namespace GameManager.Core.Data.MetadataConstructors;

/// <summary>
///     A developer of the game.
/// </summary>
public class Developer : IMetadata
{
    public Dictionary<string, object>? Tags { get; init; }
    public required Guid Id { get; init; }
    public string Name { get; init; } = null!;
    [JsonIgnore] public string JsonFile => "developers.json";
}

/// <summary>
///     A publisher of the game.
/// </summary>
public class Publisher : IMetadata
{
    public Dictionary<string, object>? Tags { get; init; }
    public required Guid Id { get; init; }
    public string Name { get; init; } = null!;
    [JsonIgnore] public string JsonFile => "publishers.json";
}
