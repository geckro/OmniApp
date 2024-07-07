using System.Text.Json;

namespace GameManager.Core.Data;

public class Data<T>(T data, string jsonFile)
{
    public void Serialize()
    {
        string jsonString = JsonSerializer.Serialize(data);
        File.WriteAllText(jsonFile, jsonString);
    }

    public T Deserialize()
    {
        try
        {
            string jsonString = File.ReadAllText(jsonFile);
            return JsonSerializer.Deserialize<T>(jsonString)!;
        }
        catch (FileNotFoundException)
        {
            T defaultData = DefaultData();
            Serialize();
            return defaultData;
        }
    }

    protected virtual T DefaultData()
    {
        throw new NotImplementedException("DefaultData method must be implemented in subclass.");
    }
}

public class GameData(string title, Genre[]? genres, Platform[]? platforms)
    : Data<Game>(new Game(title, genres, platforms), "games.json")
{
}

public class GenreData() : Data<List<Genre>>([], "genres.json")
{
    protected override List<Genre> DefaultData()
    {
        return
        [
            new Genre("Platformer"),
            new Genre("Shooter")
        ];
    }
}

public class PlatformData() : Data<List<Platform>>([], "platforms.json")
{
    protected override List<Platform> DefaultData()
    {
        return
        [
            new Platform("NES"),
            new Platform("SNES")
        ];
    }
}