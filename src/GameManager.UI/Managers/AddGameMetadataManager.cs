using GameManager.Core.Data;
using GameManager.Core.Data.MetadataConstructors;
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

    public AddGameMetadataManager(MetadataAccessorFactory mtdAccessorFactory)
    {
        // _addGame = addGame;
        _mtdAccessorFactory = mtdAccessorFactory;
        _metadataCollections = new Dictionary<string, ObservableCollection<IMetadata>>();
        CurrentMetadata = new ObservableCollection<IMetadata>();
        SelectedMetadata = new ObservableCollection<string>();

        InitializeMetadataCollections();
    }

    public ObservableCollection<IMetadata> CurrentMetadata { get; }
    public ObservableCollection<string> SelectedMetadata { get; }

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

        List<IMetadata> filteredItems = _metadataCollections[_currentCategory]
                .Where(m => m.Name.Contains(searchText.Trim(), StringComparison.CurrentCultureIgnoreCase))
                .ToList();

        CurrentMetadata.Clear();
        foreach (IMetadata item in filteredItems)
        {
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

        List<string> selectedMetadataStrings = SelectedMetadata.ToList();

        CurrentMetadata.Clear();

        foreach (IMetadata item in _metadataCollections[_currentCategory])
        {
            CurrentMetadata.Add(item);
        }

        SelectedMetadata.Clear();
        foreach (string selectedItem in selectedMetadataStrings)
        {
            SelectedMetadata.Add(selectedItem);
        }

        OnPropertyChanged(nameof(CurrentMetadata));
    }

    private void OnPropertyChanged(string propertyName)
    {
        Logger.Debug(LogClass.GameMgrUiManagers, $"Property changed: {propertyName}");
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}
