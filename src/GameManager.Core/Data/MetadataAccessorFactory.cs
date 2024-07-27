namespace GameManager.Core.Data;

/// <summary>
///     Factory class for creating metadata accessors.
/// </summary>
/// <param name="metadataPersistence">An instance of <see cref="MetadataPersistence" /> for handling metadata.</param>
public class MetadataAccessorFactory(MetadataPersistence metadataPersistence)
{
    private readonly MetadataPersistence _metadataPersistence = metadataPersistence ?? throw new ArgumentNullException(nameof(metadataPersistence));

    /// <summary>
    ///     Creates a metadata accessor for the specified type.
    /// </summary>
    /// <typeparam name="T">The type of metadata.</typeparam>
    /// <returns>An instance of <see cref="MetadataAccessor{T}" />.</returns>
    /// <exception cref="ArgumentException">Thrown when the JsonFile property is not defined for the specified type.</exception>
    public MetadataAccessor<T> CreateMetadataAccessor<T>() where T : IMetadata
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
