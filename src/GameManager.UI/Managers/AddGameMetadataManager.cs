using GameManager.Core.Data;
using GameManager.Core.Data.MetadataConstructors;
using GameManager.UI.Windows;
using OmniApp.Common.Logging;
using System.Collections.ObjectModel;
using System.ComponentModel;

namespace GameManager.UI.Managers;

public sealed class AddGameMetadataManager : INotifyPropertyChanged
{
    // private readonly AddGameWindow _addGame;
    private readonly Dictionary<string, ObservableCollection<IMetadata>> _metadataCollections;
    private readonly MetadataAccessorFactory _mtdAccessorFactory;
    private string _currentCategory = string.Empty;

    public AddGameMetadataManager(AddGameWindow addGame, MetadataAccessorFactory mtdAccessorFactory)
    {
        // _addGame = addGame;
        _mtdAccessorFactory = mtdAccessorFactory;
        _metadataCollections = new Dictionary<string, ObservableCollection<IMetadata>>();
        CurrentMetadata = new ObservableCollection<IMetadata>();

        InitializeMetadataCollections();
    }

    public ObservableCollection<IMetadata> CurrentMetadata { get; }

    public event PropertyChangedEventHandler? PropertyChanged;

    public void UpdateCurrentCategory(string category)
    {
        Logger.Debug(LogClass.GameMgrUiManagers, $"Updating current category to: {category}");

        _currentCategory = category;
        RefreshCurrentMetadata();
    }

    public void UpdateSearchText(string searchText)
    {
        Logger.Debug(LogClass.GameMgrUiManagers, $"Updating search text to: {_currentCategory}:{searchText}");
        if (string.IsNullOrEmpty(_currentCategory))
        {
            return;
        }

        CurrentMetadata.Clear();
        foreach (IMetadata item in _metadataCollections[_currentCategory]
                         .Where(m => m.Name.Contains(searchText.Trim(), StringComparison.CurrentCultureIgnoreCase)))
        {
            // This foreach loop should work. If it doesn't, good luck.
            // Logger.Debug(LogClass.GameMgrUiManagers, $"Item: {item.Name}, {item.Id}");

            CurrentMetadata.Add(item);
        }

        OnPropertyChanged(nameof(CurrentMetadata));
    }

    private void InitializeMetadataCollections()
    {
        _metadataCollections["Genres"] =
                new ObservableCollection<IMetadata>(_mtdAccessorFactory.CreateMetadataAccessor<Genre>().LoadMetadata());
        _metadataCollections["Platforms"] =
                new ObservableCollection<IMetadata>(_mtdAccessorFactory.CreateMetadataAccessor<Platform>()
                        .LoadMetadata());
        _metadataCollections["Developers"] =
                new ObservableCollection<IMetadata>(_mtdAccessorFactory.CreateMetadataAccessor<Developer>()
                        .LoadMetadata());
        _metadataCollections["Publishers"] =
                new ObservableCollection<IMetadata>(_mtdAccessorFactory.CreateMetadataAccessor<Publisher>()
                        .LoadMetadata());
        _metadataCollections["Series"] =
                new ObservableCollection<IMetadata>(_mtdAccessorFactory.CreateMetadataAccessor<Series>()
                        .LoadMetadata());
        Logger.Debug(LogClass.GameMgrUiManagers, "Metadata collections initialized");
    }

    private void RefreshCurrentMetadata()
    {
        Logger.Debug(LogClass.GameMgrUiManagers, "Refreshing current metadata");

        CurrentMetadata.Clear();
        foreach (IMetadata item in _metadataCollections[_currentCategory])
        {
            CurrentMetadata.Add(item);
        }

        OnPropertyChanged(nameof(CurrentMetadata));
    }

    public void CreateNewMetadataItem(string name)
    {
        if (string.IsNullOrEmpty(_currentCategory))
        {
            return;
        }

        IMetadata newItem;
        switch (_currentCategory)
        {
            case "Genres":
                newItem = new Genre { Id = Guid.NewGuid(), Name = name };
                _mtdAccessorFactory.CreateMetadataAccessor<Genre>().AddItemAndSave((Genre)newItem);
                break;
            case "Platforms":
                newItem = new Platform { Id = Guid.NewGuid(), Name = name };
                _mtdAccessorFactory.CreateMetadataAccessor<Platform>().AddItemAndSave((Platform)newItem);
                break;
            case "Developers":
                newItem = new Developer { Id = Guid.NewGuid(), Name = name };
                _mtdAccessorFactory.CreateMetadataAccessor<Developer>().AddItemAndSave((Developer)newItem);
                break;
            case "Publishers":
                newItem = new Publisher { Id = Guid.NewGuid(), Name = name };
                _mtdAccessorFactory.CreateMetadataAccessor<Publisher>().AddItemAndSave((Publisher)newItem);
                break;
            case "Series":
                newItem = new Series { Id = Guid.NewGuid(), Name = name };
                _mtdAccessorFactory.CreateMetadataAccessor<Series>().AddItemAndSave((Series)newItem);
                break;
            default:
                return;
        }

        _metadataCollections[_currentCategory].Add(newItem);
        CurrentMetadata.Add(newItem);
        OnPropertyChanged(nameof(CurrentMetadata));
    }

    private void OnPropertyChanged(string propertyName)
    {
        Logger.Debug(LogClass.GameMgrUiManagers, $"Property changed: {propertyName}");
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}
