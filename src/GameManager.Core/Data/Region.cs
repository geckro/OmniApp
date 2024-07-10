namespace GameManager.Core.Data;

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
