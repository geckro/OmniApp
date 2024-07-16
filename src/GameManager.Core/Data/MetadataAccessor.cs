using OmniApp.Common.Logging;
using System.Reflection;

namespace GameManager.Core.Data;

/// <summary>
///     Represents a metadata accessor for a specific type..
/// </summary>
/// <param name="metadataPersistence">The <see cref="IMetadataPersistence" /> instance.</param>
/// <param name="jsonFile">The path to the JSON file</param>
/// <typeparam name="T">The type of <see cref="IMetadata" />.</typeparam>
public class MetadataAccessor<T>(IMetadataPersistence metadataPersistence, string jsonFile) : IMetadataAccessor<T>
     where T : IMetadata
 {
     private readonly string _metadataJsonFile = jsonFile ?? throw new ArgumentNullException(nameof(jsonFile));
     private readonly IMetadataPersistence _metadataPersistence = metadataPersistence ?? throw new ArgumentNullException(nameof(metadataPersistence));

     /// <inheritdoc />
     public void SaveMetadataCollection(ICollection<T> value)
     {
         _metadataPersistence.SaveMetadata(value, _metadataJsonFile);
     }

     /// <inheritdoc />
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

     /// <inheritdoc />
     public ICollection<T> LoadMetadataCollection()
     {
         return _metadataPersistence.LoadMetadata<T>(_metadataJsonFile);
     }

     /// <inheritdoc />
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

     /// <inheritdoc />
     public T? GetItemById(Guid id)
     {
         return LoadMetadataCollection().FirstOrDefault(item => item.Id == id);
     }

     /// <inheritdoc />
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
