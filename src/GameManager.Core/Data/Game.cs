using System.Text.Json.Serialization;

namespace GameManager.Core.Data;

[method: JsonConstructor]
public class Game() : IMetadata
{
    /// <summary>
    ///     The title or name of the game if no region is specified. Defaults to Worldwide.
    /// </summary>
    [JsonPropertyName("title")]
    public string Title { get; set; }

    /// <summary>
    ///     Region-specific titles.
    /// </summary>
    [JsonPropertyName("regionTitle")]
    public Dictionary<Region, string>? RegionTitle { get; set; }

    /// <summary>
    ///     A list of genres that apply to the game.
    /// </summary>
    [JsonPropertyName("genres")]
    public ICollection<Genre>? Genres { get; set; }

    /// <summary>
    ///     A list of platforms or systems that the game is available on.
    /// </summary>
    [JsonPropertyName("platforms")]
    public ICollection<Platform>? Platforms { get; set; }

    /// <summary>
    ///     A list of developers of the game.
    /// </summary>
    [JsonPropertyName("devs")]
    public ICollection<Developer>? Developers { get; set; }

    /// <summary>
    ///     A list of publishers of the game.
    /// </summary>
    [JsonPropertyName("pubs")]
    public ICollection<Publisher>? Publishers { get; set; }

    /// <summary>
    ///     A list of series that the game belongs to.
    /// </summary>
    [JsonPropertyName("series")]
    public ICollection<Series>? Series { get; set; }

    /// <summary>
    ///     Default release date if no region is specified, assumes that the game released worldwide on the same date.
    /// </summary>
    [JsonPropertyName("rDate")]
    public DateTime? ReleaseDateWw { get; set; }

    /// <summary>
    ///     Region-specific release dates.
    /// </summary>
    [JsonPropertyName("rDates")]
    public Dictionary<Region, DateTime?> RegionReleaseDates { get; set; }

    /// <summary>
    ///     If the game is a from the ground up remake.
    /// </summary>
    [JsonPropertyName("remake")]
    public bool? IsRemake { get; set; }

    [JsonPropertyName("remakeOf")] public Game? RemakeOf { get; set; }

    /// <summary>
    ///     If the game is a remaster.
    /// </summary>

    [JsonPropertyName("remaster")]
    public bool? IsRemaster { get; set; }

    [JsonPropertyName("remasterOf")] public Game? RemasterOf { get; set; }

    /// <summary>
    ///     If the game is a port.
    /// </summary>
    [JsonPropertyName("port")]
    public bool? IsPort { get; set; }

    [JsonPropertyName("portOf")] public Game? PortOf { get; set; }

    /// <summary>
    ///     If the game is a sequel to another game.
    /// </summary>
    [JsonPropertyName("sequel")]
    public bool? IsSequel { get; set; }

    [JsonPropertyName("sequelOf")] public Game? SequelOf { get; set; }

    /// <summary>
    ///     If the game is a prequel to another game.
    /// </summary>
    [JsonPropertyName("prequel")]
    public bool? IsPrequel { get; set; }

    [JsonPropertyName("prequelOf")] public Game? PrequelOf { get; set; }

    /// <summary>
    ///     If the Game is downloadable content.
    /// </summary>
    [JsonPropertyName("dlc")]
    public bool? IsDlc { get; set; }

    [JsonPropertyName("dlcOf")] public Game? DlcOf { get; set; }

    /// <summary>
    ///     If the game is a mod of a game, whether it be official or not.
    /// </summary>
    [JsonPropertyName("mod")]
    public bool? IsMod { get; set; }

    [JsonPropertyName("modOf")] public Game? ModOf { get; set; }

    /// <summary>
    ///     If the game is a hack of a game, whether it be official or not.
    /// </summary>
    [JsonPropertyName("hack")]
    public bool? IsHack { get; set; }

    [JsonPropertyName("hackOf")] public Game? HackOf { get; set; }

    /// <summary>
    ///     The creation date of the game entry.
    /// </summary>
    [JsonPropertyName("dateCr")]
    public DateTime CreatedOn { get; set; }

    /// <summary>
    ///     The last time the game entry was updated.
    /// </summary>
    [JsonPropertyName("dateUp")]
    public DateTime LastUpdated { get; set; }

    /// <summary>
    ///     A list of composers of the game.
    /// </summary>
    [JsonPropertyName("composers")]
    public ICollection<Composer>? Composers { get; set; }

    /// <summary>
    ///     A list of directors of the game.
    /// </summary>
    [JsonPropertyName("directors")]
    public ICollection<Director>? Directors { get; set; }

    /// <summary>
    ///     A list of writers of the game.
    /// </summary>
    [JsonPropertyName("writers")]
    public ICollection<Writer>? Writers { get; set; }

    /// <summary>
    ///     A list of producers of the game.
    /// </summary>
    [JsonPropertyName("producers")]
    public ICollection<Producer>? Producers { get; set; }

    /// <summary>
    ///     A list of programmers of the game.
    /// </summary>
    [JsonPropertyName("programmers")]
    public ICollection<Programmer>? Programmers { get; set; }

    /// <summary>
    ///     A list of artists of the game.
    /// </summary>
    [JsonPropertyName("artists")]
    public ICollection<Artist>? Artists { get; set; }

    /// <summary>
    ///     A list of designers of the game.
    /// </summary>
    [JsonPropertyName("designers")]
    public ICollection<Designer>? Designers { get; set; }

    /// <summary>
    ///     The engine that the game uses to run.
    /// </summary>
    [JsonPropertyName("engine")]
    public Engine? Engine { get; set; }

    /// <summary>
    ///     If the user has played the game.
    /// </summary>
    [JsonPropertyName("played")]
    public bool? HasPlayed { get; set; }

    /// <summary>
    ///     If the user has finished the game.
    /// </summary>
    [JsonPropertyName("finished")]
    public bool? HasFinished { get; set; }

    /// <summary>
    ///     If the user has fully completed the game.
    /// </summary>
    [JsonPropertyName("completed")]
    public bool? HasCompleted { get; set; }

    /// <summary>
    ///     The age rating of the game.
    /// </summary>
    [JsonPropertyName("ageRating")]
    public string? AgeRating { get; set; }

    [JsonIgnore] public string JsonFile => "games.json";

    string IMetadata.Name => Title;
}
