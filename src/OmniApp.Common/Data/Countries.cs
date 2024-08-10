namespace OmniApp.Common.Data;

/// <summary>
///     An interface for a current or past country.
/// </summary>
public interface ICountry
{
    /// <summary>
    ///     The name of the country, in English.
    /// </summary>
    string Name { get; }

    /// <summary>
    ///     The currency of the country, in ISO 4217 code format.
    /// </summary>
    string Currency { get; }

    /// <summary>
    ///     The currency title of the country, in ISO 4217 full name format.
    /// </summary>
    string CurrencyTitle { get; }
}

public record Albania : ICountry
{
    public string NativeName => "Shqipëri";
    public ushort CallingCode => 355;
    public string Currency => "ALD";
    public string CurrencyTitle => "Albanian Lek";
    public string Name => "Albania";
}

public record Algeria : ICountry
{
    public string NativeName => "الجزائر";
    public string NativeNameTranslation => "al-Jazāʾir";
    public string Currency => "DZD";
    public string CurrencyTitle => "Algerian Dinar";
    public string Name => "Algeria";
}

public record Argentina : ICountry
{
    public ushort CallingCode => 54;
    public string Currency => "ARS";
    public string CurrencyTitle => "Argentine Peso";
    public string Name => "Argentina";
}

public record Austria : ICountry
{
    public string NativeName => "Österreich";
    public string Demonym => "Austrian";
    public IEnumerable<string> Languages => ["German"];
    public IEnumerable<string> Regions => ["Europe"];
    public ushort CallingCode => 43;
    public string Iso3166Code => "AT";
    public string Currency => "EUR";
    public string CurrencyTitle => "Euro";
    public string Name => "Austria";
}

public record Australia : ICountry
{
    public string Demonym => "Australian";
    public IEnumerable<string> Languages => ["English"];
    public IEnumerable<string> Regions => ["Oceania"];
    public string Iso3166Code => "AU";
    public ushort CallingCode => 61;
    public string Currency => "AUD";
    public string CurrencyTitle => "Australian Dollar";
    public string Name => "Australia";
}

public record Bangladesh : ICountry
{
    public string NativeName => "বাংলাদেশ";
    public string NativeNameTranslation => "Bāṅlādēś";
    public string Currency => "BDT";
    public string CurrencyTitle => "Bangladeshi Taka";
    public string Name => "Bangladesh";
}

public record Belarus : ICountry
{
    public string NativeName => "Беларусь";
    public ushort CallingCode => 375;
    public string Currency => "BYN";
    public string CurrencyTitle => "Belarusian Ruble";
    public string Name => "Belarus";
}

public record Belgium : ICountry
{
    public string Demonym => "Belgian";
    public string NativeName => "Koninkrijk België";
    public IEnumerable<string> Languages => ["Dutch", "French", "German"];
    public ushort CallingCode => 32;
    public IEnumerable<string> Regions => ["Europe"];
    public string Iso3166Code => "BE";
    public string Currency => "EUR";
    public string CurrencyTitle => "Euro";
    public string Name => "Belgium";
}

public record Bolivia : ICountry
{
    public string Currency => "BOB";
    public string CurrencyTitle => "Boliviano";
    public string Name => "Bolivia";
}

public record Brazil : ICountry
{
    public string NativeName => "Brasil";
    public ushort CallingCode => 55;
    public string Currency => "BRL";
    public string CurrencyTitle => "Brazilian Real";
    public string Name => "Brazil";
}

public record Bulgaria : ICountry
{
    public string Demonym => "Bulgarian";
    public string NativeName => "България";
    public string NativeNameTranslation => "Bŭlgariya";
    public ushort CallingCode => 359;
    public string Currency => "BGN";
    public string CurrencyTitle => "Bulgarian Lev";
    public string Name => "Bulgaria";
}

public record Canada : ICountry
{
    public string Demonym => "Canadian";
    public IEnumerable<string> Languages => ["English"];
    public IEnumerable<string> Regions => ["America"];
    public string Iso3166Code => "CA";
    public ushort CallingCode => 1;
    public string Currency => "CAN";
    public string CurrencyTitle => "Canadian Dollar";
    public string Name => "Canada";
}

public record Chile : ICountry
{
    public ushort CallingCode => 56;
    public string Currency => "CLP";
    public string CurrencyTitle => "Chilean Peso";
    public string Name => "Chile";
}

public record China : ICountry
{
    public string NativeName => "中国";
    public string NativeNameTranslation => "Zhōngguó";
    public string Demonym => "Chinese";
    public IEnumerable<string> Languages => ["Chinese"];
    public IEnumerable<string> Regions => ["Asia"];
    public ushort CallingCode => 86;
    public string Iso3166Code => "CN";
    public string Currency => "CNY";
    public string CurrencyTitle => "Chinese Yuan";
    public string Name => "China (People's Republic)";
}

public record Colombia : ICountry
{
    public ushort CallingCode => 57;
    public string Currency => "COP";
    public string CurrencyTitle => "Colombian Peso";
    public string Name => "Colombia";
}

public record CostaRica : ICountry
{
    public ushort CallingCode => 506;
    public string Currency => "CRC";
    public string CurrencyTitle => "Costa Rican Colon";
    public string Name => "Costa Rica";
}

public record Croatia : ICountry
{
    public string NativeName => "Hrvatska";
    public ushort CallingCode => 385;
    public string Currency => "EUR";
    public string CurrencyTitle => "Euro";
    public string Name => "Croatia";
}

public record Cuba : ICountry
{
    public ushort CallingCode => 53;
    public string Currency => "CUP";
    public string CurrencyTitle => "Cuban Peso";
    public string Name => "Cuba";
}

public record Czechia : ICountry
{
    public string NativeName => "Česko";
    public ushort CallingCode => 420;
    public string Currency => "CZK";
    public string CurrencyTitle => "Czech Koruna";
    public string Name => "Czech Republic";
}

public record Denmark : ICountry
{
    public string NativeName => "Danmark";
    public ushort CallingCode => 45;
    public string Currency => "DKK";
    public string CurrencyTitle => "Danish Krone";
    public string Name => "Denmark";
}

public record DominicanRepublic : ICountry
{
    public string NativeName => "República Dominicana";
    public string Currency => "DOP";
    public string CurrencyTitle => "Dominican Peso";
    public string Name => "Dominican Republic";
}

public record Ecuador : ICountry
{
    public string Currency => "USD";
    public string CurrencyTitle => "United States Dollar";
    public string Name => "Ecuador";
}

public record Egypt : ICountry
{
    public ushort CallingCode => 20;
    public string NativeName => "مصر";
    public string NativeNameTranslation => "Miṣr";
    public string Currency => "EGP";
    public string CurrencyTitle => "Egyptian Pound";
    public string Name => "Egypt";
}

public record ElSalvador : ICountry
{
    public ushort CallingCode => 503;
    public string Currency => "SVC";
    public string CurrencyTitle => "Salvadoran Colon";
    public string Name => "El Salvador";
}

public record Estonia : ICountry
{
    public string NativeName => "Eesti";
    public ushort CallingCode => 372;
    public string Currency => "EUR";
    public string CurrencyTitle => "Euro";
    public string Name => "Estonia";
}

public record EuropeanUnion : ICountry
{
    public string Iso3166Code => "EU";
    public string Currency => "EUR";
    public string CurrencyTitle => "Euro";
    public string Name => "European Union";
}

public record Finland : ICountry
{
    public string NativeName => "Suomi";
    public ushort CallingCode => 358;
    public string Currency => "EUR";
    public string CurrencyTitle => "Euro";
    public string Name => "Finland";
}

public record France : ICountry
{
    public IEnumerable<string> Languages => ["French"];
    public IEnumerable<string> Regions => ["Europe"];
    public string Iso3166Code => "FR";
    public ushort CallingCode => 33;
    public string Currency => "EUR";
    public string CurrencyTitle => "Euro";
    public string Name => "France";
}

public record Germany : ICountry
{
    public string NativeName => "Deutschland";
    public string Demonym => "German";
    public IEnumerable<string> Languages => ["German"];
    public IEnumerable<string> Regions => ["Europe"];
    public string Iso3166Code => "DE";
    public ushort CallingCode => 49;
    public string Currency => "EUR";
    public string CurrencyTitle => "Euro";
    public string Name => "Germany";
}

public record Greece : ICountry
{
    public string Demonym => "Greek";
    public IEnumerable<string> Languages => ["Greek"];
    public IEnumerable<string> Regions => ["Europe"];
    public string Iso3166Code => "GR";
    public ushort CallingCode => 30;
    public string NativeName => "Ελλάδα";
    public string NativeNameTranslation => "Elláda";
    public string Currency => "EUR";
    public string CurrencyTitle => "Euro";
    public string Name => "Greece";
}

public record Guatemala : ICountry
{
    public ushort CallingCode => 502;
    public string Currency => "GTQ";
    public string CurrencyTitle => "Guatemalan Quetzal";
    public string Name => "Guatemala";
}

public record Haiti : ICountry
{
    public string NativeName => "Ayiti";
    public ushort CallingCode => 509;
    public string Currency => "HTG";
    public string CurrencyTitle => "Haitian Gourde";
    public string Name => "Haiti";
}

public record Honduras : ICountry
{
    public ushort CallingCode => 504;
    public string Currency => "HNL";
    public string CurrencyTitle => "Honduran Lempira";
    public string Name => "Honduras";
}

public record Hungary : ICountry
{
    public string NativeName => "Magyarország";
    public ushort CallingCode => 36;
    public string Currency => "HUF";
    public string CurrencyTitle => "Hungarian Forint";
    public string Name => "Hungary";
}

public record India : ICountry
{
    public string NativeName => "भारत";
    public string NativeNameTranslation => "Bhaarat";
    public ushort CallingCode => 91;
    public string Currency => "INR";
    public string CurrencyTitle => "Indian Rupee";
    public string Name => "India";
}

public record Indonesia : ICountry
{
    public ushort CallingCode => 62;
    public string Currency => "IDR";
    public string CurrencyTitle => "Indonesian Rupiah";
    public string Name => "Indonesia";
}

public record Iran : ICountry
{
    public string NativeName => "ایران";
    public ushort CallingCode => 98;
    public string Currency => "IRR";
    public string CurrencyTitle => "Iranian Rial";
    public string Name => "Iran";
}

public record Iraq : ICountry
{
    public string NativeName => "ٱلْعِرَاق";
    public string NativeNameTranslation => "Êraq";
    public string Currency => "IQD";
    public string CurrencyTitle => "Iraqi Dollar";
    public string Name => "Iraq";
}

public record Ireland : ICountry
{
    public string NativeName => "Éire";
    public ushort CallingCode => 353;
    public string Currency => "EUR";
    public string CurrencyTitle => "Euro";
    public string Name => "Ireland";
}

public record Israel : ICountry
{
    public string NativeName => "יִשְׂרָאֵל";
    public string NativeNameTranslation => "Yīsrāʾēl";
    public string Currency => "ILS";
    public string CurrencyTitle => "Israeli New Shekel";
    public string Name => "Israel";
}

public record Italy : ICountry
{
    public string NativeName => "Italia";
    public IEnumerable<string> Languages => ["Italian"];
    public IEnumerable<string> Regions => ["Europe"];
    public ushort CallingCode => 39;
    public string Iso3166Code => "IT";
    public string Currency => "EUR";
    public string CurrencyTitle => "Euro";
    public string Name => "Italy";
}

public record Japan : ICountry
{
    public string NativeName => "日本国";
    public string NativeNameTranslation => "Nihon";
    public string Demonym => "Japanese";
    public IEnumerable<string> Languages => ["Japanese"];
    public IEnumerable<string> Regions => ["Asia"];
    public ushort CallingCode => 81;
    public string Iso3166Code => "JP";
    public string Currency => "JPY";
    public string CurrencyTitle => "Japanese Yen";
    public string Name => "Japan";
}

public record Latvia : ICountry
{
    public string NativeName => "Latvijas";
    public ushort CallingCode => 371;
    public string Currency => "EUR";
    public string CurrencyTitle => "Euro";
    public string Name => "Latvia";
}

public record Libya : ICountry
{
    public string NativeName => "ليبيا";
    public string NativeNameTranslation => "Lībiyā";
    public string Currency => "LYD";
    public string CurrencyTitle => "Libyan Dinar";
    public string Name => "Libya";
}

public record Lithuania : ICountry
{
    public string NativeName => "Lietuva";
    public ushort CallingCode => 370;
    public string Currency => "EUR";
    public string CurrencyTitle => "Euro";
    public string Name => "Lithuania";
}

public record Luxembourg : ICountry
{
    public string NativeName => "Lëtzebuerg";
    public ushort CallingCode => 352;
    public string Currency => "EUR";
    public string CurrencyTitle => "Euro";
    public string Name => "Luxembourg";
}

public record Malaysia : ICountry
{
    public ushort CallingCode => 60;
    public string Currency => "MYR";
    public string CurrencyTitle => "Malaysian Ringgit";
    public string Name => "Malaysia";
}

public record Mexico : ICountry
{
    public ushort CallingCode => 52;
    public string Currency => "MXN";
    public string CurrencyTitle => "Mexican Peso";
    public string Name => "Mexico";
}

public record Moldova : ICountry
{
    public ushort CallingCode => 373;
    public string Currency => "MDL";
    public string CurrencyTitle => "Moldovan Leu";
    public string Name => "Moldova";
}

public record Netherlands : ICountry
{
    public string NativeName => "Nederland";
    public IEnumerable<string> Languages => ["Dutch"];
    public IEnumerable<string> Regions => ["Europe"];
    public string Iso3166Code => "NL";
    public ushort CallingCode => 31;
    public string Currency => "EUR";
    public string CurrencyTitle => "Euro";
    public string Name => "Netherlands";
}

public record NewZealand : ICountry
{
    public string NativeName => "Aotearoa";
    public ushort CallingCode => 64;
    public string Currency => "NZD";
    public string CurrencyTitle => "New Zealand Dollar";
    public string Name => "New Zealand";
}

public record Nicaragua : ICountry
{
    public ushort CallingCode => 505;
    public string Currency => "NIO";
    public string CurrencyTitle => "Nicaraguan Cordoba";
    public string Name => "Nicaragua";
}

public record Nigeria : ICountry
{
    public string NativeName => "Najeriya";
    public string Currency => "NGN";
    public string CurrencyTitle => "Nigerian Naira";
    public string Name => "Nigeria";
}

public record NorthAmerica : ICountry
{
    public string Iso3166Code => "NA";
    public string Currency => "N/A";
    public string CurrencyTitle => "N/A";
    public string Name => "North America";
}

public record NorthKorea : ICountry
{
    public string NativeName => "조선";
    public string NativeNameTranslation => "Chosŏn";
    public string Currency => "KPW";
    public string CurrencyTitle => "North Korean Won";
    public string Name => "North Korea";
}

public record Norway : ICountry
{
    public string NativeName => "Norge";
    public ushort CallingCode => 47;
    public string Currency => "NOK";
    public string CurrencyTitle => "Norwegian Krone";
    public string Name => "Norway";
}

public record Oman : ICountry
{
    public string NativeName => "عُمَان";
    public string NativeNameTranslation => "ʿUmān";
    public string Currency => "OMR";
    public string CurrencyTitle => "Omani Rial";
    public string Name => "Oman";
}

public record Pakistan : ICountry
{
    public string NativeName => "پَاکِسْتَان";
    public ushort CallingCode => 92;
    public string Currency => "PKR";
    public string CurrencyTitle => "Pakistani Rupee";
    public string Name => "Pakistan";
}

public record Palestine : ICountry
{
    public string NativeName => "فلسطين";
    public string NativeNameTranslation => "Filasṭīn";
    public string Currency => "N/A";
    public string CurrencyTitle => "N/A";
    public string Name => "Palestine";
}

public record Panama : ICountry
{
    public ushort CallingCode => 507;
    public string Currency => "PAB";
    public string CurrencyTitle => "Panamanian Balboa";
    public string Name => "Panama";
}

public record Paraguay : ICountry
{
    public string Currency => "PYG";
    public string CurrencyTitle => "Paraguayan Guarani";
    public string Name => "Paraguay";
}

public record Peru : ICountry
{
    public ushort CallingCode => 51;
    public string Currency => "PEN";
    public string CurrencyTitle => "Peruvian Sol";
    public string Name => "Peru";
}

public record Poland : ICountry
{
    public string NativeName => "Polska";
    public IEnumerable<string> Languages => ["Polish"];
    public IEnumerable<string> Regions => ["Europe"];
    public string Iso3166Code => "PL";
    public ushort CallingCode => 48;
    public string Currency => "PLN";
    public string CurrencyTitle => "Polish Zloty";
    public string Name => "Poland";
}

public record Portugal : ICountry
{
    public IEnumerable<string> Languages => ["Portugese"];
    public IEnumerable<string> Regions => ["Europe"];
    public string Iso3166Code => "PT";
    public ushort CallingCode => 351;
    public string Currency => "EUR";
    public string CurrencyTitle => "Euro";
    public string Name => "Portugal";
}

public record Romania : ICountry
{
    public string NativeName => "România";
    public ushort CallingCode => 40;
    public string Currency => "RON";
    public string CurrencyTitle => "Romanian Leu";
    public string Name => "Romania";
}

public record Russia : ICountry
{
    public string NativeName => "Россия";
    public string NativeNameTranslation => "Rossiya";
    public ushort CallingCode => 7;
    public string Currency => "RUB";
    public string CurrencyTitle => "Russian Ruble";
    public string Name => "Russia";
}

public record SaudiArabia : ICountry
{
    public string NativeName => "ٱلسُّعُودِيَّة";
    public string NativeNameTranslation => "Suʿūdiyya";
    public string Currency => "SAR";
    public string CurrencyTitle => "Saudi Riyal";
    public string Name => "Saudi Arabia";
}

public record Slovakia : ICountry
{
    public string NativeName => "Slovensko";
    public ushort CallingCode => 421;
    public string Currency => "EUR";
    public string CurrencyTitle => "Euro";
    public string Name => "Slovakia";
}

public record SouthKorea : ICountry
{
    public string NativeName => "한국";
    public string NativeNameTranslation => "Hanguk";
    public ushort CallingCode => 82;
    public string Currency => "KRW";
    public string CurrencyTitle => "South Korean Won";
    public string Name => "South Korea";
}

public record Spain : ICountry
{
    public string NativeName => "España";
    public IEnumerable<string> Languages => ["Spanish"];
    public IEnumerable<string> Regions => ["Europe"];
    public ushort CallingCode => 34;
    public string Iso3166Code => "ES";
    public string Currency => "EUR";
    public string CurrencyTitle => "Euro";
    public string Name => "Spain";
}

public record Sweden : ICountry
{
    public string NativeName => "Sverige";
    public ushort CallingCode => 46;
    public string Currency => "SEK";
    public string CurrencyTitle => "Swedish Krona";
    public string Name => "Sweden";
}

public record Switzerland : ICountry
{
    public string NativeName => "Schweiz";
    public ushort CallingCode => 41;
    public string Currency => "CHE";
    public string CurrencyTitle => "WIR Euro";
    public string Name => "Switzerland";
}

public record Syria : ICountry
{
    public string NativeName => "سُورِيَا";
    public string NativeNameTranslation => "Sūriyā";
    public string Currency => "SYP";
    public string CurrencyTitle => "Syrian Pound";
    public string Name => "Syria";
}

public record Taiwan : ICountry
{
    public string NativeName => "中華";
    public string NativeNameTranslation => "Zhōnghuá Mínguó";
    public ushort CallingCode => 886;
    public string Currency => "TWD";
    public string CurrencyTitle => "New Taiwan Dollar";
    public string Name => "Taiwan";
}

public record Turkey : ICountry
{
    public string NativeName => "Türkiye";
    public ushort CallingCode => 90;
    public string Currency => "TRY";
    public string CurrencyTitle => "Turkish Lira";
    public string Name => "Turkey";
}

public record Ukraine : ICountry
{
    public string NativeName => "Україна";
    public string NativeNameTranslation => "Ukraina";
    public ushort CallingCode => 380;
    public string Currency => "UAH";
    public string CurrencyTitle => "Ukrainian Hryvnia";
    public string Name => "Ukraine";
}

public record UnitedKingdom : ICountry
{
    public string Demonym => "British";
    public IEnumerable<string> Languages => ["English", "Irish", "Welsh", "Scottish Gaelic"];
    public IEnumerable<string> Regions => ["Europe"];
    public string Iso3166Code => "GB";
    public ushort CallingCode => 44;
    public string Currency => "GBP";
    public string CurrencyTitle => "Pound Sterling";
    public string Name => "United Kingdom";
}

public record UnitedStates : ICountry
{
    public string Demonym => "American";
    public IEnumerable<string> Languages => ["English", "Spanish"];
    public IEnumerable<string> Regions => ["America", "North America"];
    public string Iso3166Code => "US";
    public ushort CallingCode => 1;
    public string Currency => "USD";
    public string CurrencyTitle => "United States Dollar";
    public string Name => "United States of America";
}

public record Uruguay : ICountry
{
    public string Currency => "UYU";
    public string CurrencyTitle => "Uruguayan Peso";
    public string Name => "Uruguay";
}

public record Venezuela : ICountry
{
    public ushort CallingCode => 58;
    public string Currency => "VES";
    public string CurrencyTitle => "Venezuelan Sovereign Bolivar";
    public string Name => "Venezuela";
}

public record Yemen : ICountry
{
    public string NativeName => "ٱلْيَمَنْ";
    public string NativeNameTranslation => "al-Yaman";
    public string Currency => "YER";
    public string CurrencyTitle => "Yemeni Rial";
    public string Name => "Yemen";
}
