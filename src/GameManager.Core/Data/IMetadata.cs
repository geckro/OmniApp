namespace GameManager.Core.Data;

/// <summary>
///     An interface for common required metadata.
/// </summary>
public interface IMetadata
{
    Guid Id { get; }
    string Name { get; }
    Dictionary<string, object>? Tags { get; }
    string? JsonFile { get; }
}
