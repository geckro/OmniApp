namespace GameManager.Core.Data;

/// <summary>
///     Provides methods for creating metadata accessors.
/// </summary>
public interface IMetadataAccessorFactory
{
    /// <summary>
    ///     Creates a metadata accessor for the specified type.
    /// </summary>
    /// <typeparam name="T">The type of metadata.</typeparam>
    /// <returns>An instance of <see cref="IMetadataAccessor{T}" />.</returns>
    IMetadataAccessor<T> CreateMetadataAccessor<T>() where T : IMetadata;
}
