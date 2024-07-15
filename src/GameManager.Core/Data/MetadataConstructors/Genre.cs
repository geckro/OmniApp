using System.Text.Json.Serialization;

namespace GameManager.Core.Data.MetadataConstructors;

/// <summary>
///     A genre of the game.
/// </summary>
public class Genre : IMetadata
{
    public required Guid Id { get; set; }
    public string Name { get; set; }
    [JsonIgnore] public string JsonFile => "genres.json";
}
