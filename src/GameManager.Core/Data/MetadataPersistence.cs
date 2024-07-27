using OmniApp.Common.Data;
using OmniApp.Common.Logging;
using System.Reflection;

namespace GameManager.Core.Data;

/// <summary>
///     Manages reading and writing of JSON data.
/// </summary>
public class MetadataPersistence
{
    private const string MetadataDirectory = "Data/GameMgr/";
    private readonly JsonHelper _jsonHelper = new();

    /// <summary>
    ///     Saves a collection of metadata to a JSON file.
    /// </summary>
    /// <param name="value">The collection of metadata to save.</param>
    /// <param name="metadataJsonFile">The json file.</param>
    /// <typeparam name="T">The type of metadata.</typeparam>
    public void SaveMetadata<T>(ICollection<T> value, string metadataJsonFile)
    {
        string fullPath = Path.Combine(MetadataDirectory, metadataJsonFile);
        _jsonHelper.WriteToJsonFile(value, fullPath);
    }

    /// <summary>
    ///     Loads a collection of metadata from a JSON file.
    /// </summary>
    /// <param name="metadataJsonFile">The json file.</param>
    /// <typeparam name="T">The type of metadata.</typeparam>
    /// <returns>The collection of loaded metadata.</returns>
    public ICollection<T> LoadMetadata<T>(string metadataJsonFile)
    {
        try
        {
            InitializeMetadataWithDefaultData<T>(metadataJsonFile);
            string fullPath = Path.Combine(MetadataDirectory, metadataJsonFile);
            return _jsonHelper.LoadFromJsonFile<T>(fullPath);
        }
        catch (DirectoryNotFoundException)
        {
            Directory.CreateDirectory(MetadataDirectory);
            return LoadMetadata<T>(metadataJsonFile);
        }
    }

    /// <summary>
    ///     Check to see if the metadata file exists.
    /// </summary>
    /// <param name="pathToFile">The file path of the JSON file.</param>
    /// <returns>True if metadata file exists, False otherwise.</returns>
    public static bool MetadataFileExists(string pathToFile)
    {
        return File.Exists(Path.Combine(MetadataDirectory, pathToFile));
    }

    /// <summary>
    ///     Deletes the metadata file.
    /// </summary>
    /// <param name="pathToFile">The file path of the JSON file.</param>
    public static void DeleteMetadataFile(string pathToFile)
    {
        string fullPath = Path.Combine(MetadataDirectory, pathToFile);
        if (File.Exists(fullPath))
        {
            File.Delete(fullPath);
        }
    }

    /// <summary>
    ///     Initializes metadata with default data if the JSON file has not been generated yet.
    /// </summary>
    /// <param name="jsonFile">The name of the JSON file/</param>
    /// <typeparam name="T">The type of data</typeparam>
    private static void InitializeMetadataWithDefaultData<T>(string jsonFile)
    {
        string filePath = Path.Combine(MetadataDirectory, jsonFile);
        if (File.Exists(filePath))
        {
            return;
        }

        PropertyInfo? jsonFileProperty = typeof(T).GetProperty("JsonFile");
        if (jsonFileProperty == null)
        {
            throw new InvalidOperationException($"Type {typeof(T).Name} does not have a JsonFile property.");
        }

        string? defaultJsonFileName = (string?)jsonFileProperty.GetValue(Activator.CreateInstance<T>());
        if (string.IsNullOrEmpty(defaultJsonFileName))
        {
            throw new InvalidOperationException($"The JsonFile property of {typeof(T).Name} is null or empty.");
        }

        string defaultDataPath = Path.Combine("Data/JsonData/", defaultJsonFileName);
        if (!File.Exists(defaultDataPath))
        {
            Logger.Warning(LogClass.GameMgrCore, $"Default JSON file '{defaultDataPath}' not found. Skipping.");
            return;
        }

        Directory.CreateDirectory(Path.GetDirectoryName(filePath)!);
        File.Copy(defaultDataPath, filePath, true);
    }
}
