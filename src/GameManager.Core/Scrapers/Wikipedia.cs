using OmniApp.Common.Logging;
using System.Text.RegularExpressions;

namespace GameManager.Core.Scrapers;

public partial class Wikipedia
{
    private const string DefaultUserAgent = "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/126.0.0.0 Safari/537.36";
    private readonly string _baseUrl;
    private readonly HttpClient _httpClient;

    public Wikipedia(string baseUrl = "en.wikipedia.org")
    {
        _baseUrl = baseUrl;
        _httpClient = new HttpClient();
        _httpClient.Timeout = TimeSpan.FromSeconds(5);
        _httpClient.DefaultRequestHeaders.Add("User-Agent", DefaultUserAgent);
    }

    private async Task<Uri?> SearchWikipediaForPageAsync(string text)
    {
        Logger.Info(LogClass.GameMgrCore, $"Searching wikipedia for page with text {text}");
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

            Logger.Error(LogClass.GameMgrCore, "No wikipedia page was found.");

            return null;
        }
        catch (HttpRequestException ex)
        {
            Logger.Error(LogClass.GameMgrCore, $"HTTP request error when searching Wikipedia: {ex.Message}");
            return null;
        }
    }

    public async Task<WikipediaPage?> ScrapeFromResultAsync(string text)
    {
        Logger.Info(LogClass.GameMgrCore, $"Trying to scrape from search result {text}");
        Uri? link = await SearchWikipediaForPageAsync(text);
        if (link != null)
        {
            return await ScrapePageAsync(link);
        }

        return null;
    }

    public async Task<WikipediaPage?> ScrapePageAsync(Uri pageLink)
    {
        Logger.Info(LogClass.GameMgrCore, $"Trying to scrape wikipedia page with link {pageLink.ToString()}");
        try
        {
            Logger.Info(LogClass.GameMgrCore, $"Sending GET request to Wikipedia URL {pageLink.ToString()}");
            string pageContent = await _httpClient.GetStringAsync(pageLink).ConfigureAwait(false);
            Logger.Info(LogClass.GameMgrCore, "Received response from Wikipedia page.");

            string title = ExtractTitle(pageContent);
            Logger.Info(LogClass.GameMgrCore, $"Extracted article header: {title}");

            Dictionary<string, string>? infobox = ExtractInfobox(pageContent);
            Logger.Info(LogClass.GameMgrCore, "Infobox extracted.");

            return new WikipediaPage { Title = title, WikipediaLink = pageLink.ToString(), InfoboxData = infobox };
        }
        catch (HttpRequestException ex)
        {
            Logger.Error(LogClass.GameMgrCore, $"HTTP request error when scraping Wikipedia: {ex.Message}");
            return null;
        }
        catch (Exception ex)
        {
            Logger.Error(LogClass.GameMgrCore, $"Unexpected error when scraping Wikipedia: {ex.Message}");
            return null;
        }
    }

    private static string ExtractTitle(string pageContent)
    {
        Logger.Info(LogClass.GameMgrCore, "Extracting article header from wikipedia page...");
        Match titleMatch = WikipediaArticleHeadingRegex().Match(pageContent);
        return titleMatch.Success ? titleMatch.Groups[1].Value : "Article header not found";
    }

    private static Dictionary<string, string>? ExtractInfobox(string pageContent)
    {
        Logger.Info(LogClass.GameMgrCore, "Extracting infobox from wikipedia page...");
        Dictionary<string, string> infobox = new();
        Match infoboxMatch = InfoboxRegex().Match(pageContent);

        if (!infoboxMatch.Success)
        {
            Logger.Warning(LogClass.GameMgrCore, "No match found for an infobox.");
            return null;
        }

        string infoboxContent = infoboxMatch.Groups[1].Value;
        MatchCollection rowMatches = InfoboxRowRegex().Matches(infoboxContent);

        foreach (Match rowMatch in rowMatches)
        {
            string key = CleanHtml(rowMatch.Groups[1].Value)
                .Trim()
                .Replace("(s)", "s")
                .ToLower();
            string value = CleanHtml(rowMatch.Groups[2].Value).Trim();

            if (key == "release")
            {
                value = ReleaseDateRegex().Replace(value, string.Empty);
            }

            if (!string.IsNullOrEmpty(key) && !string.IsNullOrEmpty(value))
            {
                infobox[key] = value;
            }
        }

        return infobox;
    }

    private static bool _hasPrintedCleanHtmlLog;

    private static string CleanHtml(string html)
    {
        if (_hasPrintedCleanHtmlLog == false)
        {
            Logger.Info(LogClass.GameMgrCore, "Cleaning unwanted HTML elements from string...");
            _hasPrintedCleanHtmlLog = true;
        }
        html = HtmlTagRegex().Replace(html, string.Empty);
        html = CitationsRegex().Replace(html, string.Empty);
        html = NbspRegex().Replace(html, " ");
        return html.Trim();
    }

    [GeneratedRegex("""<table class="infobox.+?">(.+?)</table>""")]
    private static partial Regex InfoboxRegex();

    [GeneratedRegex("<tr><th.+?>(.+?)</th><td.+?>(.+?)</td></tr>")]
    private static partial Regex InfoboxRowRegex();

    [GeneratedRegex("""<a href="/wiki/([^"]+)" title="([^"]+)">""")]
    private static partial Regex WikipediaPageRegex();

    [GeneratedRegex("""<h1 id="firstHeading" class="firstHeading mw-first-heading"><i>(.+?)</i></h1>""")]
    private static partial Regex WikipediaArticleHeadingRegex();

    [GeneratedRegex("<.+?>")]
    private static partial Regex HtmlTagRegex();

    [GeneratedRegex(@"\[.+?\]")]
    private static partial Regex CitationsRegex();

    [GeneratedRegex("&nbsp;")]
    private static partial Regex NbspRegex();

    [GeneratedRegex(".mw-parser-output .plainlist ol,.mw-parser-output .plainlist ul{line-height:inherit;list-style:none;margin:0;padding:0}.mw-parser-output .plainlist ol li,.mw-parser-output .plainlist ul li{margin-bottom:0}")]
    private static partial Regex ReleaseDateRegex();
}

public class WikipediaPage
{
    public required string Title { get; init; }
    public required string WikipediaLink { get; init; }
    public Dictionary<string, string>? InfoboxData { get; init; } = new();
}
