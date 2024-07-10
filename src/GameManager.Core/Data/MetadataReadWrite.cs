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
        DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingDefault, PropertyNameCaseInsensitive = true
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
            new Platform {Name = "2600", Company = "Atari", Type = Home},
            new Platform {Name = "32X", Company = "Sega", Type = Home},
            new Platform {Name = "3DS", Company = "Nintendo", Type = Handheld},
            new Platform {Name = "5200", Company = "Atari", Type = Home},
            new Platform {Name = "7800", Company = "Atari", Type = Home},
            new Platform {Name = "Android", Company = "Google", Type = Home},
            new Platform {Name = "DS", Company = "Nintendo", Type = Handheld},
            new Platform {Name = "Dreamcast", Company = "Sega", Type = Home},
            new Platform {Name = "Game Boy Advance", Company = "Nintendo", Type = Handheld},
            new Platform {Name = "Game Boy Color", Company = "Nintendo", Type = Handheld},
            new Platform {Name = "Game Boy", Company = "Nintendo", Type = Handheld},
            new Platform {Name = "Game Gear", Company = "Sega", Type = Handheld},
            new Platform {Name = "GameCube", Company = "Nintendo", Type = Home},
            new Platform {Name = "Genesis", Company = "Sega", Type = Home},
            new Platform {Name = "Jaguar", Company = "Atari", Type = Home},
            new Platform {Name = "Linux", Type = Computer},
            new Platform {Name = "Lynx", Company = "Atari", Type = Handheld},
            new Platform {Name = "MacOS", Company = "Apple", Type = Computer},
            new Platform {Name = "Master System", Company = "Sega", Type = Home},
            new Platform {Name = "NES", Company = "Nintendo", Type = Home},
            new Platform {Name = "Nintendo 64", Company = "Nintendo", Type = Home},
            new Platform {Name = "PlayStation 2", Company = "Sony", Type = Home},
            new Platform {Name = "PlayStation 3", Company = "Sony", Type = Home},
            new Platform {Name = "PlayStation 4", Company = "Sony", Type = Home},
            new Platform {Name = "PlayStation 5", Company = "Sony", Type = Home},
            new Platform {Name = "PlayStation Portable", Company = "Sony", Type = Handheld},
            new Platform {Name = "PlayStation Vita", Company = "Sony", Type = Handheld},
            new Platform {Name = "PlayStation", Company = "Sony", Type = Home},
            new Platform {Name = "Saturn", Company = "Sega", Type = Home},
            new Platform {Name = "Sega CD", Company = "Sega", Type = Home},
            new Platform {Name = "Super Nintendo", Company = "Nintendo", Type = Home},
            new Platform {Name = "Switch", Company = "Nintendo", Type = Hybrid},
            new Platform {Name = "Virtual Boy", Company = "Nintendo", Type = Handheld},
            new Platform {Name = "Wii U", Company = "Nintendo", Type = Home},
            new Platform {Name = "Wii", Company = "Nintendo", Type = Home},
            new Platform {Name = "Windows", Company = "Microsoft", Type = Computer},
            new Platform {Name = "Xbox 360", Company = "Microsoft", Type = Home},
            new Platform {Name = "Xbox One", Company = "Microsoft", Type = Home},
            new Platform {Name = "Xbox Series X/S", Company = "Microsoft", Type = Home},
            new Platform {Name = "Xbox", Company = "Microsoft", Type = Home},
            new Platform {Name = "iOS", Company = "Apple", Type = Handheld}
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
