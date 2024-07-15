namespace GameManager.Core.Data;

/// <summary>
///     Factory class for creating metadata accessors.
/// </summary>
/// <param name="metadataPersistence">An instance of <see cref="IMetadataPersistence" /> for handling metadata.</param>
public class MetadataAccessorFactory(IMetadataPersistence metadataPersistence) : IMetadataAccessorFactory
{
    private readonly IMetadataPersistence _metadataPersistence = metadataPersistence ?? throw new ArgumentNullException(nameof(metadataPersistence));

    /// <inheritdoc />
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
