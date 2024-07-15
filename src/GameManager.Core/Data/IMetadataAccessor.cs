namespace GameManager.Core.Data;

/// <summary>
///     Provides methods for accessing and manipulating metadata of type <typeparamref name="T"/>.
/// </summary>
/// <typeparam name="T">The type of metadata, which must implement <see cref="IMetadata"/>.</typeparam>
public interface IMetadataAccessor<T> where T : IMetadata
{
    /// <summary>
    ///     Saves a collection of metadata to a file.
    /// </summary>
    /// <param name="value">The collection of metadata to save.</param>
    void SaveMetadataCollection(ICollection<T> value);

    /// <summary>
    ///     Adds an item to the metadata collection and saves the updated collection.
    /// </summary>
    /// <param name="newItem">The metadata item to add.</param>
    void AddItemAndSave(T newItem);

    /// <summary>
    ///     Loads a metadata collection.
    /// </summary>
    /// <returns>The loaded metadata collection.</returns>
    ICollection<T> LoadMetadataCollection();

    /// <summary>
    ///     Updates the specified metadata item with the new value.
    /// </summary>
    /// <param name="id">The unique identifier of the metadata item to update</param>
    /// <param name="key">The property name of the item to update.</param>
    /// <param name="value">The new value to update for the specified property.</param>
    void UpdateItemAndSave(Guid id, string key, object? value);

    /// <summary>
    ///     Gets the specified item by the unique identifier.
    /// </summary>
    /// <param name="id">The unique identifier of the item.</param>
    /// <returns>The metadata item with the specified ID, or null if not found.</returns>
    T? GetItemById(Guid id);

    /// <summary>
    ///    Removes the metadata item with the specified identifier.
    /// </summary>
    /// <param name="id">The unique identifier of the metadata item to remove.</param>
    void RemoveItemById(Guid id);
}
