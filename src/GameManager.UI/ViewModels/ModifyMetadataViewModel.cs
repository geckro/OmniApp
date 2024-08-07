using GameManager.Core.Data;
using GameManager.Core.Data.MetadataConstructors;
using OmniApp.UI.Common;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace GameManager.UI.ViewModels;

public class ModifyMetadataViewModel
{
    private readonly MetadataAccessor<Developer> _developerAcc;
    private readonly MetadataAccessor<Publisher> _publisherAcc;
    private readonly MetadataAccessor<Genre> _genreAcc;
    private readonly MetadataAccessor<Platform> _platformAcc;
    private readonly MetadataAccessor<Series> _seriesAcc;

    public ModifyMetadataViewModel(
            MetadataAccessor<Developer> developerAcc,
            MetadataAccessor<Publisher> publisherAcc,
            MetadataAccessor<Genre> genreAcc,
            MetadataAccessor<Platform> platformAcc,
            MetadataAccessor<Series> seriesAcc)
    {

        _developerAcc = developerAcc;
        _publisherAcc = publisherAcc;
        _genreAcc = genreAcc;
        _platformAcc = platformAcc;
        _seriesAcc = seriesAcc;

        InitializeCommands();
    }

    public ObservableCollection<string?> MetadataItems { get; } = [];
    public ICommand DeveloperCommand { get; private set; } = null!;
    public ICommand PublisherCommand { get; private set; } = null!;
    public ICommand GenreCommand { get; private set; } = null!;
    public ICommand PlatformCommand { get; private set; } = null!;
    public ICommand SeriesCommand { get; private set; } = null!;

    private void InitializeCommands()
    {
        DeveloperCommand = new RelayCommand<object>(_ => PopulateItemsControl(_developerAcc));
        PublisherCommand = new RelayCommand<object>(_ => PopulateItemsControl(_publisherAcc));
        GenreCommand = new RelayCommand<object>(_ => PopulateItemsControl(_genreAcc));
        PlatformCommand = new RelayCommand<object>(_ => PopulateItemsControl(_platformAcc));
        SeriesCommand = new RelayCommand<object>(_ => PopulateItemsControl(_seriesAcc));
    }

    private void PopulateItemsControl<T>(MetadataAccessor<T> metadataAcc) where T : IMetadata
    {
        MetadataItems.Clear();
        ICollection<T> metadata = metadataAcc.LoadMetadata();
        foreach (T item in metadata)
        {
            if (item.Name != "")
            {
                MetadataItems.Add(item.Name);
                continue;
            }
            if (item.Id != Guid.Empty)
            {
                MetadataItems.Add(item.Id.ToString());
            }
        }
    }
}
