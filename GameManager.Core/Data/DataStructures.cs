﻿namespace GameManager.Core.Data;

public class Game(
    string title,
    Genre[]? genres = null,
    Platform[]? platforms = null,
    DateTime? date = null,
    Developer[]? developers = null,
    Publisher[]? publishers = null,
    Series[]? series = null) : IMetadata
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

public class Platform(string name, string? company = null, PlatformType? type = null) : IMetadata
{
    public string Name { get; } = name;
    public string? Company { get; } = company;
    public PlatformType? Type { get; } = type;
}

public enum PlatformType : byte
{
    Home = 0,
    Handheld = 1,
    Hybrid = 2,
    Other = 3
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