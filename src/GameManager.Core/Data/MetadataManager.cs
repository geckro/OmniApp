using OmniApp.Common.Data;
using System.Collections;
using System.Reflection;

namespace GameManager.Core.Data;

/// <summary>
///     Manages reading and writing of JSON data.
/// </summary>
public class JsonDataManager
{
    private readonly JsonHelper _jsonHelper = new();

    /// <summary>
    ///     Writes a collection of data to a JSON file.
    /// </summary>
    /// <param name="value">The collection of data to write.</param>
    /// <param name="jsonFile">The path to the JSON file.</param>
    /// <typeparam name="T">The type of data to write.</typeparam>
    public void WriteJson<T>(ICollection<T> value, string jsonFile)
    {
        _jsonHelper.WriteToJsonFile(value, $"Data/GameMgr/{jsonFile}");
    }

    /// <summary>
    ///     Reads a collection of data from a JSON file.
    /// </summary>
    /// <param name="jsonFile">The path to the JSON file.</param>
    /// <typeparam name="T">The type of data.</typeparam>
    /// <returns>A collection of data read from the JSON file.</returns>
    public ICollection<T> ReadFromJson<T>(string jsonFile)
    {
        try
        {
            EnsureFileExistsWithDefaultData<T>(jsonFile);
            return _jsonHelper.LoadFromJsonFile<T>($"Data/GameMgr/{jsonFile}");
        }
        catch (DirectoryNotFoundException)
        {
            MakeDirectories();
            return ReadFromJson<T>(jsonFile);
        }
    }

    private static void EnsureFileExistsWithDefaultData<T>(string jsonFile)
    {
        string filePath = $"Data/GameMgr/{jsonFile}";
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

        string defaultDataPath = $"Data/JsonData/{defaultJsonFileName}";
        if (!File.Exists(defaultDataPath))
        {
            Console.WriteLine($"Default JSON file '{defaultDataPath}' not found. Skipping.");
            return;
        }

        string? targetDirectory = Path.GetDirectoryName(filePath);
        Directory.CreateDirectory(targetDirectory!);

        File.Copy(defaultDataPath, filePath, true);
    }

    private static void MakeDirectories()
    {
        Directory.CreateDirectory("Data/GameMgr/");
    }
}

/// <summary>
///     Factory class for making data managers.
/// </summary>
public class DataManagerFactory
{
    private readonly JsonDataManager _jsonDataManager = new();

    /// <summary>
    ///     Creates a JSON data manager for the specified type.
    /// </summary>
    /// <typeparam name="T">The type of IMetadata</typeparam>
    /// <returns>A JsonDataManager for the specified type.</returns>
    /// <exception cref="ArgumentException">Thrown when the JsonFile property is not defined for the specified type.</exception>
    public JsonData<T> CreateData<T>() where T : IMetadata
    {
        if (typeof(T).GetConstructor(Type.EmptyTypes) == null)
        {
            throw new ArgumentException($"{typeof(T).Name} must have a parameterless constructor.");
        }

        T instance = Activator.CreateInstance<T>();

        string jsonFile = instance.JsonFile ?? string.Empty;

        if (string.IsNullOrEmpty(jsonFile))
        {
            throw new ArgumentException($"JsonFile property is not defined for type {typeof(T).Name}");
        }

        return new JsonData<T>(_jsonDataManager, jsonFile);
    }
}

/// <summary>
///     Represents a JsonDataManager for a specific type.
/// </summary>
/// <param name="dataManager">The JsonDataManger instance</param>
/// <param name="jsonFile">The path to the JSON file</param>
/// <typeparam name="T">The type of IMetadata</typeparam>
public class JsonData<T>(JsonDataManager dataManager, string jsonFile) where T : IMetadata
{
    /// <summary>
    ///     Writes a collection of data to the JSON file.
    /// </summary>
    /// <param name="value">The collection of data to write.</param>
    public void WriteJson(ICollection<T> value)
    {
        dataManager.WriteJson(value, jsonFile);
    }

    /// <summary>
    ///     Appends a new item to the existing data and writes it to the JSON file.
    /// </summary>
    /// <param name="newItem">The new item to add.</param>
    public void AppendAndWriteJson(T newItem)
    {
        ICollection<T> existingData = dataManager.ReadFromJson<T>(jsonFile);

        string newItemKey = newItem.Name;

        if (existingData.Any(item => item.Name == newItemKey))
        {
            Console.WriteLine("Item with the same key already exists.");
            return;
        }

        existingData.Add(newItem);

        dataManager.WriteJson(existingData, jsonFile);
    }

    /// <summary>
    ///     Reads a collection of data from the JSON file.
    /// </summary>
    /// <returns>A collection of data read from the JSON file.</returns>
    public ICollection<T> ReadFromJson()
    {
        return dataManager.ReadFromJson<T>(jsonFile);
    }

    /// <summary>
    /// Updates an item in the JSON file based on a key identifier and writes it back.
    /// </summary>
    /// <param name="id">The unique identifier of the game to update</param>
    /// <param name="key">The key of the item to update.</param>
    /// <param name="value">The new value to update.</param>
    public void UpdateAndWriteJson(Guid id, string key, object value)
    {
        ICollection<T> existingData = dataManager.ReadFromJson<T>(jsonFile);

        T? itemToUpdate = existingData.FirstOrDefault(item => item.Id == id);

        if (itemToUpdate != null)
        {
            PropertyInfo? propertyInfo = typeof(T).GetProperty(key);
            if (propertyInfo != null)
            {
                Type propertyType = Nullable.GetUnderlyingType(propertyInfo.PropertyType) ?? propertyInfo.PropertyType;

                try
                {
                    object convertedValue = value == null ? null : Convert.ChangeType(value, propertyType);
                    propertyInfo.SetValue(itemToUpdate, convertedValue);
                }
                catch (InvalidCastException ex)
                {
                    Console.WriteLine($"Invalid cast: {ex.Message}");
                    Console.WriteLine($"Value: {value}, Expected Type: {propertyInfo.PropertyType}");
                    return;
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error setting property '{key}': {ex.Message}");
                    return;
                }
            }
            else
            {
                Console.WriteLine($"Property '{key}' not found in type {typeof(T).Name}");
                return;
            }

            dataManager.WriteJson(existingData, jsonFile);
        }
        else
        {
            Console.WriteLine($"Item with key '{key}' not found.");
        }
    }
}
