using System.Text.Json;

namespace GameManager.Core.Data;

public class Data<T>(T data, string jsonFile)
{
    private readonly JsonSerializerOptions _jsonSerializerOptions = new() { WriteIndented = true };

    public void Serialize()
    {
        try
        {
            List<object> items = [];

            if (File.Exists(jsonFile))
            {
                string existingJson = File.ReadAllText(jsonFile);
                if (!string.IsNullOrWhiteSpace(existingJson))
                {
                    items = JsonSerializer.Deserialize<List<object>>(existingJson);
                }
            }

            items.Add(data);

            string jsonString = JsonSerializer.Serialize(items, _jsonSerializerOptions);

            File.WriteAllText(jsonFile, jsonString);
        }
        catch (FileNotFoundException)
        {
            File.Create(jsonFile);
        }
    }

    public T Deserialize()
    {
        try
        {
            string jsonString = File.ReadAllText(jsonFile);
            data = JsonSerializer.Deserialize<T>(jsonString) ?? DefaultData();
        }
        catch (FileNotFoundException)
        {
            data = DefaultData();
            Serialize();
        }

        return data;
    }

    protected virtual T DefaultData()
    {
        throw new NotSupportedException("DefaultData method must be implemented in subclass.");
    }
}

public class GameData(string title, Genre[]? genres, Platform[]? platforms)
    : Data<Game>(new Game(title, genres, platforms), "games.json");

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