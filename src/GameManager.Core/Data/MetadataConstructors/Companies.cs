using GameManager.Core.Data.Enums;
using System.Text.Json.Serialization;

namespace GameManager.Core.Data.MetadataConstructors;

/// <summary>
///     Generic company.
/// </summary>
public class Company : IMetadata
{
    public required Guid Id { get; set; }
    public required string Name { get; set; }
    public ICollection<string>? OtherNames { get; set; }
    public DateTime? DateFounded { get; set; }
    public Region? LocatedAt { get; set; }
    public Uri? Logo { get; set; }
    public Uri? Website { get; set; }
    [JsonIgnore] public string JsonFile => "companies.json";
}

/// <summary>
///     A developer of the game.
/// </summary>
public class Developer : IMetadata
{
    public required Guid Id { get; set; }
    public required string Name { get; set; }
    public ICollection<string>? OtherNames { get; set; }
    public DateTime? DateFounded { get; set; }
    public Region? LocatedAt { get; set; }
    public Uri? Logo { get; set; }
    public Uri? Website { get; set; }
    [JsonIgnore] public string JsonFile => "developers.json";
}

/// <summary>
///     A publisher of the game.
/// </summary>
public class Publisher : IMetadata
{
    public required Guid Id { get; set; }
    public required string Name { get; set; }
    public ICollection<string>? OtherNames { get; set; }
    public DateTime? DateFounded { get; set; }
    public Region? LocatedAt { get; set; }
    public Uri? Logo { get; set; }
    public Uri? Website { get; set; }
    [JsonIgnore] public string JsonFile => "publishers.json";
}
