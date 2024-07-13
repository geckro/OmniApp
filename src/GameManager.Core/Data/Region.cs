namespace GameManager.Core.Data;

/// <summary>
///     A list of regions that games commonly exclusively release in
/// </summary>
public enum Region
{
    [RegName(Name.Australia), RegName(Name.Worldwide), RegLang(Lang.English)]
    Au,

    [RegName(Name.Brazil), RegName(Name.Worldwide), RegName(Name.Pal), RegLang(Lang.Portuguese)]
    Br,

    [RegName(Name.NorthAmerica), RegName(Name.Canada), RegName(Name.Worldwide), RegLang(Lang.English)]
    Ca,

    [RegName(Name.China), RegName(Name.Worldwide), RegLang(Lang.Chinese)]
    Cn,

    [RegName(Name.Europe), RegName(Name.Pal), RegName(Name.Worldwide)]
    Eu,

    [RegName(Name.France), RegName(Name.Europe), RegName(Name.Pal), RegName(Name.Worldwide), RegLang(Lang.French)]
    Fr,

    [RegName(Name.Germany), RegName(Name.Europe), RegName(Name.Pal), RegName(Name.Worldwide), RegLang(Lang.German)]
    Ger,

    [RegName(Name.Italy), RegName(Name.Europe), RegName(Name.Pal), RegName(Name.Worldwide), RegLang(Lang.Italian)]
    It,

    [RegName(Name.Japan), RegName(Name.Worldwide), RegLang(Lang.Japanese)]
    Jp,

    [RegName(Name.Korea), RegName(Name.Worldwide), RegLang(Lang.Korean)]
    Ko,

    [RegName(Name.Netherlands), RegName(Name.Europe), RegName(Name.Pal), RegName(Name.Worldwide), RegLang(Lang.Dutch)]
    Ne,

    [RegName(Name.NorthAmerica), RegName(Name.Worldwide)]
    Na,

    [RegName(Name.Norway), RegName(Name.Europe), RegName(Name.Pal), RegName(Name.Worldwide), RegLang(Lang.Norwegian)]
    No,

    [RegName(Name.Pal), RegName(Name.Worldwide)]
    Pal,

    [RegName(Name.Russia), RegName(Name.Pal), RegName(Name.Worldwide), RegLang(Lang.Russian)]
    Ru,

    [RegName(Name.Spain), RegName(Name.Europe), RegName(Name.Pal), RegName(Name.Worldwide), RegLang(Lang.Spanish)]
    Sp,

    [RegName(Name.Sweden), RegName(Name.Europe), RegName(Name.Pal), RegName(Name.Worldwide), RegLang(Lang.Swedish)]
    Sw,

    [RegName(Name.UnitedKingdom), RegName(Name.Worldwide), RegLang(Lang.English)]
    Uk,

    [RegName(Name.NorthAmerica), RegName(Name.UnitedStates), RegName(Name.Worldwide)]
    Usa,

    [RegName(Name.Worldwide)] Ww
}

[AttributeUsage(AttributeTargets.Field, AllowMultiple = true)]
public class RegName(Name fullName) : Attribute
{
    public Name FullName { get; } = fullName;
}

public enum Name
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
public class RegLang(Lang lang) : Attribute
{
    public Lang Lang { get; } = lang;
}

public enum Lang
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
