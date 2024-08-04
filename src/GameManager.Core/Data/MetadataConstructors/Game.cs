using System.Text.Json.Serialization;

namespace GameManager.Core.Data.MetadataConstructors;

/// <summary>
///     Constructor for a Game.
/// </summary>
[method: JsonConstructor]
public class Game() : IMetadata
{
    /// <summary>
    ///     The title or name of the game if no region is specified. Defaults to Worldwide.
    /// </summary>
    public required string Title { get; init; }

    /// <summary>
    ///     Other game titles.
    /// </summary>
    public ICollection<string>? OtherTitles { get; set; }

    /// <summary>
    ///     A list of genres that apply to the game.
    /// </summary>
    public ICollection<Genre>? Genres { get; set; }

    /// <summary>
    ///     A list of platforms or systems that the game is available on.
    /// </summary>
    public ICollection<Platform>? Platforms { get; set; }

    /// <summary>
    ///     A list of developers of the game.
    /// </summary>
    public ICollection<Developer>? Developers { get; set; }

    /// <summary>
    ///     A list of publishers of the game.
    /// </summary>
    public ICollection<Publisher>? Publishers { get; set; }

    /// <summary>
    ///     A list of series that the game belongs to.
    /// </summary>
    public ICollection<Series>? Series { get; set; }

    /// <summary>
    ///     Default release date if no region is specified, assumes that the game released worldwide on the same date.
    /// </summary>
    public DateTime? ReleaseDateWw { get; set; }

    /// <summary>
    ///     Region-specific release dates.
    /// </summary>
    public Dictionary<string, DateTime?>? RegionReleaseDates { get; set; }

    /// <summary>
    ///     The creation date of the game entry, and the last time the game entry was updated.
    /// </summary>
    public DateTime CreatedOn { get; set; }

    public DateTime LastUpdated { get; set; }

    /// <summary>
    ///     If the user has played, finished or fully completed the game.
    /// </summary>
    public bool? HasPlayed { get; set; }

    public bool? HasFinished { get; set; }
    public bool? HasCompleted { get; set; }

    /// <summary>
    ///     Miscellaneous user-made tags
    /// </summary>
    public Dictionary<string, ICollection<string>>? Tags { get; set; }

    public required Guid Id { get; init; }

    [JsonIgnore] public string JsonFile => "games.json";

    string IMetadata.Name => Title;
}
