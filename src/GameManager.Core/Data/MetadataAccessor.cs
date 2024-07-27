using OmniApp.Common.Logging;
using System.Reflection;

namespace GameManager.Core.Data;

/// <summary>
///     Represents a metadata accessor for a specific type..
/// </summary>
/// <param name="metadataPersistence">The <see cref="MetadataPersistence" /> instance.</param>
/// <param name="jsonFile">The path to the JSON file</param>
/// <typeparam name="T">The type of <see cref="IMetadata" />.</typeparam>
public class MetadataAccessor<T>(MetadataPersistence metadataPersistence, string jsonFile) where T : IMetadata
{
    private readonly string _metadataJsonFile = jsonFile ?? throw new ArgumentNullException(nameof(jsonFile));
    private readonly MetadataPersistence _metadataPersistence = metadataPersistence ?? throw new ArgumentNullException(nameof(metadataPersistence));

    /// <summary>
    ///     Saves a collection of metadata to a file.
    /// </summary>
    /// <param name="value">The collection of metadata to save.</param>
    public void SaveMetadataCollection(ICollection<T> value)
    {
        _metadataPersistence.SaveMetadata(value, _metadataJsonFile);
    }

    /// <summary>
    ///     Adds an item to the metadata collection and saves the updated collection.
    /// </summary>
    /// <param name="newItem">The metadata item to add.</param>
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
    ///     Loads a metadata collection.
    /// </summary>
    /// <returns>The loaded metadata collection.</returns>
    public ICollection<T> LoadMetadataCollection()
    {
        return _metadataPersistence.LoadMetadata<T>(_metadataJsonFile);
    }

    /// <summary>
    ///     Updates the specified metadata item with the new value.
    /// </summary>
    /// <param name="id">The unique identifier of the metadata item to update</param>
    /// <param name="key">The property name of the item to update.</param>
    /// <param name="value">The new value to update for the specified property.</param>
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

    /// <summary>
    ///     Gets the specified item by the unique identifier.
    /// </summary>
    /// <param name="id">The unique identifier of the item.</param>
    /// <returns>The metadata item with the specified ID, or null if not found.</returns>
    public T? GetItemById(Guid id)
    {
        return LoadMetadataCollection().FirstOrDefault(item => item.Id == id);
    }

    /// <summary>
    ///    Removes the metadata item with the specified identifier.
    /// </summary>
    /// <param name="id">The unique identifier of the metadata item to remove.</param>
    public void RemoveItemById(Guid id)
    {
        ICollection<T> existingData = LoadMetadataCollection();
        T? itemToRemove = existingData.FirstOrDefault(item => item.Id == id);

        if (itemToRemove == null)
        {
            return;
        }

        existingData.Remove(itemToRemove);
        SaveMetadataCollection(existingData);
    }
}
