namespace GameManager.Core.Data;

/// <summary>
///     An interface for required metadata.
/// </summary>
public interface IMetadata
{
    Guid Id { get; }
    string Name { get; }
    Dictionary<string, ICollection<string>>? Tags { get; set; }
    string? JsonFile { get; }
}
