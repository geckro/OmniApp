using GameManager.Core.Data.Enums;
using System.Text.Json.Serialization;

namespace GameManager.Core.Data.MetadataConstructors;

/// <summary>
///     Constructor for a Game.
/// </summary>
[method: JsonConstructor]
public class Game() : IMetadata
{
    public required Guid Id { get; set; }

    /// <summary>
    ///     The title or name of the game if no region is specified. Defaults to Worldwide.
    /// </summary>
    public required string Title { get; set; }

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
    public Dictionary<Region, DateTime?>? RegionReleaseDates { get; set; }

    /// <summary>
    ///     If the game is a from the ground up remake.
    /// </summary>
    public bool? IsRemake { get; set; }

    public Game? RemakeOf { get; set; }

    /// <summary>
    ///     If the game is a remaster.
    /// </summary>

    public bool? IsRemaster { get; set; }

    public Game? RemasterOf { get; set; }

    /// <summary>
    ///     If the game is a port.
    /// </summary>
    public bool? IsPort { get; set; }

    public Game? PortOf { get; set; }

    /// <summary>
    ///     If the game is a sequel to another game.
    /// </summary>
    public bool? IsSequel { get; set; }

    public Game? SequelOf { get; set; }

    /// <summary>
    ///     If the game is a prequel to another game.
    /// </summary>
    public bool? IsPrequel { get; set; }

    public Game? PrequelOf { get; set; }

    /// <summary>
    ///     If the Game is downloadable content.
    /// </summary>
    public bool? IsDlc { get; set; }

    public Game? DlcOf { get; set; }

    /// <summary>
    ///     If the game is a mod of a game, whether it be official or not.
    /// </summary>
    public bool? IsMod { get; set; }

    public Game? ModOf { get; set; }

    /// <summary>
    ///     If the game is a hack of a game, whether it be official or not.
    /// </summary>
    public bool? IsHack { get; set; }

    public Game? HackOf { get; set; }

    /// <summary>
    ///     The creation date of the game entry.
    /// </summary>
    public DateTime CreatedOn { get; set; }

    /// <summary>
    ///     The last time the game entry was updated.
    /// </summary>
    public DateTime LastUpdated { get; set; }

    /// <summary>
    ///     A list of composers of the game.
    /// </summary>
    public ICollection<Composer>? Composers { get; set; }

    /// <summary>
    ///     A list of directors of the game.
    /// </summary>
    public ICollection<Director>? Directors { get; set; }

    /// <summary>
    ///     A list of writers of the game.
    /// </summary>
    public ICollection<Writer>? Writers { get; set; }

    /// <summary>
    ///     A list of producers of the game.
    /// </summary>
    public ICollection<Producer>? Producers { get; set; }

    /// <summary>
    ///     A list of programmers of the game.
    /// </summary>
    public ICollection<Programmer>? Programmers { get; set; }

    /// <summary>
    ///     A list of artists of the game.
    /// </summary>
    public ICollection<Artist>? Artists { get; set; }

    /// <summary>
    ///     A list of designers of the game.
    /// </summary>
    public ICollection<Designer>? Designers { get; set; }

    /// <summary>
    ///     The engine that the game uses to run.
    ///     Is collection as Games could technically use multiple engines.
    /// </summary>
    public ICollection<Engine>? Engine { get; set; }

    /// <summary>
    ///     If the user has played the game.
    /// </summary>
    public bool? HasPlayed { get; set; }

    /// <summary>
    ///     If the user has finished the game.
    /// </summary>
    public bool? HasFinished { get; set; }

    /// <summary>
    ///     If the user has fully completed the game.
    /// </summary>
    public bool? HasCompleted { get; set; }

    /// <summary>
    ///     If the game is open source.
    /// </summary>
    public bool? IsOpenSource { get; set; }

    /// <summary>
    ///     What license the game is under, if any.
    /// </summary>
    public Licenses? License { get; set; }

    /// <summary>
    ///     The website of the game, if there is one.
    /// </summary>
    public Uri? Website { get; set; }

    /// <summary>
    ///     The age rating of the game.
    /// </summary>
    public ICollection<AgeRatings>? AgeRatings { get; set; }

    [JsonIgnore] public string JsonFile => "games.json";

    string IMetadata.Name => Title;
}
