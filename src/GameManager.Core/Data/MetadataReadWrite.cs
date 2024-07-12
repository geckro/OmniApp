using OmniApp.Common.Data;

namespace GameManager.Core.Data;

public class JsonDataManager
{
    private readonly JsonHelper _jsonHelper = new();

    public void WriteJson<T>(ICollection<T> value, string jsonFile)
    {
        _jsonHelper.WriteToJsonFile(value, jsonFile);
    }

    public ICollection<T> ReadFromJson<T>(string jsonFile)
    {
        return _jsonHelper.LoadFromJsonFile<T>(jsonFile);
    }
}

public class DataManagerFactory
{
    private readonly JsonDataManager _jsonDataManager = new();

    public JsonData<T> CreateData<T>() where T : IMetadata, new()
    {
        T instance = new();

        string jsonFile = instance.JsonFile ?? string.Empty;

        if (string.IsNullOrEmpty(jsonFile))
        {
            throw new ArgumentException($"JsonFile property is not defined for type {typeof(T).Name}");
        }

        return new JsonData<T>(_jsonDataManager, jsonFile);
    }
}

public class JsonData<T>(JsonDataManager dataManager, string jsonFile)
{
    public void WriteJson(ICollection<T> value)
    {
        dataManager.WriteJson(value, jsonFile);
    }

    public void AppendAndWriteJson(T newItem)
    {
        ICollection<T> existingData = dataManager.ReadFromJson<T>(jsonFile);

        existingData.Add(newItem);

        dataManager.WriteJson(existingData, jsonFile);
    }

    public ICollection<T> ReadFromJson()
    {
        return dataManager.ReadFromJson<T>(jsonFile);
    }
}
