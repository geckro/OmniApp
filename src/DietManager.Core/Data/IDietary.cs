namespace DietManager.Core.Data;

public interface IDietary
{
    Guid Id { get; }
    string Name { get; }
    string? JsonFile { get; }
}
