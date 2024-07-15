using OmniApp.Common.Data;
using OmniApp.Common.Logging;
using System.Reflection;

namespace GameManager.Core.Data;

/// <summary>
///     Manages reading and writing of JSON data.
/// </summary>
public class MetadataPersistence : IMetadataPersistence
{
    private const string MetadataDirectory = "Data/GameMgr/";
    private readonly JsonHelper _jsonHelper = new();

    /// <inheritdoc />
    public void SaveMetadata<T>(ICollection<T> value, string metadataJsonFile)
    {
        string fullPath = Path.Combine(MetadataDirectory, metadataJsonFile);
        _jsonHelper.WriteToJsonFile(value, fullPath);
    }

    /// <inheritdoc />
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

    /// <inheritdoc />
    public bool MetadataFileExists(string pathToFile)
    {
        return File.Exists(Path.Combine(MetadataDirectory, pathToFile));
    }

    /// <inheritdoc />
    public void DeleteMetadataFile(string pathToFile)
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
