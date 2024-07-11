using OmniApp.Common.Logging;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace OmniApp.Common.Data;

/// <summary>
///     Helper class to help read and write data to and from JSON format.
/// </summary>
public class JsonHelper
{
    private readonly JsonSerializerOptions _jsonSerializerOptions = new()
    {
        DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingDefault,
        DictionaryKeyPolicy = JsonNamingPolicy.CamelCase,
        IncludeFields = true,
        MaxDepth = 16,
        PropertyNameCaseInsensitive = true,
        PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
        ReadCommentHandling = JsonCommentHandling.Skip
    };

    private string SerializeToJson<T>(ICollection<T> value)
    {
        return JsonSerializer.Serialize(value, _jsonSerializerOptions);
    }

    private ICollection<T> DeserializeFromJson<T>(string jsonString)
    {
        return JsonSerializer.Deserialize<ICollection<T>>(jsonString, _jsonSerializerOptions) ??
               throw new JsonException("Failed to deserialize JSON as the data is null.");
    }

    /// <summary>
    ///     Writes an ICollection of objects to a specified JSON file.
    /// </summary>
    /// <typeparam name="T">The type of objects in the ICollection.</typeparam>
    /// <param name="value">ICollection of objects to serialize and write to the file.</param>
    /// <param name="jsonFileName">The name of the JSON file to write to.</param>
    /// <exception cref="JsonException">Thrown when there is an error during JSON serialization.</exception>
    public void WriteToJsonFile<T>(ICollection<T> value, string jsonFileName)
    {
        try
        {
            File.WriteAllText(jsonFileName, SerializeToJson(value));
            Logger.Info(LogClass.OmniCommon, $"Serialized {value} in {jsonFileName}");
        }
        catch (JsonException ex)
        {
            Logger.Error(LogClass.OmniCommon, $"Exception when writing to JSON: {ex}");
        }
    }

    /// <summary>
    ///     Reads an ICollection of objects from a specified JSON file.
    /// </summary>
    /// <typeparam name="T">The type of objects to deserialize from the JSON file.</typeparam>
    /// <param name="jsonFileName">The name of the JSON file to read from.</param>
    /// <returns>ICollection of objects read and deserialized from the JSON file.</returns>
    /// <remarks>If the file cannot be read or deserialized, an empty collection is returned.</remarks>
    public ICollection<T> LoadFromJsonFile<T>(string jsonFileName)
    {
        try
        {
            if (!File.Exists(jsonFileName))
            {
                File.WriteAllText(jsonFileName, "[]");
            }
            string jsonRawString = File.ReadAllText(jsonFileName);
            return DeserializeFromJson<T>(jsonRawString);
        }
        catch (JsonException ex)
        {
            Logger.Error(LogClass.OmniCommon, $"JSON Exception when reading from {jsonFileName}: {ex}");
            return [];
        }
    }
}
