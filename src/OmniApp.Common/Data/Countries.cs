namespace OmniApp.Common.Data;

public interface ICountry
{
    string Name { get; }
}

public record Albania : ICountry
{
    public string NativeName => "Shqipëri";
    public string Name => "Albania";
}

public record Algeria : ICountry
{
    public string NativeName => "الجزائر";
    public string NativeNameTranslation => "al-Jazāʾir";
    public string Name => "Algeria";
}

public record Argentina : ICountry
{
    public string Name => "Argentina";
}

public record Austria : ICountry
{
    public string NativeName => "Österreich";
    public string Demonym => "Austrian";
    public IEnumerable<string> Languages => ["German"];
    public IEnumerable<string> Regions => ["Europe"];
    public string Iso3166Code => "AT";
    public string Currency => "EUR";
    public string Name => "Austria";
}

public record Australia : ICountry
{
    public string Demonym => "Australian";
    public IEnumerable<string> Languages => ["English"];
    public IEnumerable<string> Regions => ["Oceania"];
    public string Iso3166Code => "AU";
    public string Currency => "AUD";
    public string Name => "Australia";
}

public record Bangladesh : ICountry
{
    public string NativeName => "বাংলাদেশ";
    public string NativeNameTranslation => "Bāṅlādēś";
    public string Name => "Bangladesh";
}

public record Belarus : ICountry
{
    public string NativeName => "Беларусь";
    public string Name => "Belarus";
}

public record Belgium : ICountry
{
    public string Demonym => "Belgian";
    public string NativeName => "Koninkrijk België";
    public IEnumerable<string> Languages => ["Dutch", "French", "German"];
    public IEnumerable<string> Regions => ["Europe"];
    public string Iso3166Code => "BE";
    public string Currency => "EUR";
    public string Name => "Belgium";
}

public record Bolivia : ICountry
{
    public string Name => "Bolivia";
}

public record Brazil : ICountry
{
    public string NativeName => "Brasil";
    public string Name => "Brazil";
}

public record Bulgaria : ICountry
{
    public string Demonym => "Bulgarian";
    public string NativeName => "България";
    public string NativeNameTranslation => "Bŭlgariya";
    public string Name => "Bulgaria";
}

public record Canada : ICountry
{
    public string Demonym => "Canadian";
    public IEnumerable<string> Languages => ["English"];
    public IEnumerable<string> Regions => ["America"];
    public string Iso3166Code => "CN";
    public string Currency => "CAD";
    public string Name => "Canada";
}

public record Chile : ICountry
{
    public string Name => "Chile";
}

public record China : ICountry
{
    public string NativeName => "中国";
    public string NativeNameTranslation => "Zhōngguó";
    public string Demonym => "Chinese";
    public IEnumerable<string> Languages => ["Chinese"];
    public IEnumerable<string> Regions => ["Asia"];
    public string Iso3166Code => "CN";
    public string Currency => "CNY";
    public string Name => "China (People's Republic)";
}

public record Colombia : ICountry
{
    public string Name => "Colombia";
}

public record CostaRica : ICountry
{
    public string Name => "Costa Rica";
}

public record Croatia : ICountry
{
    public string NativeName => "Hrvatska";
    public string Name => "Croatia";
}

public record Cuba : ICountry
{
    public string Name => "Cuba";
}

public record Czechia : ICountry
{
    public string NativeName => "Česko";
    public string Name => "Czech Republic";
}

public record Denmark : ICountry
{
    public string NativeName => "Danmark";
    public string Name => "Denmark";
}

public record DominicanRepublic : ICountry
{
    public string NativeName => "República Dominicana";
    public string Name => "Dominican Republic";
}

public record Ecuador : ICountry
{
    public string Name => "Ecuador";
}

public record Egypt : ICountry
{
    public string NativeName => "مصر";
    public string NativeNameTranslation => "Miṣr";
    public string Name => "Egypt";
}

public record ElSalvador : ICountry
{
    public string Name => "El Salvador";
}

public record Estonia : ICountry
{
    public string NativeName => "Eesti";
    public string Name => "Estonia";
}

public record Finland : ICountry
{
    public string NativeName => "Suomi";
    public string Name => "Finland";
}

public record France : ICountry
{
    public string Name => "France";
}

public record Germany : ICountry
{
    public string NativeName => "Deutschland";
    public string Demonym => "German";
    public IEnumerable<string> Languages => ["German"];
    public IEnumerable<string> Regions => ["Europe"];
    public string Iso3166Code => "DE";
    public string Currency => "EUR";
    public string Name => "Germany";
}

public record Greece : ICountry
{
    public string NativeName => "Ελλάδα";
    public string NativeNameTranslation => "Elláda";
    public string Name => "Greece";
}

public record Guatemala : ICountry
{
    public string Name => "Guatemala";
}

public record Haiti : ICountry
{
    public string NativeName => "Ayiti";
    public string Name => "Haiti";
}

public record Honduras : ICountry
{
    public string Name => "Honduras";
}

public record Hungary : ICountry
{
    public string NativeName => "Magyarország";
    public string Name => "Hungary";
}

public record India : ICountry
{
    public string NativeName => "भारत";
    public string NativeNameTranslation => "Bhaarat";
    public string Name => "India";
}

public record Indonesia : ICountry
{
    public string Name => "Indonesia";
}

public record Iran : ICountry
{
    public string NativeName => "ایران";
    public string Name => "Iran";
}

public record Iraq : ICountry
{
    public string NativeName => "ٱلْعِرَاق";
    public string NativeNameTranslation => "Êraq";
    public string Name => "Iraq";
}

public record Ireland : ICountry
{
    public string NativeName => "Éire";
    public string Name => "Ireland";
}

public record Israel : ICountry
{
    public string NativeName => "יִשְׂרָאֵל";
    public string NativeNameTranslation => "Yīsrāʾēl";
    public string Name => "Israel";
}

public record Italy : ICountry
{
    public string NativeName => "Italia";
    public IEnumerable<string> Languages => ["Italian"];
    public IEnumerable<string> Regions => ["Europe"];
    public string Iso3166Code => "IT";
    public string Currency => "EUR";
    public string Name => "Italy";
}

public record Japan : ICountry
{
    public string NativeName => "日本国";
    public string NativeNameTranslation => "Nihon";
    public string Demonym => "Japanese";
    public IEnumerable<string> Languages => ["Japanese"];
    public IEnumerable<string> Regions => ["Asia"];
    public string Iso3166Code => "JP";
    public string Currency => "JPY";
    public string Name => "Japan";
}

public record Latvia : ICountry
{
    public string NativeName => "Latvijas";
    public string Name => "Latvia";
}

public record Libya : ICountry
{
    public string NativeName => "ليبيا";
    public string NativeNameTranslation => "Lībiyā";
    public string Name => "Libya";
}

public record Lithuania : ICountry
{
    public string NativeName => "Lietuva";
    public string Name => "Lithuania";
}

public record Luxembourg : ICountry
{
    public string NativeName => "Lëtzebuerg";
    public string Name => "Luxembourg";
}

public record Malaysia : ICountry
{
    public string Name => "Malaysia";
}

public record Mexico : ICountry
{
    public string Name => "Mexico";
}

public record Moldova : ICountry
{
    public string Name => "Moldova";
}

public record Netherlands : ICountry
{
    public string NativeName => "Nederland";
    public IEnumerable<string> Languages => ["Dutch"];
    public IEnumerable<string> Regions => ["Europe"];
    public string Iso3166Code => "NL";
    public string Currency => "EUR";
    public string Name => "Netherlands";
}

public record NewZealand : ICountry
{
    public string NativeName => "Aotearoa";
    public string Name => "New Zealand";
}

public record Nicaragua : ICountry
{
    public string Name => "Nicaragua";
}

public record Nigeria : ICountry
{
    public string NativeName => "Najeriya";
    public string Name => "Nigeria";
}

public record NorthKorea : ICountry
{
    public string NativeName => "조선";
    public string NativeNameTranslation => "Chosŏn";
    public string Name => "North Korea";
}

public record Norway : ICountry
{
    public string NativeName => "Norge";
    public string Name => "Norway";
}

public record Oman : ICountry
{
    public string NativeName => "عُمَان";
    public string NativeNameTranslation => "ʿUmān";
    public string Name => "Oman";
}

public record Pakistan : ICountry
{
    public string NativeName => "پَاکِسْتَان";
    public string Name => "Pakistan";
}

public record Palestine : ICountry
{
    public string NativeName => "فلسطين";
    public string NativeNameTranslation => "Filasṭīn";
    public string Name => "Palestine";
}

public record Panama : ICountry
{
    public string Name => "Panama";
}

public record Paraguay : ICountry
{
    public string Name => "Paraguay";
}

public record Peru : ICountry
{
    public string Name => "Peru";
}

public record Poland : ICountry
{
    public string NativeName => "Polska";
    public IEnumerable<string> Languages => ["Polish"];
    public IEnumerable<string> Regions => ["Europe"];
    public string Iso3166Code => "PL";
    public string Currency => "PLN";
    public string Name => "Poland";
}

public record Portugal : ICountry
{
    public IEnumerable<string> Languages => ["Portugese"];
    public IEnumerable<string> Regions => ["Europe"];
    public string Iso3166Code => "PT";
    public string Currency => "EUR";
    public string Name => "Portugal";
}

public record Romania : ICountry
{
    public string NativeName => "România";
    public string Name => "Romania";
}

public record Russia : ICountry
{
    public string NativeName => "Россия";
    public string NativeNameTranslation => "Rossiya";
    public string Name => "Russia";
}

public record SaudiArabia : ICountry
{
    public string NativeName => "ٱلسُّعُودِيَّة";
    public string NativeNameTranslation => "Suʿūdiyya";
    public string Name => "Saudi Arabia";
}

public record Slovakia : ICountry
{
    public string NativeName => "Slovensko";
    public string Name => "Slovakia";
}

public record SouthKorea : ICountry
{
    public string NativeName => "한국";
    public string NativeNameTranslation => "Hanguk";
    public string Name => "South Korea";
}

public record Spain : ICountry
{
    public string NativeName => "España";
    public IEnumerable<string> Languages => ["Spanish"];
    public IEnumerable<string> Regions => ["Europe"];
    public string Iso3166Code => "ES";
    public string Currency => "EUR";
    public string Name => "Spain";
}

public record Sweden : ICountry
{
    public string NativeName => "Sverige";
    public string Name => "Sweden";
}

public record Switzerland : ICountry
{
    public string NativeName => "Schweiz";
    public string Name => "Switzerland";
}

public record Syria : ICountry
{
    public string NativeName => "سُورِيَا";
    public string NativeNameTranslation => "Sūriyā";
    public string Name => "Syria";
}

public record Taiwan : ICountry
{
    public string NativeName => "中華";
    public string NativeNameTranslation => "Zhōnghuá Mínguó";
    public string Name => "Taiwan";
}

public record Turkey : ICountry
{
    public string NativeName => "Türkiye";
    public string Name => "Turkey";
}

public record Ukraine : ICountry
{
    public string NativeName => "Україна";
    public string NativeNameTranslation => "Ukraina";
    public string Name => "Ukraine";
}

public record UnitedKingdom : ICountry
{
    public string Demonym => "British";
    public IEnumerable<string> Languages => ["English"];
    public IEnumerable<string> Regions => ["Europe"];
    public string Iso3166Code => "GB";
    public string Currency => "GBP";
    public string Name => "United Kingdom";
}

public record UnitedStates : ICountry
{
    public string Demonym => "American";
    public IEnumerable<string> Languages => ["English"];
    public IEnumerable<string> Regions => ["America", "South America"];
    public string Iso3166Code => "US";
    public string Currency => "USD";
    public string Name => "United States of America";
}

public record Uruguay : ICountry
{
    public string Name => "Uruguay";
}

public record Venezuela : ICountry
{
    public string Name => "Venezuela";
}

public record Yemen : ICountry
{
    public string NativeName => "ٱلْيَمَنْ";
    public string NativeNameTranslation => "al-Yaman";
    public string Name => "Yemen";
}
