using System.Text.Json.Serialization;

namespace GameManager.Core.Data.MetadataConstructors;

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
