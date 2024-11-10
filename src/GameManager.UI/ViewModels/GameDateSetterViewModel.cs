using OmniApp.Common.Data;
using OmniApp.Common.Logging;
using OmniApp.UI.Common;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace GameManager.UI.ViewModels;

public class GameDateSetterViewModel : ViewModelBase
{
    private readonly IEnumerable<ICountry> _countryRegistry;
    private static readonly ICollection<Type> CommonCountries =
    [
            typeof(Australia),
            typeof(Brazil),
            typeof(Canada),
            typeof(China),
            typeof(EuropeanUnion),
            typeof(France),
            typeof(Germany),
            typeof(India),
            typeof(Italy),
            typeof(Japan),
            typeof(Netherlands),
            typeof(NorthAmerica),
            typeof(Portugal),
            typeof(Russia),
            typeof(SouthKorea),
            typeof(Spain),
            typeof(UnitedKingdom),
            typeof(UnitedStates)
    ];
    public ObservableCollection<string?> DateTexts { get; } = [];
    public ObservableCollection<string?> DatePickers { get; } = [];

    public GameDateSetterViewModel(CountryRegistry countryRegistry)
    {
        _countryRegistry = countryRegistry.GetAllCountries();
        InitializeAsync();
    }

    private async void InitializeAsync()
    {
        await PopulateDateTextsAsync();
        await PopulateDatePickersAsync();
    }

    private async Task PopulateDateTextsAsync()
    {
        Logger.Debug(LogClass.GameMgrUiViewModels, "Populating Date Texts...");
        DateTexts.Clear();

        foreach (ICountry? country in _countryRegistry)
        {
            if (CommonCountries.Contains(country.GetType()))
            {
                DateTexts.Add(country.Name);
            }
        }
    }

    private async Task PopulateDatePickersAsync()
    {
        Logger.Debug(LogClass.GameMgrUiViewModels, "Populating Date Pickers...");
        DatePickers.Clear();

        foreach (ICountry? country in _countryRegistry)
        {
            if (CommonCountries.Contains(country.GetType()))
            {
                DatePickers.Add(country.Name);
            }
        }
    }

    public void HandleKeyDown(object sender, KeyEventArgs e)
    {
        if (e.Key != Key.LeftShift)
        {
            return;
        }
        DateTexts.Clear();
        foreach (ICountry? country in _countryRegistry)
        {
            DateTexts.Add(country.GetType().GetProperty("Iso3166Code")?.GetValue(country)?.ToString() ?? country.Name);
        }
    }

    public void HandleKeyUp(object sender, KeyEventArgs e)
    {
        if (e.Key != Key.LeftShift)
        {
            return;
        }
        _ = PopulateDateTextsAsync();
    }
}
