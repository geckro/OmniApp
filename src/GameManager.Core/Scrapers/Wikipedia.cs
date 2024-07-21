using OmniApp.Common.Logging;
using System.Text.RegularExpressions;

namespace GameManager.Core.Scrapers;

public partial class Wikipedia
{
    private readonly string _baseUrl;
    private readonly HttpClient _httpClient;
    private const string DefaultUserAgent = "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/126.0.0.0 Safari/537.36";

    public Wikipedia(string baseUrl = "en.wikipedia.org")
    {
        _baseUrl = baseUrl;
        _httpClient = new HttpClient();
        _httpClient.DefaultRequestHeaders.Add("User-Agent", DefaultUserAgent);
    }

    private async Task<Uri?> SearchWikipediaForPageAsync(string text)
    {
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
        Uri? link = await SearchWikipediaForPageAsync(text);
        if (link != null)
        {
            return await ScrapePageAsync(link);
        }

        return null;
    }

    public async Task<WikipediaPage?> ScrapePageAsync(Uri pageLink)
    {
        try
        {
            string pageContent = await _httpClient.GetStringAsync(pageLink);

            string title = ExtractTitle(pageContent);
            Dictionary<string, string>? infobox = ExtractInfobox(pageContent);

            return new WikipediaPage { Title = title, Url = pageLink.ToString(), InfoboxData = infobox };
        }
        catch (HttpRequestException ex)
        {
            Logger.Error(LogClass.GameMgrCore, $"HTTP request error when scraping Wikipedia: {ex.Message}");
            return null;
        }
    }

    private static string ExtractTitle(string pageContent)
    {
        Match titleMatch = WikipediaArticleHeadingRegex().Match(pageContent);
        return titleMatch.Success ? titleMatch.Groups[1].Value : "Title not found";
    }

    private static Dictionary<string, string>? ExtractInfobox(string pageContent)
    {
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
            string key = CleanHtml(rowMatch.Groups[1].Value).Trim();
            string value = CleanHtml(rowMatch.Groups[2].Value).Trim();

            if (!string.IsNullOrEmpty(key) && !string.IsNullOrEmpty(value))
            {
                infobox[key] = value;
            }
        }

        return infobox;
    }

    private static string CleanHtml(string html)
    {
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

    [GeneratedRegex("""<h1 id="firstHeading" class="firstHeading mw-first-heading">(.+?)</h1>""")]
    private static partial Regex WikipediaArticleHeadingRegex();

    [GeneratedRegex("<.+?>")]
    private static partial Regex HtmlTagRegex();

    [GeneratedRegex(@"\[.+?\]")]
    private static partial Regex CitationsRegex();

    [GeneratedRegex("&nbsp;")]
    private static partial Regex NbspRegex();
}

public class WikipediaPage
{
    public required string Title { get; set; }
    public required string Url { get; set; }
    public Dictionary<string, string>? InfoboxData { get; set; } = new();
}
