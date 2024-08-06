using OmniApp.Common.Logging;

namespace OmniApp.Common.Data;

public static class UrlHelper
{
    public static Uri? ConvertStringToUri(string url)
    {
        Logger.Debug(LogClass.OmniCommon, $"Converting string \"{url}\" to a Uri...");
        if (IsUrl(url))
        {
            Logger.Info(LogClass.OmniCommon, $"string \"{url}\" is a valid URL.");
            return new Uri(url, UriKind.Absolute);
        }

        Logger.Debug(LogClass.OmniCommon, $"string \"{url}\" is not a valid URL.");
        return null;
    }

    private static bool IsUrl(string url)
    {
        Logger.Debug(LogClass.OmniCommon, $"Testing to see if \"{url}\" is a valid URL.");
        bool result = Uri.TryCreate(url, UriKind.Absolute, out Uri? uriResult) &&
                      (uriResult.Scheme == Uri.UriSchemeHttp || uriResult.Scheme == Uri.UriSchemeHttps);

        Logger.Debug(LogClass.OmniCommon, result ? $"Url \"{url}\" is valid." : $"Url \"{url}\" is not valid.");

        return result;
    }
}
