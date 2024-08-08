namespace OmniApp.Common.Data;

/// <summary>
///     A registry for methods to use for all countries registered with <see cref="CountryTypeRegistry" />.
/// </summary>
public class CountryRegistry
{
    private readonly Dictionary<string, ICountry> _countries;

    /// <summary>
    ///     Initializes an instance of the <see cref="CountryRegistry"/> class.
    /// </summary>
    /// <param name="countries">An IEnumerable of <see cref="ICountry"/>. </param>
    public CountryRegistry(IEnumerable<ICountry> countries)
    {
        _countries = countries.ToDictionary(c => c.Name);
    }

    /// <summary>
    ///     Gets all the country types currently registered with <see cref="CountryTypeRegistry"/>.
    /// </summary>
    /// <returns>An IEnumerable of <see cref="ICountry"/> with all the types registered.</returns>
    public IEnumerable<ICountry> GetAllCountries() => _countries.Values;

    /// <summary>
    ///     Gets a specific country.
    /// </summary>
    /// <param name="countryName">The <see cref="ICountry.Name"/> of the country.</param>
    /// <returns>An <see cref="ICountry"/> if the countryName was found, otherwise <c>null</c>.</returns>
    public ICountry? GetCountry(string countryName) => _countries.GetValueOrDefault(countryName);
}

/// <summary>
///    A registry of all types in <c>Countries.cs</c>. Country classes must be defined here for the program to use them.
/// </summary>
public record CountryTypeRegistry
{
    /// <summary>
    ///     All the registered country types.
    /// </summary>
    // an array here is much faster (nearly 4x) than an IEnumerable
    public static readonly Type[] CountryTypes =
    [
            typeof(Albania),
            typeof(Algeria),
            typeof(Argentina),
            typeof(Austria),
            typeof(Australia),
            typeof(Bangladesh),
            typeof(Belarus),
            typeof(Belgium),
            typeof(Bolivia),
            typeof(Brazil),
            typeof(Bulgaria),
            typeof(Canada),
            typeof(Chile),
            typeof(China),
            typeof(Colombia),
            typeof(CostaRica),
            typeof(Croatia),
            typeof(Cuba),
            typeof(Czechia),
            typeof(Denmark),
            typeof(DominicanRepublic),
            typeof(Ecuador),
            typeof(Egypt),
            typeof(ElSalvador),
            typeof(Estonia),
            typeof(EuropeanUnion),
            typeof(Finland),
            typeof(France),
            typeof(Germany),
            typeof(Greece),
            typeof(Guatemala),
            typeof(Haiti),
            typeof(Honduras),
            typeof(Hungary),
            typeof(India),
            typeof(Indonesia),
            typeof(Iran),
            typeof(Iraq),
            typeof(Ireland),
            typeof(Israel),
            typeof(Italy),
            typeof(Japan),
            typeof(Latvia),
            typeof(Libya),
            typeof(Lithuania),
            typeof(Luxembourg),
            typeof(Malaysia),
            typeof(Mexico),
            typeof(Moldova),
            typeof(Netherlands),
            typeof(NewZealand),
            typeof(Nicaragua),
            typeof(Nigeria),
            typeof(NorthKorea),
            typeof(Norway),
            typeof(Oman),
            typeof(Pakistan),
            typeof(Palestine),
            typeof(Panama),
            typeof(Paraguay),
            typeof(Peru),
            typeof(Poland),
            typeof(Portugal),
            typeof(Romania),
            typeof(Russia),
            typeof(SaudiArabia),
            typeof(Slovakia),
            typeof(SouthKorea),
            typeof(Spain),
            typeof(Sweden),
            typeof(Switzerland),
            typeof(Syria),
            typeof(Taiwan),
            typeof(Turkey),
            typeof(Ukraine),
            typeof(UnitedKingdom),
            typeof(UnitedStates),
            typeof(Uruguay),
            typeof(Venezuela),
            typeof(Yemen)
    ];
}
