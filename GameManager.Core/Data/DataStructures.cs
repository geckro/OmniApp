namespace GameManager.Core.Data;

public class Game(
    string title,
    Genre[]? genres = null,
    Platform[]? platforms = null,
    Developer[]? developers = null,
    Publisher[]? publishers = null,
    Series[]? series = null,

    DateTime? dateWw = null,
    DateTime? dateAu = null,
    DateTime? dateCa = null,
    DateTime? dateCh = null,
    DateTime? dateEu = null,
    DateTime? dateFr = null,
    DateTime? dateGe = null,
    DateTime? dateJp = null,
    DateTime? dateKo = null,
    DateTime? dateNa = null,
    DateTime? dateNo = null,
    DateTime? datePal = null,
    DateTime? dateRu = null,
    DateTime? dateSp = null,
    DateTime? dateSw = null,
    DateTime? dateUk = null,
    DateTime? dateUs = null,

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
    public Developer[]? Developers { get; } = developers;
    public Publisher[]? Publishers { get; } = publishers;
    public Series[]? Series { get; } = series;

    /// <summary>
    /// Default date if no region is specified
    /// </summary>
    public DateTime? DateWw { get; } = dateWw;

    /// <summary>
    /// Region-specific dates
    /// </summary>
    public DateTime? DateAu { get; } = dateAu;
    public DateTime? DateCa { get; } = dateCa;
    public DateTime? DateCh { get; } = dateCh;
    public DateTime? DateEu { get; } = dateEu;
    public DateTime? DateFr { get; } = dateFr;
    public DateTime? DateGe { get; } = dateGe;
    public DateTime? DateJp { get; } = dateJp;
    public DateTime? DateKo { get; } = dateKo;
    public DateTime? DateNa { get; } = dateNa;
    public DateTime? DateNo { get; } = dateNo;
    public DateTime? DatePal { get; } = datePal;
    public DateTime? DateRu { get; } = dateRu;
    public DateTime? DateSp { get; } = dateSp;
    public DateTime? DateSw { get; } = dateSw;
    public DateTime? DateUk { get; } = dateUk;
    public DateTime? DateUs { get; } = dateUs;

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