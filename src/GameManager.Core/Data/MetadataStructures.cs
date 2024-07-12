using System.Text.Json.Serialization;

namespace GameManager.Core.Data;

public class Engine() : IMetadata
{
    public string Name { get; set; }
    [JsonIgnore] public string JsonFile => "engines.json";
}

public class Composer() : IMetadata
{
    public string Name { get; set; }
    [JsonIgnore] public string JsonFile => "composers.json";
}

public class Writer() : IMetadata
{
    public string Name { get; set; }
    [JsonIgnore] public string JsonFile => "writers.json";
}

public class Programmer() : IMetadata
{
    public string Name { get; set; }
    [JsonIgnore] public string JsonFile => "programmers.json";
}

public class Artist() : IMetadata
{
    public string Name { get; set; }
    [JsonIgnore] public string JsonFile => "artists.json";
}

public class Designer() : IMetadata
{
    public string Name { get; set; }
    [JsonIgnore] public string JsonFile => "designers.json";
}

public class Producer() : IMetadata
{
    public string Name { get; set; }
    [JsonIgnore] public string JsonFile => "producers.json";
}

public class Director() : IMetadata
{
    public string Name { get; set; }
    [JsonIgnore] public string JsonFile => "directors.json";
}

public class Genre() : IMetadata
{
    public string Name { get; set; }

    [JsonIgnore] public string JsonFile => "genres.json";
}

public class Developer() : IMetadata
{
    public string Name { get; set; }
    [JsonIgnore] public string JsonFile => "developers.json";
}

public class Publisher() : IMetadata
{
    public string Name { get; set; }
    [JsonIgnore] public string JsonFile => "publishers.json";
}

public class Series() : IMetadata
{
    public string Name { get; set; }
    [JsonIgnore] public string JsonFile => "series.json";
}

public class AgeRatings() : IMetadata
{
    public string Name { get; set; }
    [JsonIgnore] public string JsonFile => "ageratings.json";
}
