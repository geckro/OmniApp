using OmniApp.Common.Logging;
using System.Text.RegularExpressions;

namespace GameManager.Core.Scrapers;

public partial class Wikipedia
{
    private readonly HttpClient _httpClient;
    private readonly string _baseUrl;

    public Wikipedia(string baseUrl = "en.wikipedia.org")
    {
        _baseUrl = baseUrl;
        _httpClient = new HttpClient();
        _httpClient.DefaultRequestHeaders.Add("User-Agent", "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/126.0.0.0 Safari/537.36");
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

            return new WikipediaPage
            {
                Title = title,
                Url = pageLink.ToString()
            };
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

    [GeneratedRegex("""<a href="/wiki/([^"]+)" title="([^"]+)">""")]
    private static partial Regex WikipediaPageRegex();

    [GeneratedRegex("""<h1 id="firstHeading" class="firstHeading mw-first-heading">(.+?)</h1>""")]
    private static partial Regex WikipediaArticleHeadingRegex();
}

public class WikipediaPage
{
    public required string Title { get; set; }
    public required string Url { get; set; }
}
