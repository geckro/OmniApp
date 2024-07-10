namespace GameManager.Core.Data;

public class Platform : IMetadata
{
    /// <summary>
    ///     The name of the platform.
    /// </summary>
    public string Name { get; set; }

    /// <summary>
    ///     The company that owns the platform.
    /// </summary>
    public string? Company { get; set;  }

    /// <summary>
    ///     The type of the platform.
    /// </summary>
    public PlatformType? Type { get; set; }
}

public enum PlatformType : byte
{
    Home = 0,
    Handheld = 1,
    Hybrid = 2,
    Computer = 3,
    Other = 4
}
