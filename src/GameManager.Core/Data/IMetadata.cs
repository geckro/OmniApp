namespace GameManager.Core.Data;

/// <summary>
///     An interface for common required metadata.
/// </summary>
public interface IMetadata
{
    string Name { get; }
    string? JsonFile { get; }
}
