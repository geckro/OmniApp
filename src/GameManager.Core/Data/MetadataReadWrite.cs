using OmniApp.Common.Data;

namespace GameManager.Core.Data;

public abstract class Data<T>(string jsonFile)
{
    private readonly JsonHelper _jsonHelper = new();

    public void WriteJson(ICollection<T> value)
    {
        _jsonHelper.WriteToJsonFile(value, jsonFile);
    }

    public ICollection<T> ReadFromJson()
    {
        return _jsonHelper.LoadFromJsonFile<T>(jsonFile);
    }
}

public class GameData() : Data<Game>("games.json")
{
}

public class GenreData() : Data<Genre>("genres.json")
{
}

public class PlatformData() : Data<Platform>("platforms.json")
{
}

public class DeveloperData() : Data<Developer>("developers.json")
{
}

public class PublisherData() : Data<Publisher>("publishers.json")
{
}

public class SeriesData() : Data<Series>("series.json")
{
}
