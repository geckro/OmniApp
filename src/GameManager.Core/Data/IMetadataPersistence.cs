namespace GameManager.Core.Data;

/// <summary>
///     Provides methods to use for saving and loading metadata.
/// </summary>
public interface IMetadataPersistence
{
    /// <summary>
    ///     Saves a collection of metadata to a JSON file.
    /// </summary>
    /// <param name="value">The collection of metadata to save.</param>
    /// <param name="metadataJsonFile">The json file.</param>
    /// <typeparam name="T">The type of metadata.</typeparam>
    void SaveMetadata<T>(ICollection<T> value, string metadataJsonFile);

    /// <summary>
    ///     Loads a collection of metadata from a JSON file.
    /// </summary>
    /// <param name="metadataJsonFile">The json file.</param>
    /// <typeparam name="T">The type of metadata.</typeparam>
    /// <returns>The collection of loaded metadata.</returns>
    ICollection<T> LoadMetadata<T>(string metadataJsonFile);

    /// <summary>
    ///     Check to see if the metadata file exists.
    /// </summary>
    /// <param name="metadataJsonFilePath">The file path of the JSON file.</param>
    /// <returns>True if metadata file exists, False otherwise.</returns>
    bool MetadataFileExists(string metadataJsonFilePath);

    /// <summary>
    ///     Deletes the metadata file.
    /// </summary>
    /// <param name="metadataJsonFilePath">The file path of the JSON file.</param>
    void DeleteMetadataFile(string metadataJsonFilePath);
}
