using System.Text.Json.Serialization;

namespace GameManager.Core.Data;

/// <summary>
///     An engine the game was built on.
/// </summary>
public class Engine : IMetadata
{
    public required string Name { get; set; }
    [JsonIgnore] public string JsonFile => "engines.json";
}

/// <summary>
///     An abstract class for a Person
/// </summary>
public abstract class Person : IPerson
{
    public required string Name { get; set; }
    public bool? MainPerson { get; set; }
    public Uri? Picture { get; set; }
    [JsonIgnore] public abstract string JsonFile { get; }
}

/// <summary>
///     An abstract class for a Company
/// </summary>
public abstract class Company : ICompany
{
    public required string Name { get; set; }
    public Uri? Logo { get; set; }
    [JsonIgnore] public abstract string JsonFile { get; }
}

/// <summary>
///     A person that composed/made the music of the game.
/// </summary>
public class Composer : Person
{
    [JsonIgnore] public override string JsonFile => "composers.json";
}

/// <summary>
///     A person who wrote or helped write the game.
/// </summary>
public class Writer : Person
{
    [JsonIgnore] public override string JsonFile => "writers.json";
}

/// <summary>
///     A person who programmed parts of, or the whole game.
/// </summary>
public class Programmer : Person
{
    [JsonIgnore] public override string JsonFile => "programmers.json";
}

/// <summary>
///     A person who made the artwork and graphics of the game.
/// </summary>
public class Artist : Person
{
    [JsonIgnore] public override string JsonFile => "artists.json";
}

/// <summary>
///     A person who designed the game.
/// </summary>
public class Designer : Person
{
    [JsonIgnore] public override string JsonFile => "designers.json";
}

/// <summary>
///     A person who produced or help produce the game.
/// </summary>
public class Producer : Person
{
    [JsonIgnore] public override string JsonFile => "producers.json";
}

/// <summary>
///     A person who directed the making of the game.
/// </summary>
public class Director : Person
{
    [JsonIgnore] public override string JsonFile => "directors.json";
}

/// <summary>
///     A genre of the game.
/// </summary>
public class Genre : IMetadata
{
    public required string Name { get; set; }
    [JsonIgnore] public string JsonFile => "genres.json";
}

/// <summary>
///     A developer of the game.
/// </summary>
public class Developer : Company
{
    [JsonIgnore] public override string JsonFile => "developers.json";
}

/// <summary>
///     A publisher of the game.
/// </summary>
public class Publisher : Company
{
    [JsonIgnore] public override string JsonFile => "publishers.json";
}

/// <summary>
///     A series that the game is linked to.
/// </summary>
public class Series : IMetadata
{
    public required string Name { get; set; }
    public Uri? Logo { get; set; }
    [JsonIgnore] public string JsonFile => "series.json";
}

/// <summary>
///     An age rating that the game was classified as.
/// </summary>
public class AgeRatings : IMetadata
{
    public required string Name { get; set; }
    public Uri? RatingSymbol { get; set; }
    [JsonIgnore] public string JsonFile => "ageratings.json";
}
