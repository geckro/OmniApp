using OmniApp.Common.Data;

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
        _jsonHelper.WriteToJsonFile(value, jsonFile);
    }

    /// <summary>
    ///     Reads a collection of data from a JSON file.
    /// </summary>
    /// <param name="jsonFile">The path to the JSON file.</param>
    /// <typeparam name="T">The type of data.</typeparam>
    /// <returns>A collection of data read from the JSON file.</returns>
    public ICollection<T> ReadFromJson<T>(string jsonFile)
    {
        return _jsonHelper.LoadFromJsonFile<T>(jsonFile);
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
}
