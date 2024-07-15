using OmniApp.Common.Data;
using OmniApp.Common.Logging;
using System.Reflection;

namespace GameManager.Core.Data;

/// <summary>
///     Public interface to use for saving and loading metadata.
/// </summary>
public interface IMetadataPersistence
{
    /// <summary>
    /// Saves a collection of metadata to a JSON file.
    /// </summary>
    /// <param name="value">The collection of metadata to save.</param>
    /// <param name="metadataJsonFile">The json file.</param>
    /// <typeparam name="T">The type of metadata.</typeparam>
    void SaveMetadata<T>(ICollection<T> value, string metadataJsonFile);

    /// <summary>
    /// Loads a collection of metadata from a JSON file.
    /// </summary>
    /// <param name="metadataJsonFile">The json file.</param>
    /// <typeparam name="T">The type of metadata.</typeparam>
    /// <returns>The collection of loaded metadata.</returns>
    ICollection<T> LoadMetadata<T>(string metadataJsonFile);
}

/// <summary>
///     Manages reading and writing of JSON data.
/// </summary>
public class JsonMetadataPersistence : IMetadataPersistence
{
    private const string MetadataDirectory = "Data/GameMgr/";
    private readonly JsonHelper _jsonHelper = new();

    /// <summary>
    ///     Writes a collection of data to a JSON file.
    /// </summary>
    /// <param name="value">The collection of data to write.</param>
    /// <param name="metadataJsonFile">The path to the JSON file.</param>
    /// <typeparam name="T">The type of data to write.</typeparam>
    public void SaveMetadata<T>(ICollection<T> value, string metadataJsonFile)
    {
        string fullPath = Path.Combine(MetadataDirectory, metadataJsonFile);
        _jsonHelper.WriteToJsonFile(value, fullPath);
    }

    /// <summary>
    ///     Reads a collection of data from a JSON file.
    /// </summary>
    /// <param name="metadataJsonFile">The path to the JSON file.</param>
    /// <typeparam name="T">The type of data.</typeparam>
    /// <returns>A collection of data read from the JSON file.</returns>
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
    /// Initializes metadata with default data if the JSON file has not been generated yet.
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

/// <summary>
/// Methods for creating metadata accessors.
/// </summary>
public interface IMetadataAccessorFactory
{
    /// <summary>
    /// Creates a metadata accessor for the specified type.
    /// </summary>
    /// <typeparam name="T">The type of metadata.</typeparam>
    /// <returns>An instance of <see cref="IMetadataAccessor{T}"/>.</returns>
    IMetadataAccessor<T> CreateMetadataAccessor<T>() where T : IMetadata;
}

/// <summary>
/// Factory class for creating metadata accessors.
/// </summary>
/// <param name="metadataPersistence">An instance of <see cref="IMetadataPersistence"/> for handling metadata.</param>
public class MetadataAccessorFactory(IMetadataPersistence metadataPersistence) : IMetadataAccessorFactory
{
    private readonly IMetadataPersistence _metadataPersistence = metadataPersistence ?? throw new ArgumentNullException(nameof(metadataPersistence));

    /// <summary>
    ///     Creates a JSON data manager for the specified type.
    /// </summary>
    /// <typeparam name="T">The type of IMetadata</typeparam>
    /// <returns>An instance of <see cref="IMetadataAccessor{T}"/>.</returns>
    /// <exception cref="ArgumentException">Thrown when the JsonFile property is not defined for the specified type.</exception>
    public IMetadataAccessor<T> CreateMetadataAccessor<T>() where T : IMetadata
    {
        if (typeof(T).GetConstructor(Type.EmptyTypes) == null)
        {
            throw new ArgumentException($"{typeof(T).Name} must have a parameterless constructor.");
        }

        T instance = Activator.CreateInstance<T>();
        string jsonFile = instance.JsonFile ?? throw new ArgumentException($"JsonFile property is not defined for type {typeof(T).Name}");

        return new MetadataAccessor<T>(_metadataPersistence, jsonFile);
    }
}

/// <summary>
/// Methods for accessing and manipulating metadata.
/// </summary>
/// <typeparam name="T">The type of <see cref="IMetadata"/>.</typeparam>
public interface IMetadataAccessor<T> where T : IMetadata
{
    /// <summary>
    /// Saves a collection of metadata.
    /// </summary>
    /// <param name="value">The collection of metadata to save.</param>
    void SaveMetadataCollection(ICollection<T> value);

    /// <summary>
    /// Adds an item to the metadata collection and saves it.
    /// </summary>
    /// <param name="newItem">The item to add.</param>
    void AddItemAndSave(T newItem);

    /// <summary>
    /// Loads a collection of metadata.
    /// </summary>
    /// <returns>The collection of loaded metadata.</returns>
    ICollection<T> LoadMetadataCollection();

    /// <summary>
    /// Updates an item in the metadata collection and saves it.
    /// </summary>
    /// <param name="id">The unique identifier of the item to update.</param>
    /// <param name="key">The key of the item to update.</param>
    /// <param name="value">The new value to update.</param>
    void UpdateItemAndSave(Guid id, string key, object? value);
}

/// <summary>
///     Represents a metadata accessor for a specific type..
/// </summary>
/// <param name="metadataPersistence">The <see cref="IMetadataPersistence"/> instance.</param>
/// <param name="jsonFile">The path to the JSON file</param>
/// <typeparam name="T">The type of <see cref="IMetadata"/>.</typeparam>
public class MetadataAccessor<T>(IMetadataPersistence metadataPersistence, string jsonFile) : IMetadataAccessor<T>
    where T : IMetadata
{
    private readonly IMetadataPersistence _metadataPersistence = metadataPersistence ?? throw new ArgumentNullException(nameof(metadataPersistence));
    private readonly string _metadataJsonFile = jsonFile ?? throw new ArgumentNullException(nameof(jsonFile));

    /// <summary>
    ///     Writes a collection of data to the JSON file.
    /// </summary>
    /// <param name="value">The collection of data to write.</param>
    public void SaveMetadataCollection(ICollection<T> value)
    {
        _metadataPersistence.SaveMetadata(value, _metadataJsonFile);
    }


    /// <summary>
    /// Adds a new item to the metadata collection and saves it to the JSON file.
    /// </summary>
    /// <param name="newItem">The new item to add.</param>
    public void AddItemAndSave(T newItem)
    {
        ICollection<T> existingData = LoadMetadataCollection();
        if (existingData.Any(item => item.Name == newItem.Name))
        {
            Logger.Warning(LogClass.GameMgrCore, "Item with the same key already exists.");
            return;
        }

        existingData.Add(newItem);
        SaveMetadataCollection(existingData);
    }

    /// <summary>
    /// Loads a collection of metadata from the JSON file.
    /// </summary>
    /// <returns>The collection of loaded metadata.</returns>
    public ICollection<T> LoadMetadataCollection()
    {
        return _metadataPersistence.LoadMetadata<T>(_metadataJsonFile);
    }

    /// <summary>
    ///     Updates an item in the JSON file based on a key identifier and writes it back.
    /// </summary>
    /// <param name="id">The unique identifier of the game to update</param>
    /// <param name="key">The key of the item to update.</param>
    /// <param name="value">The new value to update.</param>
    public void UpdateItemAndSave(Guid id, string key, object? value)
    {
        ICollection<T> existingData = LoadMetadataCollection();
        T? itemToUpdate = existingData.FirstOrDefault(item => item.Id == id);
        if (itemToUpdate == null)
        {
            Logger.Warning(LogClass.GameMgrCore, $"Item with id '{id}' not found.");
            return;
        }

        PropertyInfo? propertyInfo = typeof(T).GetProperty(key);
        if (propertyInfo == null)
        {
            Logger.Warning(LogClass.GameMgrCore, $"Property '{key}' not found in type {typeof(T).Name}");
            return;
        }

        try
        {
            Type propertyType = Nullable.GetUnderlyingType(propertyInfo.PropertyType) ?? propertyInfo.PropertyType;
            object? convertedValue = value == null ? null : Convert.ChangeType(value, propertyType);
            propertyInfo.SetValue(itemToUpdate, convertedValue);
            SaveMetadataCollection(existingData);
        }
        catch (Exception ex)
        {
            Logger.Error(LogClass.GameMgrCore, $"Error updating property '{key}': {ex.Message}");
        }
    }
}
