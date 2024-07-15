using System.Text.Json.Serialization;

namespace GameManager.Core.Data.MetadataConstructors;

/// <summary>
///     An age rating that the game was classified as.
/// </summary>
public class AgeRatings : IMetadata
{
    public required Guid Id { get; set; }
    public string Name { get; set; }
    public Uri? RatingSymbol { get; set; }
    [JsonIgnore] public string JsonFile => "ageratings.json";
}
