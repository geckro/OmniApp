namespace GameManager.Core.Data;

public interface IMetadata
{
    string Name { get; }
}

public class Engine(string name) : IMetadata
{
    public string Name { get; } = name;
}

public class Composer(string name) : IMetadata
{
    public string Name { get; } = name;
}

public class Writer(string name) : IMetadata
{
    public string Name { get; } = name;
}

public class Programmer(string name) : IMetadata
{
    public string Name { get; } = name;
}

public class Artist(string name) : IMetadata
{
    public string Name { get; } = name;
}

public class Designer(string name) : IMetadata
{
    public string Name { get; } = name;
}

public class Producer(string name) : IMetadata
{
    public string Name { get; } = name;
}

public class Director(string name) : IMetadata
{
    public string Name { get; } = name;
}

public class Genre(string name) : IMetadata
{
    public string Name { get; } = name;
}

public class Platform(string name, string? company = null, PlatformType? type = null) : IMetadata
{
    public string? Company { get; } = company;
    public PlatformType? Type { get; } = type;
    public string Name { get; } = name;
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
