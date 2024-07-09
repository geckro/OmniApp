namespace GameManager.Core.Data;

public class Game(
    string title,
    Genre[]? genres = null,
    Platform[]? platforms = null,
    DateTime? date = null,
    Developer[]? developers = null,
    Publisher[]? publishers = null,
    Series[]? series = null,
    bool? isRemake = null,
    Game? remakeOf = null,
    bool? isRemaster = null,
    Game? remasterOf = null,
    bool? isPort = null,
    Game? portOf = null,
    bool? isSequel = null,
    Game? sequelOf = null,
    bool? isPrequel = null,
    Game? prequelOf = null,
    bool? isDlc = null,
    Game? dlcOf = null,
    bool? isMod = null,
    Game? modOf = null,
    bool? isHack = null,
    Game? hackOf = null) : IMetadata
{
    public string Title { get; } = title;
    public Genre[]? Genres { get; } = genres;
    public Platform[]? Platforms { get; } = platforms;
    public DateTime? Date { get; } = date;
    public Developer[]? Developers { get; } = developers;
    public Publisher[]? Publishers { get; } = publishers;
    public Series[]? Series { get; } = series;

    public bool? IsRemake { get; } = isRemake;
    public Game? RemakeOf { get; } = remakeOf;
    public bool? IsRemaster { get; } = isRemaster;
    public Game? RemasterOf { get; } = remasterOf;
    public bool? IsPort { get; } = isPort;
    public Game? PortOf { get; } = portOf;
    public bool? IsSequel { get; } = isSequel;
    public Game? SequelOf { get; } = sequelOf;
    public bool? IsPrequel { get; } = isPrequel;
    public Game? PrequelOf { get; } = prequelOf;

    public bool? IsDlc { get; } = isDlc;
    public Game? DlcOf { get; } = dlcOf;
    public bool? IsMod { get; } = isMod;
    public Game? ModOf { get; } = modOf;
    public bool? IsHack { get; } = isHack;
    public Game? HackOf { get; } = hackOf;

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