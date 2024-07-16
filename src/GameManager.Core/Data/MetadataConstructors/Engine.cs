using System.Text.Json.Serialization;

namespace GameManager.Core.Data.MetadataConstructors;

/// <summary>
///     An engine the game was built on.
/// </summary>
public class Engine : IMetadata
{
    public required Guid Id { get; set; }
    public string Name { get; set; } = null!;
    public ICollection<Company>? Developer { get; set; }
    public ICollection<Platform>? SupportedPlatforms { get; set; }
    public ICollection<string>? ProgrammingLanguages { get; set; }
    public Licenses? License { get; set; }
    public Uri? Logo { get; set; }
    public Uri? Website { get; set; }
    [JsonIgnore] public string JsonFile => "engines.json";
}
