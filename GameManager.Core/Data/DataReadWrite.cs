using System.Text.Json;

namespace GameManager.Core.Data;

public interface IMetadataData<in T> where T : IMetadata
{
    void Add(T metadata);
}

public abstract class Data<T>(string jsonFile) : IMetadataData<T> where T : IMetadata
{
    private readonly JsonSerializerOptions _jsonSerializerOptions = new();

    protected void Serialize(List<T> items)
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

    public List<T> Deserialize()
    {
        try
        {
            string jsonString = File.ReadAllText(jsonFile);
            return JsonSerializer.Deserialize<List<T>>(jsonString) ?? DefaultData();
        }
        catch (FileNotFoundException)
        {
            List<T> dataList = DefaultData();
            Serialize(dataList);
            return dataList;
        }
    }

    protected abstract List<T> DefaultData();

    public abstract void Add(T item);
}

public class GameData() : Data<Game>("games.json")
{
    public override void Add(Game game)
    {
        List<Game> games = Deserialize();
        games.Add(game);
        Serialize(games);
    }

    public List<Game> GetGames()
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
        List<Genre> genres = Deserialize();
        genres.Add(genre);
        Serialize(genres);
    }
}

public class PlatformData() : Data<Platform>("platforms.json")
{
    protected override List<Platform> DefaultData()
    {
        return
        [
            new Platform("NES"),
            new Platform("Super Nintendo"),
            new Platform("Xbox 360")
        ];
    }

    public override void Add(Platform platform)
    {
        List<Platform> platforms = Deserialize();
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
        List<Developer> developers = Deserialize();
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
        List<Publisher> publishers = Deserialize();
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
        List<Series> seriess = Deserialize();
        seriess.Add(series);
        Serialize(seriess);
    }
}