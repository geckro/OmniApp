using System.Text.Json.Serialization;

namespace GameManager.Core.Data;

/// <summary>
///     An engine the game was built on.
/// </summary>
public class Engine : IMetadata
{
    public string Name { get; set; }
    [JsonIgnore] public string JsonFile => "engines.json";
}

/// <summary>
///     A person that composed/made the music of the game.
/// </summary>
public class Composer : IMetadata
{
    public string Name { get; set; }
    [JsonIgnore] public string JsonFile => "composers.json";
}

/// <summary>
///     A person who wrote or helped write the game.
/// </summary>
public class Writer : IMetadata
{
    public string Name { get; set; }
    [JsonIgnore] public string JsonFile => "writers.json";
}

/// <summary>
///     A person who programmed parts of, or the whole game.
/// </summary>
public class Programmer : IMetadata
{
    public string Name { get; set; }
    [JsonIgnore] public string JsonFile => "programmers.json";
}

/// <summary>
///     A person who made the artwork and graphics of the game.
/// </summary>
public class Artist : IMetadata
{
    public string Name { get; set; }
    [JsonIgnore] public string JsonFile => "artists.json";
}

/// <summary>
///     A person who designed the game.
/// </summary>
public class Designer : IMetadata
{
    public string Name { get; set; }
    [JsonIgnore] public string JsonFile => "designers.json";
}

/// <summary>
///     A person who produced or help produce the game.
/// </summary>
public class Producer : IMetadata
{
    public string Name { get; set; }
    [JsonIgnore] public string JsonFile => "producers.json";
}

/// <summary>
///     A person who directed the making of the game.
/// </summary>
public class Director : IMetadata
{
    public string Name { get; set; }
    [JsonIgnore] public string JsonFile => "directors.json";
}

/// <summary>
///     A genre of the game.
/// </summary>
public class Genre : IMetadata
{
    public string Name { get; set; }

    [JsonIgnore] public string JsonFile => "genres.json";
}

/// <summary>
///     A developer of the game.
/// </summary>
public class Developer : IMetadata
{
    public string Name { get; set; }
    [JsonIgnore] public string JsonFile => "developers.json";
}

/// <summary>
///     A publisher of the game.
/// </summary>
public class Publisher : IMetadata
{
    public string Name { get; set; }
    [JsonIgnore] public string JsonFile => "publishers.json";
}

/// <summary>
///     A series that the game is linked to.
/// </summary>
public class Series : IMetadata
{
    public string Name { get; set; }
    [JsonIgnore] public string JsonFile => "series.json";
}

/// <summary>
///     An age rating that the game was classified as.
/// </summary>
public class AgeRatings : IMetadata
{
    public string Name { get; set; }
    [JsonIgnore] public string JsonFile => "ageratings.json";
}
