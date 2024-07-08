namespace GameManager.Core.Data;

public class Game(
    string title,
    Genre[]? genres,
    Platform[]? platforms,
    DateTime? date,
    Developer[]? developers,
    Publisher[]? publishers,
    Series[]? series) : IMetadata
{
    public string Title { get; } = title;
    public Genre[]? Genres { get; } = genres;
    public Platform[]? Platforms { get; } = platforms;
    public DateTime? Date { get; } = date;
    public Developer[]? Developers { get; } = developers;
    public Publisher[]? Publishers { get; } = publishers;
    public Series[]? Series { get; } = series;

    string IMetadata.Name => Title;
}

public interface IMetadata
{
    string Name { get; }
}

public class Genre(string name) : IMetadata
{
    public string Name { get; } = name;
}

public class Platform(string name) : IMetadata
{
    public string Name { get; } = name;
}

public class Developer(string name) : IMetadata
{
    public string Name { get; } = name;
}

public class Publisher(string name) : IMetadata
{
    public string Name { get; } = name;
}

public class Series(string name) : IMetadata
{
    public string Name { get; } = name;
}