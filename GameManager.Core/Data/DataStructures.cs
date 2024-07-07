namespace GameManager.Core.Data;

public class Game(string title, Genre[]? genres, Platform[]? platforms)
{
    public string Title { get; init; } = title;
    public Genre[]? Genres { get; init; } = genres;
    public Platform[]? Platforms { get; init; } = platforms;
}

public class Genre(string name)
{
    public string Name { get; } = name;
}

public class Platform(string name)
{
    public string Name { get; } = name;
}