using System.Text.Json.Serialization;

namespace GameManager.Core.Data.MetadataConstructors;

/// <summary>
///     The license the game is under.
/// </summary>
public class Licenses : IMetadata
{
    public required Guid Id { get; set; }
    public required string Name { get; set; }
    [JsonIgnore] public string JsonFile => "licenses.json";
}
