namespace OmniApp.Common.Data;

public class CountryRegistry
{
    private readonly Dictionary<string, ICountry> _countries;

    public CountryRegistry(IEnumerable<ICountry> countries)
    {
        _countries = countries.ToDictionary(c => c.Name);
    }

    public IEnumerable<ICountry> GetAllCountries() => _countries.Values;
    public ICountry? GetCountry(string name) => _countries.GetValueOrDefault(name);
}

public static class CountryTypeRegistry
{
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
