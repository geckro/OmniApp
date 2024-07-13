namespace GameManager.Core.Data;

/// <summary>
///     An interface for common required metadata.
/// </summary>
public interface IMetadata
{
    string Name { get; }
    string? JsonFile { get; }
}

/// <summary>
///     An interface that inherits from IMetadata that represents a person.
/// </summary>
public interface IPerson : IMetadata
{
    bool? MainPerson { get; }
    Uri? Picture { get; }
}

/// <summary>
///     An interface that inherits from IMetadata that represents a company.
/// </summary>
public interface ICompany : IMetadata
{
    Uri? Logo { get; }
}
