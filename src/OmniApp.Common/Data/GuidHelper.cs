namespace OmniApp.Common.Data;

public class GuidHelper
{
    public static Guid? ConvertStringToGuid(string stringToConvert)
    {
        return Guid.TryParse(stringToConvert, out Guid guidOutput) ? guidOutput : null;
    }

    public static bool IsValidGuid(string guidString)
    {
        return Guid.TryParse(guidString, out _);
    }
}
