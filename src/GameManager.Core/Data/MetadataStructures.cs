using System.Text.Json.Serialization;

namespace GameManager.Core.Data;

/// <summary>
///     An engine the game was built on.
/// </summary>
public class Engine : IMetadata
{
    public required Guid Id { get; set; }
    public required string Name { get; set; }
    public ICollection<Company>? Developer { get; set; }
    public ICollection<Platform>? SupportedPlatforms { get; set; }
    public ICollection<string>? ProgrammingLanguages { get; set; }
    public Licenses? License { get; set; }
    public Uri? Logo { get; set; }
    public Uri? Website { get; set; }
    [JsonIgnore] public string JsonFile => "engines.json";
}

/// <summary>
///     Generic person.
/// </summary>
public class Person : IMetadata
{
    public required Guid Id { get; set; }
    public required string Name { get; set; }
    public DateTime? BirthDate { get; set; }
    public Uri? Picture { get; set; }
    [JsonIgnore] public string JsonFile => "people.json";
}

/// <summary>
///     A person that composed/made the music of the game.
/// </summary>
public class Composer : IMetadata
{
    public required Guid Id { get; set; }
    public required string Name { get; set; }
    public Uri? Picture { get; set; }
    public bool? IsMainPerson { get; set; }
    [JsonIgnore] public string JsonFile => "composers.json";
}

/// <summary>
///     A person who wrote or helped write the game.
/// </summary>
public class Writer : IMetadata
{
    public required Guid Id { get; set; }
    public required string Name { get; set; }
    public Uri? Picture { get; set; }
    public bool? IsMainPerson { get; set; }
    [JsonIgnore] public string JsonFile => "writers.json";
}

/// <summary>
///     A person who programmed parts of, or the whole game.
/// </summary>
public class Programmer : IMetadata
{
    public required Guid Id { get; set; }
    public required string Name { get; set; }
    public Uri? Picture { get; set; }
    public bool? IsMainPerson { get; set; }
    [JsonIgnore] public string JsonFile => "programmers.json";
}

/// <summary>
///     A person who made the artwork and graphics of the game.
/// </summary>
public class Artist : IMetadata
{
    public required Guid Id { get; set; }
    public required string Name { get; set; }
    public Uri? Picture { get; set; }
    public bool? IsMainPerson { get; set; }
    [JsonIgnore] public string JsonFile => "artists.json";
}

/// <summary>
///     A person who designed the game.
/// </summary>
public class Designer : IMetadata
{
    public required Guid Id { get; set; }
    public required string Name { get; set; }
    public Uri? Picture { get; set; }
    public bool? IsMainPerson { get; set; }
    [JsonIgnore] public string JsonFile => "designers.json";
}

/// <summary>
///     A person who produced or help produce the game.
/// </summary>
public class Producer : IMetadata
{
    public required Guid Id { get; set; }
    public required string Name { get; set; }
    public Uri? Picture { get; set; }
    public bool? IsMainPerson { get; set; }
    [JsonIgnore] public string JsonFile => "producers.json";
}

/// <summary>
///     A person who directed the making of the game.
/// </summary>
public class Director : IMetadata
{
    public required Guid Id { get; set; }
    public required string Name { get; set; }
    public Uri? Picture { get; set; }
    public bool? IsMainPerson { get; set; }
    [JsonIgnore] public string JsonFile => "directors.json";
}

/// <summary>
///     A genre of the game.
/// </summary>
public class Genre : IMetadata
{
    public required Guid Id { get; set; }
    public required string Name { get; set; }
    [JsonIgnore] public string JsonFile => "genres.json";
}

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

/// <summary>
///     A series that the game is linked to.
/// </summary>
public class Series : IMetadata
{
    public required Guid Id { get; set; }
    public required string Name { get; set; }
    public Uri? Logo { get; set; }
    [JsonIgnore] public string JsonFile => "series.json";
}

/// <summary>
///     An age rating that the game was classified as.
/// </summary>
public class AgeRatings : IMetadata
{
    public required Guid Id { get; set; }
    public required string Name { get; set; }
    public Uri? RatingSymbol { get; set; }
    [JsonIgnore] public string JsonFile => "ageratings.json";
}

/// <summary>
///     The license the game is under.
/// </summary>
public class Licenses : IMetadata
{
    public required Guid Id { get; set; }
    public required string Name { get; set; }
    [JsonIgnore] public string JsonFile => "licenses.json";
}
