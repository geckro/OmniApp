using GameManager.Core.Data.MetadataConstructors;
using OmniApp.Common.Logging;
using System.Reflection;

namespace GameManager.Core.Data;

/// <summary>
///     Represents a metadata accessor for an <see cref="IMetadata" /> inheritor.
/// </summary>
/// <typeparam name="T">The type of <see cref="IMetadata" />.</typeparam>
public class MetadataAccessor<T> where T : IMetadata
{
    private readonly MetadataPersistence _mtdPersistence;
    private readonly string _jsonFile;
    private readonly TimeSpan _defaultMtdCacheExpiry = TimeSpan.FromMinutes(60);
    private ICollection<T>? _cachedMtd;
    private DateTime _lastLoadTimeOfMtd;

    /// <summary>
    ///     Represents a metadata accessor for an <see cref="IMetadata" /> inheritor.
    /// </summary>
    /// <param name="mtdPersistence">The <see cref="MetadataPersistence" /> instance.</param>
    /// <param name="jsonFile">The path to the JSON file.</param>
    /// <typeparam name="T">The type of <see cref="IMetadata" />.</typeparam>
    public MetadataAccessor(MetadataPersistence mtdPersistence, string jsonFile)
    {
        _jsonFile = jsonFile;
        _mtdPersistence = mtdPersistence;
    }

    /// <summary>
    ///     Saves a collection of metadata to a file.
    /// </summary>
    /// <param name="value">The collection of metadata to save.</param>
    public void SaveMetadata(ICollection<T> value)
    {
        Logger.Debug(LogClass.GameMgrCoreMtdAccessor, $"MetadataAccessor: Saving metadata: {string.Join(",", value)}");
        _mtdPersistence.SaveMetadata(value, _jsonFile);
        _cachedMtd = null;
    }

    /// <summary>
    ///     Adds an item to the metadata collection and saves the updated collection.
    /// </summary>
    /// <param name="newItem">The metadata item to add.</param>
    public void AddItemAndSave(T newItem)
    {
        Logger.Debug(LogClass.GameMgrCoreMtdAccessor, "MetadataAccessor: Adding item to metadata.");
        ICollection<T> existingMtd = LoadMetadata();
        if (existingMtd.Any(item => item.Name == newItem.Name))
        {
            Logger.Warning(LogClass.GameMgrCoreMtdAccessor, "Item with the same key already exists.");
            return;
        }

        existingMtd.Add(newItem);
        SaveMetadata(existingMtd);
    }

    /// <summary>
    ///     Loads a metadata collection.
    /// </summary>
    /// <returns>The loaded metadata collection.</returns>
    public ICollection<T> LoadMetadata(bool forceRefresh = false)
    {
        // Logger.Debug(LogClass.GameMgrCoreMtdAccessor, $"MetadataAccessor: Loading Metadata, parameter forceRefresh is \"{forceRefresh}\"");
        bool notLongerThanCacheExpiry = DateTime.Now - _lastLoadTimeOfMtd <= _defaultMtdCacheExpiry;

        if (!forceRefresh && _cachedMtd != null && notLongerThanCacheExpiry)
        {
            return _cachedMtd;
        }

        _cachedMtd = _mtdPersistence.LoadMetadata<T>(_jsonFile);
        _lastLoadTimeOfMtd = DateTime.Now;
        return _cachedMtd;
    }

    /// <summary>
    ///     Updates the specified metadata item with the new value.
    /// </summary>
    /// <param name="id">The unique identifier of the metadata item to update</param>
    /// <param name="key">The property name of the item to update.</param>
    /// <param name="value">The new value to update for the specified property.</param>
    public void UpdatePropertyAndSave(Guid id, string key, object? value)
    {
        Logger.Debug(LogClass.GameMgrCoreMtdAccessor, $"MetadataAccessor: Updating property {key} with {value} on {id}.");
        ICollection<T> existingData = LoadMetadata();
        T? itemToUpdate = GetItemById(id);
        if (itemToUpdate == null)
        {
            Logger.Warning(LogClass.GameMgrCoreMtdAccessor, $"Item with id '{id}' not found.");
            return;
        }

        SetLastUpdatedValue(itemToUpdate);

        PropertyInfo? propertyInfo = typeof(T).GetProperty(key);
        if (propertyInfo == null)
        {
            Logger.Warning(LogClass.GameMgrCoreMtdAccessor, $"Property '{key}' not found in type {typeof(T).Name}");
            Logger.Warning(LogClass.GameMgrCoreMtdAccessor,
                    $"^ Valid properties: {string.Join(", ", typeof(T).GetProperties().Select(p => p.Name))}");
            return;
        }

        try
        {
            Type propertyType = Nullable.GetUnderlyingType(propertyInfo.PropertyType) ?? propertyInfo.PropertyType;
            object? convertedValue = value == null ? null : Convert.ChangeType(value, propertyType);
            propertyInfo.SetValue(itemToUpdate, convertedValue);
            SaveMetadata(existingData);
        }
        catch (Exception ex)
        {
            Logger.Error(LogClass.GameMgrCoreMtdAccessor, $"Error updating property '{key}': {ex.Message}");
        }
    }

    private static void SetLastUpdatedValue(T itemToUpdate)
    {
        if (typeof(T) == typeof(Game))
        {
            itemToUpdate.GetType().GetProperty("LastUpdated")?.SetValue(itemToUpdate, DateTime.Now);
        }
    }

    /// <summary>
    ///     Gets the specified item by the unique identifier.
    /// </summary>
    /// <param name="id">The unique identifier of the item.</param>
    /// <returns>The metadata item with the specified ID, or null if not found.</returns>
    public T? GetItemById(Guid id)
    {
        // Logger.Debug(LogClass.GameMgrCoreMtdAccessor, $"MetadataAccessor: Getting item by Id \"{id}\"");
        return LoadMetadata().FirstOrDefault(item => item.Id == id);
    }

    public Guid? GetIdByName(string name)
    {
        return LoadMetadata()
                .FirstOrDefault(item => item.Name.Equals(name, StringComparison.OrdinalIgnoreCase))?.Id;
    }

    public string? GetTagValueOrKey(Guid id, string? key, object? value)
    {
        T? item = GetItemById(id);
        if (item == null || item.Tags == null)
        {
            Logger.Warning(LogClass.GameMgrCoreMtdAccessor, $"Id {id} not found, or tag not found in Id.");
            return null;
        }

        if (value != null && key != null)
        {
            Logger.Warning(LogClass.GameMgrCoreMtdAccessor, "You can not specify both key and value in GetTagValueOrKey.");
            return null;
        }

        if (value != null)
        {
            return item.Tags.FirstOrDefault(tag => tag.Value.ToString() == value.ToString()).Key;
        }

        if (key != null && item.Tags.TryGetValue(key, out ICollection<string>? result))
        {
            return result.ToString();
        }

        Logger.Error(LogClass.GameMgrCoreMtdAccessor, "Unknown error, returning null in GetTagValueOrKey.");
        return null;
    }

    /// <summary>
    ///     Removes the metadata item with the specified identifier.
    /// </summary>
    /// <param name="id">The unique identifier of the metadata item to remove.</param>
    public void RemoveItem(Guid id)
    {
        T? itemToRemove = GetItemById(id);

        if (itemToRemove == null)
        {
            return;
        }

        RemoveItem(itemToRemove);
    }

    public void RemoveItem(T item)
    {
        ICollection<T> existingMtd = LoadMetadata();

        existingMtd.Remove(item);
        SaveMetadata(existingMtd);
    }

    public ICollection<string?> GetAllProperties(Game game)
    {
        ICollection<string?> properties = [];

        foreach (PropertyInfo property in game.GetType().GetProperties())
        {
            properties.Add(property.Name);
        }

        return properties;
    }

    public void AddTagToMetadata(Guid id, Dictionary<string, ICollection<string>> tagToAdd)
    {
        T? item = GetItemById(id);

        if (item == null)
        {
            Logger.Error(LogClass.GameMgrCoreMtdAccessor, $"Item with id '{id}' not found.");
            return;
        }

        AddTagToMetadata(item, tagToAdd);
    }

    public void AddTagToMetadata(T item, Dictionary<string, ICollection<string>> tagToAdd)
    {
        item.Tags ??= new Dictionary<string, ICollection<string>>();

        foreach ((string key, ICollection<string> values) in tagToAdd)
        {
            Logger.Debug(LogClass.GameMgrCoreMtdAccessor,
                    $"Metadata tag key: {key}, Metadata tag values: {string.Join(";", values)}");
            if (!item.Tags.TryGetValue(key, out ICollection<string>? existingValues))
            {
                item.Tags[key] = new List<string>(values);
            }
            else
            {
                foreach (string value in values)
                {
                    if (!existingValues.Contains(value))
                    {
                        existingValues.Add(value);
                    }
                }
            }
        }

        UpdatePropertyAndSave(item.Id, "Tags", item.Tags);
    }
}
