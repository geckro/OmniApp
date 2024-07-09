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