namespace GameManager.Core.Data;

public class Game(string title, Genre[]? genres, Platform[]? platforms)
{
    public string Title { get; } = title;
    public Genre[]? Genres { get; } = genres;
    public Platform[]? Platforms { get; } = platforms;
}

public class Genre(string name)
{
    public string Name { get; } = name;
}

public class Platform(string name)
{
    public string Name { get; } = name;
}