using System.Text.Json;
using System.Text.Json.Serialization;
using static GameManager.Core.Data.PlatformType;

namespace GameManager.Core.Data;

public interface IMetadataData<in T> where T : IMetadata
{
    void Add(T metadata);
}

public abstract class Data<T>(string jsonFile) : IMetadataData<T> where T : IMetadata
{
    private readonly JsonSerializerOptions _jsonSerializerOptions = new()
    {
        DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingDefault,
        PropertyNameCaseInsensitive = true,
    };

    public abstract void Add(T item);

    protected void Serialize(IList<T> items)
    {
        try
        {
            string jsonString = JsonSerializer.Serialize(items, _jsonSerializerOptions);
            File.WriteAllText(jsonFile, jsonString);
        }
        catch (FileNotFoundException)
        {
            File.Create(jsonFile);
            string jsonString = JsonSerializer.Serialize(items, _jsonSerializerOptions);
            File.WriteAllText(jsonFile, jsonString);
        }
    }

    public IList<T> Deserialize()
    {
        try
        {
            string jsonString = File.ReadAllText(jsonFile);

            Console.WriteLine(jsonString);

            return JsonSerializer.Deserialize<IList<T>>(jsonString, _jsonSerializerOptions) ?? DefaultData();
        }
        catch (FileNotFoundException)
        {
            IList<T> dataList = DefaultData();
            Serialize(dataList);
            return dataList;
        }
        catch (JsonException ex)
        {
            Console.WriteLine($"JSON deserialization error: {ex.Message}");
            throw;
        }
    }

    protected abstract List<T> DefaultData();
}

public class GameData() : Data<Game>("games.json")
{
    public override void Add(Game game)
    {
        IList<Game> games = Deserialize();
        games.Add(game);
        Serialize(games);
    }

    public IList<Game> GetGames()
    {
        return Deserialize();
    }

    protected override List<Game> DefaultData()
    {
        return [];
    }
}

public class GenreData() : Data<Genre>("genres.json")
{
    protected override List<Genre> DefaultData()
    {
        return
        [
            new Genre("Platformer"),
            new Genre("Shooter")
        ];
    }

    public override void Add(Genre genre)
    {
        IList<Genre> genres = Deserialize();
        genres.Add(genre);
        Serialize(genres);
    }
}

public class PlatformData() : Data<Platform>("platforms.json")
{
    public IList<Platform> GetAllPlatforms()
    {
        return Deserialize();
    }

    protected override List<Platform> DefaultData()
    {
        return
        [
            new Platform("2600", "Atari", Home),
            new Platform("32X", "Sega", Home),
            new Platform("3DS", "Nintendo", Handheld),
            new Platform("5200", "Atari", Home),
            new Platform("7800", "Atari", Home),
            new Platform("Android", "Google", Home),
            new Platform("DS", "Nintendo", Handheld),
            new Platform("Dreamcast", "Sega", Home),
            new Platform("Game Boy Advance", "Nintendo", Handheld),
            new Platform("Game Boy Color", "Nintendo", Handheld),
            new Platform("Game Boy", "Nintendo", Handheld),
            new Platform("Game Gear", "Sega", Handheld),
            new Platform("GameCube", "Nintendo", Home),
            new Platform("Genesis", "Sega", Home),
            new Platform("Jaguar", "Atari", Home),
            new Platform("Linux", type: Home),
            new Platform("Lynx", "Atari", Handheld),
            new Platform("MacOS", "Apple", Home),
            new Platform("Master System", "Sega", Home),
            new Platform("NES", "Nintendo", Home),
            new Platform("Nintendo 64", "Nintendo", Home),
            new Platform("PlayStation 2", "Sony", Home),
            new Platform("PlayStation 3", "Sony", Home),
            new Platform("PlayStation 4", "Sony", Home),
            new Platform("PlayStation 5", "Sony", Home),
            new Platform("PlayStation Portable", "Sony", Handheld),
            new Platform("PlayStation Vita", "Sony", Handheld),
            new Platform("PlayStation", "Sony", Home),
            new Platform("Saturn", "Sega", Home),
            new Platform("Sega CD", "Sega", Home),
            new Platform("Super Nintendo", "Nintendo", Home),
            new Platform("Switch", "Nintendo", Hybrid),
            new Platform("Virtual Boy", "Nintendo", Handheld),
            new Platform("Wii U", "Nintendo", Home),
            new Platform("Wii", "Nintendo", Home),
            new Platform("Windows", "Microsoft", Home),
            new Platform("Xbox 360", "Microsoft", Home),
            new Platform("Xbox One", "Microsoft", Home),
            new Platform("Xbox Series X/S", "Microsoft", Home),
            new Platform("Xbox", "Microsoft", Home),
            new Platform("iOS", "Apple", Handheld)
        ];
    }

    public override void Add(Platform platform)
    {
        IList<Platform> platforms = Deserialize();
        platforms.Add(platform);
        Serialize(platforms);
    }
}

public class DeveloperData() : Data<Developer>("developers.json")
{
    protected override List<Developer> DefaultData()
    {
        return
        [
            new Developer("Insomniac Games"),
            new Developer("Nintendo EPD"),
            new Developer("Xbox Game Studios")
        ];
    }

    public override void Add(Developer developer)
    {
        IList<Developer> developers = Deserialize();
        developers.Add(developer);
        Serialize(developers);
    }
}

public class PublisherData() : Data<Publisher>("publishers.json")
{
    protected override List<Publisher> DefaultData()
    {
        return
        [
            new Publisher("Microsoft"),
            new Publisher("Nintendo"),
            new Publisher("Sony")
        ];
    }

    public override void Add(Publisher publisher)
    {
        IList<Publisher> publishers = Deserialize();
        publishers.Add(publisher);
        Serialize(publishers);
    }
}

public class SeriesData() : Data<Series>("series.json")
{
    protected override List<Series> DefaultData()
    {
        return
        [
            new Series("Mario")
        ];
    }

    public override void Add(Series series)
    {
        IList<Series> seriesList = Deserialize();
        seriesList.Add(series);
        Serialize(seriesList);
    }
}
