using OmniApp.Common.Logging;

namespace OmniApp.Common.Data;

public static class UrlHelper
{
    public static Uri? ConvertStringToUri(string url)
    {
        Logger.Info(LogClass.OmniCommon, $"Converting string {url} to a Uri...");
        if (IsUrl(url))
        {
            return new Uri(url, UriKind.Absolute);
        }

        Logger.Info(LogClass.OmniCommon, $"string {url} is not a valid URL.");
        return null;
    }

    private static bool IsUrl(string url)
    {
        Logger.Info(LogClass.OmniCommon, $"Testing to see if {url} is a valid URL.");
        bool result = Uri.TryCreate(url, UriKind.Absolute, out Uri? uriResult)
               && (uriResult.Scheme == Uri.UriSchemeHttp
                   || uriResult.Scheme == Uri.UriSchemeHttps);

        Logger.Info(LogClass.OmniCommon, result ? "Url is valid." : "Url is not valid.");

        return result;
    }
}
