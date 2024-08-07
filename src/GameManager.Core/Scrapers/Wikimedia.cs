using OmniApp.Common.Logging;
using System.Globalization;
using System.Text.RegularExpressions;

namespace GameManager.Core.Scrapers;

public partial class Wikimedia
{
    private const string DefaultUserAgent =
            "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/126.0.0.0 Safari/537.36";

    private static bool _hasPrintedCleanHtmlLog;
    private readonly string _baseUrl;
    private readonly HttpClient _httpClient;

    public Wikimedia(string baseUrl = "en.wikipedia.org")
    {
        _baseUrl = baseUrl;
        _httpClient = new HttpClient();
        _httpClient.Timeout = TimeSpan.FromSeconds(5);
        _httpClient.DefaultRequestHeaders.Add("User-Agent", DefaultUserAgent);
    }

    private async Task<Uri?> SearchWikipediaForPageAsync(string text)
    {
        Logger.Debug(LogClass.GameMgrCoreScrapersWikimedia, $"Searching wikipedia for page with text {text}");
        string searchUrl = $"https://{_baseUrl}/w/index.php?search={Uri.EscapeDataString(text)}";
        try
        {
            HttpResponseMessage response = await _httpClient.GetAsync(searchUrl);
            response.EnsureSuccessStatusCode();
            string responseContent = await response.Content.ReadAsStringAsync();

            if (response.RequestMessage.RequestUri.AbsolutePath.Contains("/wiki/"))
            {
                return response.RequestMessage.RequestUri;
            }

            Match match = WikipediaPageRegex().Match(responseContent);
            if (match.Success)
            {
                return new Uri($"https://{_baseUrl}/wiki/{match.Groups[1].Value}");
            }

            Logger.Error(LogClass.GameMgrCoreScrapersWikimedia, "No wikipedia page was found.");

            return null;
        }
        catch (HttpRequestException ex)
        {
            Logger.Error(LogClass.GameMgrCoreScrapersWikimedia, $"HTTP request error when searching Wikimedia: {ex.Message}");
            return null;
        }
    }

    public async Task<WikipediaPage?> ScrapePageAsync(Uri pageLink)
    {
        Logger.Info(LogClass.GameMgrCoreScrapersWikimedia, $"Trying to scrape wikipedia page with link {pageLink.ToString()}");
        try
        {
            Logger.Debug(LogClass.GameMgrCoreScrapersWikimedia, $"Sending GET request to Wikimedia URL {pageLink.ToString()}");
            string pageContent = await _httpClient.GetStringAsync(pageLink).ConfigureAwait(false);
            Logger.Debug(LogClass.GameMgrCoreScrapersWikimedia, "Received response from Wikimedia page.");

            string title = ExtractTitle(pageContent);
            Logger.Debug(LogClass.GameMgrCoreScrapersWikimedia, $"Extracted article header: {title}");

            Dictionary<string, string>? infobox = ExtractInfobox(pageContent);
            Logger.Debug(LogClass.GameMgrCoreScrapersWikimedia, "Infobox extracted.");

            return new WikipediaPage { Title = title, WikipediaLink = pageLink.ToString(), InfoboxData = infobox };
        }
        catch (HttpRequestException ex)
        {
            Logger.Error(LogClass.GameMgrCoreScrapersWikimedia, $"HTTP request error when scraping Wikimedia: {ex.Message}");
            return null;
        }
        catch (Exception ex)
        {
            Logger.Error(LogClass.GameMgrCoreScrapersWikimedia, $"Unexpected error when scraping Wikimedia: {ex.Message}");
            return null;
        }
    }

    private static string ExtractTitle(string pageContent)
    {
        Logger.Debug(LogClass.GameMgrCoreScrapersWikimedia, "Extracting article header from wikipedia page...");
        Match titleMatch = WikipediaArticleHeadingRegex().Match(pageContent);
        return titleMatch.Success ? titleMatch.Groups[1].Value : "Article header not found";
    }

    private static Dictionary<string, string>? ExtractInfobox(string pageContent)
    {
        Logger.Debug(LogClass.GameMgrCoreScrapersWikimedia, "Extracting infobox from wikipedia page...");
        Dictionary<string, string> infobox = new();
        Match infoboxMatch = InfoboxRegex().Match(pageContent);

        if (!infoboxMatch.Success)
        {
            Logger.Warning(LogClass.GameMgrCoreScrapersWikimedia, "No match found for an infobox.");
            return null;
        }

        string infoboxContent = infoboxMatch.Groups[0].Value;

        Logger.Debug(LogClass.GameMgrCoreScrapersWikimedia, $"Infobox HTML: {infoboxContent.Trim()}");

        MatchCollection rowMatches = InfoboxRowRegex().Matches(infoboxContent);

        foreach (Match rowMatch in rowMatches)
        {
            string key = CleanInfoboxKey(rowMatch.Groups[1].Value);
            string value = CleanInfoboxValue(rowMatch.Groups[2].Value);

            if (!string.IsNullOrEmpty(key) && !string.IsNullOrEmpty(value))
            {
                infobox[key] = value;
            }
        }

        // if (infobox.ContainsKey("release"))
        // {
            // infobox["release"] = FormatReleaseDates(infobox["release"]);
        // }

        string? developerKey = infobox.Keys.FirstOrDefault(k => k.Contains("developers", StringComparison.OrdinalIgnoreCase));

        if (developerKey != null)
        {
            Logger.Debug(LogClass.GameMgrCoreScrapersWikimedia, $"developerKey \"{developerKey}\" has found a match.");
            string newDevKey = CleanDevelopers(infobox[developerKey]);
            Logger.Debug(LogClass.GameMgrCoreScrapersWikimedia, $"The newDeveloperKey is \"{newDevKey}\"");
            infobox[developerKey] = newDevKey;
        }

        return infobox;
    }

    private static string CleanInfoboxKey(string key)
    {
        key = CleanHtml(key).Trim().ToLower();
        key = key.Replace("(s)", "s");
        return key;
    }

    private static string FormatReleaseDates(string releaseDates)
    {
        List<string> formattedDates = [];
        MatchCollection dateMatches = Regex.Matches(releaseDates, @"(\w+):\s*(\d+\s+\w+\s+\d{4})");

        foreach (Match match in dateMatches)
        {
            string region = match.Groups[1].Value.ToUpper();
            string dateString = match.Groups[2].Value;

            if (DateTime.TryParseExact(dateString, "d MMMM yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime date))
            {
                formattedDates.Add($"{region}: {date:yyyy-MM-dd}");
            }
        }

        return string.Join(", ", formattedDates);
    }

    private static string CleanDevelopers(string developers)
    {
        developers = Regex.Replace(developers, "^.*?(?=developers)", "");
        return developers.Trim();
    }

    private static string CleanInfoboxValue(string value)
    {
        value = CleanHtml(value).Trim();
        value = ReleaseDateRegex().Replace(value, string.Empty);
        value = value.Replace("\n", ", ");
        value = Regex.Replace(value, @"\s+", " ");
        value = Regex.Replace(value, @",\s*,", ",");
        value = value.Trim(',', ' ');
        return value;
    }

    private static string CleanHtml(string html)
    {
        if (!_hasPrintedCleanHtmlLog)
        {
            Logger.Debug(LogClass.GameMgrCoreScrapersWikimedia, "Cleaning unwanted HTML elements from string...");
            _hasPrintedCleanHtmlLog = true;
        }

        html = HtmlTagRegex().Replace(html, "%");
        html = CitationsRegex().Replace(html, string.Empty);
        html = CitationsUtf8Regex().Replace(html, string.Empty);
        html = NbspRegex().Replace(html, " ");
        html = html.Replace("<br />", ", ");
        return html.Trim();
    }

    [GeneratedRegex("""<table class="infobox[\s\S]+?">[\s\S]+?</table>""")]
    private static partial Regex InfoboxRegex();

    [GeneratedRegex(@"<tr><th[^>]*>([\s\S]+?)</th><td[^>]*>([\s\S]+?)</td></tr>")]
    private static partial Regex InfoboxRowRegex();

    [GeneratedRegex("""<a href="/wiki/([^"]+)" title="([^"]+)">""")]
    private static partial Regex WikipediaPageRegex();

    [GeneratedRegex("""<h1 id="firstHeading" class="firstHeading mw-first-heading"><i>(.+?)</i></h1>""")]
    private static partial Regex WikipediaArticleHeadingRegex();

    [GeneratedRegex("""<div class="plainlist">([\s\S]+?)</div>""")]
    private static partial Regex PlainlistRegex();

    [GeneratedRegex("<[^>]+>")]
    private static partial Regex HtmlTagRegex();

    [GeneratedRegex(@"\[.+?\]")]
    private static partial Regex CitationsRegex();

    [GeneratedRegex(@"\&\#91\;.+?\&\#93\;")]
    private static partial Regex CitationsUtf8Regex();

    [GeneratedRegex("&nbsp;")]
    private static partial Regex NbspRegex();

    [GeneratedRegex(".mw-parser-output.*?li{margin-bottom:0}")]
    private static partial Regex ReleaseDateRegex();
}

public class WikipediaPage
{
    public required string Title { get; init; }
    public required string WikipediaLink { get; init; }
    public Dictionary<string, string>? InfoboxData { get; init; } = new();
}
