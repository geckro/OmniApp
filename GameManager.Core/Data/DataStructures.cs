﻿namespace GameManager.Core.Data;

public class Game(string title, Genre[]? genres, Platform[]? platforms, DateTime? date) : IMetadata
{
    public string Title { get; } = title;
    public Genre[]? Genres { get; } = genres;
    public Platform[]? Platforms { get; } = platforms;
    public DateTime? Date { get; } = date;

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