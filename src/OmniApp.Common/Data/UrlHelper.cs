using OmniApp.Common.Logging;

namespace OmniApp.Common.Data;

public static class UrlHelper
{
    public static Uri? ConvertStringToUri(string url)
    {
        Logger.Debug(LogClass.OmniCommonData, $"Converting string \"{url}\" to a Uri...");
        if (IsUrl(url))
        {
            Logger.Info(LogClass.OmniCommonData, $"string \"{url}\" is a valid URL.");
            return new Uri(url, UriKind.Absolute);
        }

        Logger.Debug(LogClass.OmniCommonData, $"string \"{url}\" is not a valid URL.");
        return null;
    }

    private static bool IsUrl(string url)
    {
        Logger.Debug(LogClass.OmniCommonData, $"Testing to see if \"{url}\" is a valid URL.");
        bool result = Uri.TryCreate(url, UriKind.Absolute, out Uri? uriResult) &&
                      (uriResult.Scheme == Uri.UriSchemeHttp || uriResult.Scheme == Uri.UriSchemeHttps);

        Logger.Debug(LogClass.OmniCommonData, result ? $"Url \"{url}\" is valid." : $"Url \"{url}\" is not valid.");

        return result;
    }
}
