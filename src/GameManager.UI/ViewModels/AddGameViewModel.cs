using GameManager.Core.Data;
using GameManager.UI.Managers;
using GameManager.UI.Windows;
using OmniApp.Common.Logging;
using OmniApp.UI.Common;
using OmniApp.UI.Common.Helpers;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using System.Windows;
using System.Windows.Input;

namespace GameManager.UI.ViewModels;

public sealed class AddGameViewModel : ViewModelBase
{
    private readonly WindowHelper _windowHelper;
    private Visibility _listBoxVisibility = Visibility.Collapsed;
    private AddGameMetadataManager _metadataManager = null!;
    private string _metadataSearchText = null!;
    private string _selectedCategory = null!;

    public AddGameViewModel(WindowHelper windowHelper)
    {
        _windowHelper = windowHelper;
        InitializeCommands();
    }

    [SuppressMessage("ReSharper", "UnusedMember.Global")]
    public static ObservableCollection<string> MetadataCategories => ["Genres", "Platforms", "Developers", "Publishers", "Series"];

    [SuppressMessage("ReSharper", "UnusedMember.Global")]
    public string SelectedCategory
    {
        get => _selectedCategory;
        set
        {
            if (SetProperty(ref _selectedCategory, value))
            {
                _metadataManager.UpdateCurrentCategory(value);
                MetadataSearchText = string.Empty; // Reset search text when switching categories
                UpdateListBoxVisibility();
            }
        }
    }

    [SuppressMessage("ReSharper", "UnusedMember.Global")]
    public string MetadataSearchText
    {
        get => _metadataSearchText;
        set
        {
            if (SetProperty(ref _metadataSearchText, value))
            {
                _metadataManager.UpdateSearchText(value);
                UpdateListBoxVisibility();
            }
        }
    }

    public ObservableCollection<IMetadata> CurrentMetadata => _metadataManager.CurrentMetadata;
    public ObservableCollection<string> SelectedMetadata => _metadataManager.SelectedMetadata;

    public Visibility ListBoxVisibility
    {
        get => _listBoxVisibility;
        set
        {
            Logger.Debug(LogClass.GameMgrUiViewModels, $"Updating ListBoxVisibility from {_listBoxVisibility} to {value}");
            SetProperty(ref _listBoxVisibility, value);
        }
    }

    public ICommand PickDateCommand { get; private set; } = null!;

    public void Initialize(AddGameMetadataManager metadataManager)
    {
        _metadataManager = metadataManager;
        _metadataManager.PropertyChanged += MetadataManager_PropertyChanged;
        _metadataSearchText = _selectedCategory = string.Empty;
    }

    private void MetadataManager_PropertyChanged(object? sender, PropertyChangedEventArgs e)
    {
        if (e.PropertyName != nameof(AddGameMetadataManager.CurrentMetadata))
        {
            return;
        }

        OnPropertyChanged(nameof(CurrentMetadata));
        UpdateListBoxVisibility();
    }

    private void UpdateListBoxVisibility()
    {
        Visibility oldVisibility = ListBoxVisibility;

        ListBoxVisibility = !string.IsNullOrEmpty(SelectedCategory) ? Visibility.Visible : Visibility.Collapsed;

        Logger.Debug(LogClass.GameMgrUiViewModels,
                $"Updating listbox visibility: New value {ListBoxVisibility}, Old vis: {oldVisibility}");
    }

    private void InitializeCommands()
    {
        PickDateCommand = new RelayCommand<object>(_ => PickDate());
    }

    private void PickDate()
    {
        _windowHelper.ShowDialogWindow<GameDateSetterWindow>();
    }
}
