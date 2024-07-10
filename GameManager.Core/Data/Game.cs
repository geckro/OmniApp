using System.Text.Json.Serialization;

namespace GameManager.Core.Data;

[method: JsonConstructor]
public class Game(
    string title,
    Genre[]? genres = null,
    Platform[]? platforms = null,
    Developer[]? developers = null,
    Publisher[]? publishers = null,
    Series[]? series = null,
    DateTime? releaseDateWw = null,
    Dictionary<Region, DateTime?>? regionReleaseDates = null,
    bool? isRemake = null,
    Game? remakeOf = null,
    bool? isRemaster = null,
    Game? remasterOf = null,
    bool? isPort = null,
    Game? portOf = null,
    bool? isSequel = null,
    Game? sequelOf = null,
    bool? isPrequel = null,
    Game? prequelOf = null,
    bool? isDlc = null,
    Game? dlcOf = null,
    bool? isMod = null,
    Game? modOf = null,
    bool? isHack = null,
    Game? hackOf = null,
    Dictionary<Region, string>? regionTitles = null,
    Director[]? directors = null,
    Producer[]? producers = null,
    Designer[]? designers = null,
    Programmer[]? programmers = null,
    Artist[]? artists = null,
    Writer[]? writers = null,
    Composer[]? composers = null,
    Engine? engine = null,
    bool? hasPlayed = null,
    bool? hasFinished = null,
    bool? hasCompleted = null,
    string? ageRating = null)
    : IMetadata
{
    /// <summary>
    ///     The title or name of the game if no region is specified. Defaults to Worldwide.
    /// </summary>
    [JsonPropertyName("title")]
    public string Title { get; set; } = title;

    /// <summary>
    ///     Region-specific titles.
    /// </summary>
    [JsonPropertyName("regionTitle")]
    public Dictionary<Region, string>? RegionTitle { get; set; } = regionTitles;

    /// <summary>
    ///     A list of genres that apply to the game.
    /// </summary>
    [JsonPropertyName("genres")]
    public Genre[]? Genres { get; set; } = genres;

    /// <summary>
    ///     A list of platforms or systems that the game is available on.
    /// </summary>
    [JsonPropertyName("platforms")]
    public Platform[]? Platforms { get; set; } = platforms;

    /// <summary>
    ///     A list of developers of the game.
    /// </summary>
    [JsonPropertyName("devs")]
    public Developer[]? Developers { get; set; } = developers;

    /// <summary>
    ///     A list of publishers of the game.
    /// </summary>
    [JsonPropertyName("pubs")]
    public Publisher[]? Publishers { get; set; } = publishers;

    /// <summary>
    ///     A list of series that the game belongs to.
    /// </summary>
    [JsonPropertyName("series")]
    public Series[]? Series { get; set; } = series;

    /// <summary>
    ///     Default release date if no region is specified, assumes that the game released worldwide on the same date.
    /// </summary>
    [JsonPropertyName("rDate")]
    public DateTime? ReleaseDateWw { get; set; } = releaseDateWw;

    /// <summary>
    ///     Region-specific release dates.
    /// </summary>
    [JsonPropertyName("rDates")]
    public Dictionary<Region, DateTime?> RegionReleaseDates { get; set; } = regionReleaseDates ?? new Dictionary<Region, DateTime?>();

    /// <summary>
    ///     If the game is a from the ground up remake.
    /// </summary>
    [JsonPropertyName("remake")]
    public bool? IsRemake { get; set; } = isRemake;

    [JsonPropertyName("remakeOf")] public Game? RemakeOf { get; set; } = remakeOf;

    /// <summary>
    ///     If the game is a remaster.
    /// </summary>

    [JsonPropertyName("remaster")]
    public bool? IsRemaster { get; set; } = isRemaster;

    [JsonPropertyName("remasterOf")] public Game? RemasterOf { get; set; } = remasterOf;

    /// <summary>
    ///     If the game is a port.
    /// </summary>
    [JsonPropertyName("port")]
    public bool? IsPort { get; set; } = isPort;

    [JsonPropertyName("portOf")] public Game? PortOf { get; set; } = portOf;

    /// <summary>
    ///     If the game is a sequel to another game.
    /// </summary>
    [JsonPropertyName("sequel")]
    public bool? IsSequel { get; set; } = isSequel;

    [JsonPropertyName("sequelOf")] public Game? SequelOf { get; set; } = sequelOf;

    /// <summary>
    ///     If the game is a prequel to another game.
    /// </summary>
    [JsonPropertyName("prequel")]
    public bool? IsPrequel { get; set; } = isPrequel;

    [JsonPropertyName("prequelOf")] public Game? PrequelOf { get; set; } = prequelOf;

    /// <summary>
    ///     If the Game is downloadable content.
    /// </summary>
    [JsonPropertyName("dlc")]
    public bool? IsDlc { get; set; } = isDlc;

    [JsonPropertyName("dlcOf")] public Game? DlcOf { get; set; } = dlcOf;

    /// <summary>
    ///     If the game is a mod of a game, whether it be official or not.
    /// </summary>
    [JsonPropertyName("mod")]
    public bool? IsMod { get; set; } = isMod;

    [JsonPropertyName("modOf")] public Game? ModOf { get; set; } = modOf;

    /// <summary>
    ///     If the game is a hack of a game, whether it be official or not.
    /// </summary>
    [JsonPropertyName("hack")]
    public bool? IsHack { get; set; } = isHack;

    [JsonPropertyName("hackOf")] public Game? HackOf { get; set; } = hackOf;

    /// <summary>
    ///     The creation date of the game entry.
    /// </summary>
    [JsonPropertyName("dateCr")]
    public DateTime CreatedOn { get; set; } = DateTime.Now;

    /// <summary>
    ///     The last time the game entry was updated.
    /// </summary>
    [JsonPropertyName("dateUp")]
    public DateTime LastUpdated { get; set; } = DateTime.Now;

    /// <summary>
    ///     A list of composers of the game.
    /// </summary>
    [JsonPropertyName("composers")]
    public Composer[]? Composers { get; set; } = composers;

    /// <summary>
    ///     A list of directors of the game.
    /// </summary>
    [JsonPropertyName("directors")]
    public Director[]? Directors { get; set; } = directors;

    /// <summary>
    ///     A list of writers of the game.
    /// </summary>
    [JsonPropertyName("writers")]
    public Writer[]? Writers { get; set; } = writers;

    /// <summary>
    ///     A list of producers of the game.
    /// </summary>
    [JsonPropertyName("producers")]
    public Producer[]? Producers { get; set; } = producers;

    /// <summary>
    ///     A list of programmers of the game.
    /// </summary>
    [JsonPropertyName("programmers")]
    public Programmer[]? Programmers { get; set; } = programmers;

    /// <summary>
    ///     A list of artists of the game.
    /// </summary>
    [JsonPropertyName("artists")]
    public Artist[]? Artists { get; set; } = artists;

    /// <summary>
    ///     A list of designers of the game.
    /// </summary>
    [JsonPropertyName("designers")]
    public Designer[]? Designers { get; set; } = designers;

    /// <summary>
    ///     The engine that the game uses to run.
    /// </summary>
    [JsonPropertyName("engine")]
    public Engine? Engine { get; set; } = engine;

    /// <summary>
    ///     If the user has played the game.
    /// </summary>
    [JsonPropertyName("played")]
    public bool? HasPlayed { get; set; } = hasPlayed;

    /// <summary>
    ///     If the user has finished the game.
    /// </summary>
    [JsonPropertyName("finished")]
    public bool? HasFinished { get; set; } = hasFinished;

    /// <summary>
    ///     If the user has fully completed the game.
    /// </summary>
    [JsonPropertyName("completed")]
    public bool? HasCompleted { get; set; } = hasCompleted;

    /// <summary>
    ///     The age rating of the game.
    /// </summary>
    [JsonPropertyName("ageRating")]
    public string? AgeRating { get; set; } = ageRating;

    string IMetadata.Name => Title;
}
