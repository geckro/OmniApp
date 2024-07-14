namespace GameManager.Core.Data;

/// <summary>
///     An interface for common required metadata.
/// </summary>
public interface IMetadata
{
    Guid Id { get; }
    string Name { get; }
    string? JsonFile { get; }
}
