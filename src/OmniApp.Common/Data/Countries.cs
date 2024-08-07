namespace OmniApp.Common.Data;

public interface ICountry
{
    string Name { get; }
}

public class Albania : ICountry
{
    public string NativeName => "Shqipëri";
    public string Name => "Albania";
}

public class Algeria : ICountry
{
    public string NativeName => "الجزائر";
    public string NativeNameTranslation => "al-Jazāʾir";
    public string Name => "Algeria";
}

public class Argentina : ICountry
{
    public string Name => "Argentina";
}

public class Austria : ICountry
{
    public string NativeName => "Österreich";
    public string Demonym => "Austrian";
    public IEnumerable<string> Languages => ["German"];
    public IEnumerable<string> Regions => ["Europe"];
    public string Iso3166Code => "AT";
    public string Currency => "EUR";
    public string Name => "Austria";
}

public class Australia : ICountry
{
    public string Demonym => "Australian";
    public IEnumerable<string> Languages => ["English"];
    public IEnumerable<string> Regions => ["Oceania"];
    public string Iso3166Code => "AU";
    public string Currency => "AUD";
    public string Name => "Australia";
}

public class Bangladesh : ICountry
{
    public string NativeName => "বাংলাদেশ";
    public string NativeNameTranslation => "Bāṅlādēś";
    public string Name => "Bangladesh";
}

public class Belarus : ICountry
{
    public string NativeName => "Беларусь";
    public string Name => "Belarus";
}

public class Belgium : ICountry
{
    public string Demonym => "Belgian";
    public string NativeName => "Koninkrijk België";
    public IEnumerable<string> Languages => ["Dutch", "French", "German"];
    public IEnumerable<string> Regions => ["Europe"];
    public string Iso3166Code => "BE";
    public string Currency => "EUR";
    public string Name => "Belgium";
}

public class Bolivia : ICountry
{
    public string Name => "Bolivia";
}

public class Brazil : ICountry
{
    public string NativeName => "Brasil";
    public string Name => "Brazil";
}

public class Bulgaria : ICountry
{
    public string Demonym => "Bulgarian";
    public string NativeName => "България";
    public string NativeNameTranslation => "Bŭlgariya";
    public string Name => "Bulgaria";
}

public class Canada : ICountry
{
    public string Demonym => "Canadian";
    public IEnumerable<string> Languages => ["English"];
    public IEnumerable<string> Regions => ["America"];
    public string Iso3166Code => "CN";
    public string Currency => "CAD";
    public string Name => "Canada";
}

public class Chile : ICountry
{
    public string Name => "Chile";
}

public class China : ICountry
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

public class Colombia : ICountry
{
    public string Name => "Colombia";
}

public class CostaRica : ICountry
{
    public string Name => "Costa Rica";
}

public class Croatia : ICountry
{
    public string NativeName => "Hrvatska";
    public string Name => "Croatia";
}

public class Cuba : ICountry
{
    public string Name => "Cuba";
}

public class Czechia : ICountry
{
    public string NativeName => "Česko";
    public string Name => "Czech Republic";
}

public class Denmark : ICountry
{
    public string NativeName => "Danmark";
    public string Name => "Denmark";
}

public class DominicanRepublic : ICountry
{
    public string NativeName => "República Dominicana";
    public string Name => "Dominican Republic";
}

public class Ecuador : ICountry
{
    public string Name => "Ecuador";
}

public class Egypt : ICountry
{
    public string NativeName => "مصر";
    public string NativeNameTranslation => "Miṣr";
    public string Name => "Egypt";
}

public class ElSalvador : ICountry
{
    public string Name => "El Salvador";
}

public class Estonia : ICountry
{
    public string NativeName => "Eesti";
    public string Name => "Estonia";
}

public class Finland : ICountry
{
    public string NativeName => "Suomi";
    public string Name => "Finland";
}

public class France : ICountry
{
    public string Name => "France";
}

public class Germany : ICountry
{
    public string NativeName => "Deutschland";
    public string Demonym => "German";
    public IEnumerable<string> Languages => ["German"];
    public IEnumerable<string> Regions => ["Europe"];
    public string Iso3166Code => "DE";
    public string Currency => "EUR";
    public string Name => "Germany";
}

public class Greece : ICountry
{
    public string NativeName => "Ελλάδα";
    public string NativeNameTranslation => "Elláda";
    public string Name => "Greece";
}

public class Guatemala : ICountry
{
    public string Name => "Guatemala";
}

public class Haiti : ICountry
{
    public string NativeName => "Ayiti";
    public string Name => "Haiti";
}

public class Honduras : ICountry
{
    public string Name => "Honduras";
}

public class Hungary : ICountry
{
    public string NativeName => "Magyarország";
    public string Name => "Hungary";
}

public class India : ICountry
{
    public string NativeName => "भारत";
    public string NativeNameTranslation => "Bhaarat";
    public string Name => "India";
}

public class Indonesia : ICountry
{
    public string Name => "Indonesia";
}

public class Iran : ICountry
{
    public string NativeName => "ایران";
    public string Name => "Iran";
}

public class Iraq : ICountry
{
    public string NativeName => "ٱلْعِرَاق";
    public string NativeNameTranslation => "Êraq";
    public string Name => "Iraq";
}

public class Ireland : ICountry
{
    public string NativeName => "Éire";
    public string Name => "Ireland";
}

public class Israel : ICountry
{
    public string NativeName => "יִשְׂרָאֵל";
    public string NativeNameTranslation => "Yīsrāʾēl";
    public string Name => "Israel";
}

public class Italy : ICountry
{
    public string NativeName => "Italia";
    public IEnumerable<string> Languages => ["Italian"];
    public IEnumerable<string> Regions => ["Europe"];
    public string Iso3166Code => "IT";
    public string Currency => "EUR";
    public string Name => "Italy";
}

public class Japan : ICountry
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

public class Latvia : ICountry
{
    public string NativeName => "Latvijas";
    public string Name => "Latvia";
}

public class Libya : ICountry
{
    public string NativeName => "ليبيا";
    public string NativeNameTranslation => "Lībiyā";
    public string Name => "Libya";
}

public class Lithuania : ICountry
{
    public string NativeName => "Lietuva";
    public string Name => "Lithuania";
}

public class Luxembourg : ICountry
{
    public string NativeName => "Lëtzebuerg";
    public string Name => "Luxembourg";
}

public class Malaysia : ICountry
{
    public string Name => "Malaysia";
}

public class Mexico : ICountry
{
    public string Name => "Mexico";
}

public class Moldova : ICountry
{
    public string Name => "Moldova";
}

public class Netherlands : ICountry
{
    public string NativeName => "Nederland";
    public IEnumerable<string> Languages => ["Dutch"];
    public IEnumerable<string> Regions => ["Europe"];
    public string Iso3166Code => "NL";
    public string Currency => "EUR";
    public string Name => "Netherlands";
}

public class NewZealand : ICountry
{
    public string NativeName => "Aotearoa";
    public string Name => "New Zealand";
}

public class Nicaragua : ICountry
{
    public string Name => "Nicaragua";
}

public class Nigeria : ICountry
{
    public string NativeName => "Najeriya";
    public string Name => "Nigeria";
}

public class NorthKorea : ICountry
{
    public string NativeName => "조선";
    public string NativeNameTranslation => "Chosŏn";
    public string Name => "North Korea";
}

public class Norway : ICountry
{
    public string NativeName => "Norge";
    public string Name => "Norway";
}

public class Oman : ICountry
{
    public string NativeName => "عُمَان";
    public string NativeNameTranslation => "ʿUmān";
    public string Name => "Oman";
}

public class Pakistan : ICountry
{
    public string NativeName => "پَاکِسْتَان";
    public string Name => "Pakistan";
}

public class Palestine : ICountry
{
    public string NativeName => "فلسطين";
    public string NativeNameTranslation => "Filasṭīn";
    public string Name => "Palestine";
}

public class Panama : ICountry
{
    public string Name => "Panama";
}

public class Paraguay : ICountry
{
    public string Name => "Paraguay";
}

public class Peru : ICountry
{
    public string Name => "Peru";
}

public class Poland : ICountry
{
    public string NativeName => "Polska";
    public IEnumerable<string> Languages => ["Polish"];
    public IEnumerable<string> Regions => ["Europe"];
    public string Iso3166Code => "PL";
    public string Currency => "PLN";
    public string Name => "Poland";
}

public class Portugal : ICountry
{
    public IEnumerable<string> Languages => ["Portugese"];
    public IEnumerable<string> Regions => ["Europe"];
    public string Iso3166Code => "PT";
    public string Currency => "EUR";
    public string Name => "Portugal";
}

public class Romania : ICountry
{
    public string NativeName => "România";
    public string Name => "Romania";
}

public class Russia : ICountry
{
    public string NativeName => "Россия";
    public string NativeNameTranslation => "Rossiya";
    public string Name => "Russia";
}

public class SaudiArabia : ICountry
{
    public string NativeName => "ٱلسُّعُودِيَّة";
    public string NativeNameTranslation => "Suʿūdiyya";
    public string Name => "Saudi Arabia";
}

public class Slovakia : ICountry
{
    public string NativeName => "Slovensko";
    public string Name => "Slovakia";
}

public class SouthKorea : ICountry
{
    public string NativeName => "한국";
    public string NativeNameTranslation => "Hanguk";
    public string Name => "South Korea";
}

public class Spain : ICountry
{
    public string NativeName => "España";
    public IEnumerable<string> Languages => ["Spanish"];
    public IEnumerable<string> Regions => ["Europe"];
    public string Iso3166Code => "ES";
    public string Currency => "EUR";
    public string Name => "Spain";
}

public class Sweden : ICountry
{
    public string NativeName => "Sverige";
    public string Name => "Sweden";
}

public class Switzerland : ICountry
{
    public string NativeName => "Schweiz";
    public string Name => "Switzerland";
}

public class Syria : ICountry
{
    public string NativeName => "سُورِيَا";
    public string NativeNameTranslation => "Sūriyā";
    public string Name => "Syria";
}

public class Taiwan : ICountry
{
    public string NativeName => "中華";
    public string NativeNameTranslation => "Zhōnghuá Mínguó";
    public string Name => "Taiwan";
}

public class Turkey : ICountry
{
    public string NativeName => "Türkiye";
    public string Name => "Turkey";
}

public class Ukraine : ICountry
{
    public string NativeName => "Україна";
    public string NativeNameTranslation => "Ukraina";
    public string Name => "Ukraine";
}

public class UnitedKingdom : ICountry
{
    public string Demonym => "British";
    public IEnumerable<string> Languages => ["English"];
    public IEnumerable<string> Regions => ["Europe"];
    public string Iso3166Code => "GB";
    public string Currency => "GBP";
    public string Name => "United Kingdom";
}

public class UnitedStates : ICountry
{
    public string Demonym => "American";
    public IEnumerable<string> Languages => ["English"];
    public IEnumerable<string> Regions => ["America", "South America"];
    public string Iso3166Code => "US";
    public string Currency => "USD";
    public string Name => "United States of America";
}

public class Uruguay : ICountry
{
    public string Name => "Uruguay";
}

public class Venezuela : ICountry
{
    public string Name => "Venezuela";
}

public class Yemen : ICountry
{
    public string NativeName => "ٱلْيَمَنْ";
    public string NativeNameTranslation => "al-Yaman";
    public string Name => "Yemen";
}
