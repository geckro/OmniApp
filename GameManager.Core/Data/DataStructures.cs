namespace GameManager.Core.Data;

public class Game(
    string title,
    Genre[]? genres = null,
    Platform[]? platforms = null,
    Developer[]? developers = null,
    Publisher[]? publishers = null,
    Series[]? series = null,
    DateTime? releaseDateWw = null,
    Dictionary<Region, DateTime?>? regionReleaseDates = null,
    bool isRemake = false,
    Game? remakeOf = null,
    bool isRemaster = false,
    Game? remasterOf = null,
    bool isPort = false,
    Game? portOf = null,
    bool isSequel = false,
    Game? sequelOf = null,
    bool isPrequel = false,
    Game? prequelOf = null,
    bool isDlc = false,
    Game? dlcOf = null,
    bool isMod = false,
    Game? modOf = null,
    bool isHack = false,
    Game? hackOf = null,
    Dictionary<Region, string>? regionTitles = null,
    Director[]? directors = null,
    Producer[]? producers = null,
    Designer[]? designers = null,
    Programmer[]? programmers = null,
    Artist[]? artists = null,
    Writer[]? writers = null,
    Composer[]? composers = null,
    Engine? engine = null,
    bool hasPlayed = false,
    bool hasFinished = false,
    bool hasCompleted = false,
    string? ageRating = null) : IMetadata
{
    /// <summary>
    ///     The title or name of the game if no region is specified. Defaults to Worldwide.
    /// </summary>
    public string Title { get; } = title;

    /// <summary>
    ///     Region-specific titles.
    /// </summary>
    public Dictionary<Region, string>? RegionTitle { get; } = regionTitles;

    /// <summary>
    ///     A list of genres that apply to the game.
    /// </summary>
    public Genre[]? Genres { get; } = genres;

    /// <summary>
    ///     A list of platforms or systems that the game is available on.
    /// </summary>
    public Platform[]? Platforms { get; } = platforms;

    /// <summary>
    ///     A list of developers of the game.
    /// </summary>
    public Developer[]? Developers { get; } = developers;

    /// <summary>
    ///     A list of publishers of the game.
    /// </summary>
    public Publisher[]? Publishers { get; } = publishers;

    /// <summary>
    ///     A list of series that the game belongs to.
    /// </summary>
    public Series[]? Series { get; } = series;

    /// <summary>
    ///     Default release date if no region is specified, assumes that the game released worldwide on the same date.
    /// </summary>
    public DateTime? ReleaseDateWw { get; } = releaseDateWw;

    /// <summary>
    ///     Region-specific release dates.
    /// </summary>
    public Dictionary<Region, DateTime?> RegionReleaseDates { get; } =
        regionReleaseDates ?? new Dictionary<Region, DateTime?>();

    /// <summary>
    ///     If the game is a from the ground up remake.
    /// </summary>
    public bool IsRemake { get; } = isRemake;

    public Game? RemakeOf { get; } = remakeOf;

    /// <summary>
    ///     If the game is a remaster.
    /// </summary>
    public bool IsRemaster { get; } = isRemaster;

    public Game? RemasterOf { get; } = remasterOf;

    /// <summary>
    ///     If the game is a port.
    /// </summary>
    public bool IsPort { get; } = isPort;

    public Game? PortOf { get; } = portOf;

    /// <summary>
    ///     If the game is a sequel to another game.
    /// </summary>
    public bool IsSequel { get; } = isSequel;

    public Game? SequelOf { get; } = sequelOf;

    /// <summary>
    ///     If the game is a prequel to another game.
    /// </summary>
    public bool IsPrequel { get; } = isPrequel;

    public Game? PrequelOf { get; } = prequelOf;

    /// <summary>
    ///     If the Game is downloadable content.
    /// </summary>
    public bool IsDlc { get; } = isDlc;

    public Game? DlcOf { get; } = dlcOf;

    /// <summary>
    ///     If the game is a mod of a game, whether it be official or not.
    /// </summary>
    public bool IsMod { get; } = isMod;

    public Game? ModOf { get; } = modOf;

    /// <summary>
    ///     If the game is a hack of a game, whether it be official or not.
    /// </summary>
    public bool IsHack { get; } = isHack;

    public Game? HackOf { get; } = hackOf;

    /// <summary>
    ///     The creation date of the game entry.
    /// </summary>
    public DateTime CreatedOn { get; } = DateTime.Now;

    /// <summary>
    ///     The last time the game entry was updated.
    /// </summary>
    public DateTime LastUpdated { get; set; } = DateTime.Now;

    /// <summary>
    ///     A list of composers of the game.
    /// </summary>
    public Composer[]? Composers { get; } = composers;

    /// <summary>
    ///     A list of directors of the game.
    /// </summary>
    public Director[]? Directors { get; } = directors;

    /// <summary>
    ///     A list of writers of the game.
    /// </summary>
    public Writer[]? Writers { get; } = writers;

    /// <summary>
    ///     A list of programmers of the game.
    /// </summary>
    public Programmer[]? Programmers { get; } = programmers;

    /// <summary>
    ///     A list of artists of the game.
    /// </summary>
    public Artist[]? Artists { get; } = artists;

    /// <summary>
    ///     A list of designers of the game.
    /// </summary>
    public Designer[]? Designers { get; } = designers;

    /// <summary>
    ///     The engine that the game uses to run.
    /// </summary>
    public Engine? Engine { get; } = engine;

    /// <summary>
    ///     If the user has played the game.
    /// </summary>
    public bool HasPlayed { get; } = hasPlayed;

    /// <summary>
    ///     If the user has finished the game.
    /// </summary>
    public bool HasFinished { get; } = hasFinished;

    /// <summary>
    ///     If the user has fully completed the game.
    /// </summary>
    public bool HasCompleted { get; } = hasCompleted;

    /// <summary>
    ///     The age rating of the game.
    /// </summary>
    public string? AgeRating { get; } = ageRating;

    string IMetadata.Name => Title;
}

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

public enum Region
{
    [RegionName(RegionName.Australia), RegionName(RegionName.Worldwide), RegionLanguage(Language.English)]
    Au,

    [RegionName(RegionName.Brazil), RegionName(RegionName.Worldwide), RegionName(RegionName.Pal),
     RegionLanguage(Language.Portuguese)]
    Br,

    [RegionName(RegionName.NorthAmerica), RegionName(RegionName.Canada), RegionName(RegionName.Worldwide),
     RegionLanguage(Language.English)]
    Ca,

    [RegionName(RegionName.China), RegionName(RegionName.Worldwide), RegionLanguage(Language.Chinese)]
    Cn,

    [RegionName(RegionName.Europe), RegionName(RegionName.Pal), RegionName(RegionName.Worldwide)]
    Eu,

    [RegionName(RegionName.France), RegionName(RegionName.Europe), RegionName(RegionName.Pal),
     RegionName(RegionName.Worldwide), RegionLanguage(Language.French)]
    Fr,

    [RegionName(RegionName.Germany), RegionName(RegionName.Europe), RegionName(RegionName.Pal),
     RegionName(RegionName.Worldwide), RegionLanguage(Language.German)]
    Ger,

    [RegionName(RegionName.Italy), RegionName(RegionName.Europe), RegionName(RegionName.Pal),
     RegionName(RegionName.Worldwide), RegionLanguage(Language.Italian)]
    It,

    [RegionName(RegionName.Japan), RegionName(RegionName.Worldwide), RegionLanguage(Language.Japanese)]
    Jp,

    [RegionName(RegionName.Korea), RegionName(RegionName.Worldwide), RegionLanguage(Language.Korean)]
    Ko,

    [RegionName(RegionName.Netherlands), RegionName(RegionName.Europe), RegionName(RegionName.Pal),
     RegionName(RegionName.Worldwide), RegionLanguage(Language.Dutch)]
    Ne,

    [RegionName(RegionName.NorthAmerica), RegionName(RegionName.Worldwide)]
    Na,

    [RegionName(RegionName.Norway), RegionName(RegionName.Europe), RegionName(RegionName.Pal),
     RegionName(RegionName.Worldwide), RegionLanguage(Language.Norwegian)]
    No,

    [RegionName(RegionName.Pal), RegionName(RegionName.Worldwide)]
    Pal,

    [RegionName(RegionName.Russia), RegionName(RegionName.Pal), RegionName(RegionName.Worldwide),
     RegionLanguage(Language.Russian)]
    Ru,

    [RegionName(RegionName.Spain), RegionName(RegionName.Europe), RegionName(RegionName.Pal),
     RegionName(RegionName.Worldwide), RegionLanguage(Language.Spanish)]
    Sp,

    [RegionName(RegionName.Sweden), RegionName(RegionName.Europe), RegionName(RegionName.Pal),
     RegionName(RegionName.Worldwide), RegionLanguage(Language.Swedish)]
    Sw,

    [RegionName(RegionName.UnitedKingdom), RegionName(RegionName.Worldwide), RegionLanguage(Language.English)]
    Uk,

    [RegionName(RegionName.NorthAmerica), RegionName(RegionName.UnitedStates), RegionName(RegionName.Worldwide)]
    Usa,

    [RegionName(RegionName.Worldwide)] Ww
}

[AttributeUsage(AttributeTargets.Field, AllowMultiple = true)]
public class RegionNameAttribute(RegionName fullName) : Attribute
{
    public RegionName FullName { get; } = fullName;
}

public enum RegionName
{
    Australia,
    Brazil,
    Canada,
    China,
    Europe,
    France,
    Germany,
    Italy,
    Japan,
    Korea,
    Netherlands,
    NorthAmerica,
    Norway,
    Pal,
    Russia,
    Spain,
    Sweden,
    UnitedKingdom,
    UnitedStates,
    Worldwide
}

[AttributeUsage(AttributeTargets.Field | AttributeTargets.Method, AllowMultiple = true)]
public class RegionLanguageAttribute(Language language) : Attribute
{
    public Language Language { get; } = language;
}

public enum Language
{
    Chinese,
    Dutch,
    English,
    French,
    German,
    Italian,
    Japanese,
    Korean,
    Norwegian,
    Portuguese,
    Russian,
    Spanish,
    Swedish
}
