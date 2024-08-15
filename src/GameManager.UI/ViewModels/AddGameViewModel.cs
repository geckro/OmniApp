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
    public static ObservableCollection<string> MetadataCategories =>
            ["Genres", "Platforms", "Developers", "Publishers", "Series"];

    [SuppressMessage("ReSharper", "UnusedMember.Global")]
    public string SelectedCategory
    {
        get => _selectedCategory;
        set
        {
            if (_selectedCategory == value)
            {
                return;
            }

            _selectedCategory = value;
            _metadataManager.UpdateCurrentCategory(value);
            OnPropertyChanged(nameof(SelectedCategory));
            MetadataSearchText = string.Empty; // reset search text when you switch categories
            UpdateListBoxVisibility();
        }
    }

    [SuppressMessage("ReSharper", "UnusedMember.Global")]
    public string MetadataSearchText
    {
        get => _metadataSearchText;
        set
        {
            if (_metadataSearchText == value)
            {
                return;
            }

            _metadataSearchText = value;
            _metadataManager.UpdateSearchText(value);
            OnPropertyChanged(nameof(MetadataSearchText));
            UpdateListBoxVisibility();
        }
    }

    public ObservableCollection<IMetadata> CurrentMetadata => _metadataManager.CurrentMetadata;
    public ObservableCollection<string> SelectedMetadata => _metadataManager.SelectedMetadata;

    public Visibility ListBoxVisibility
    {
        get => _listBoxVisibility;
        set
        {
            Logger.Debug(LogClass.GameMgrUiViewModels,
                    $"ListBoxVisibility is getting updated from {_listBoxVisibility} to {value}");
            if (_listBoxVisibility == value)
            {
                return;
            }

            _listBoxVisibility = value;
            OnPropertyChanged(nameof(ListBoxVisibility));
        }
    }

    // ReSharper disable UnusedAutoPropertyAccessor.Global
    public ICommand PickDateCommand { get; private set; } = null!;
    // ReSharper restore UnusedAutoPropertyAccessor.Global

    public override event PropertyChangedEventHandler? PropertyChanged;

    public void Initialize(AddGameMetadataManager metadataManager)
    {
        _metadataManager = metadataManager;
        _metadataManager.PropertyChanged += MetadataManager_PropertyChanged;
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

        if (CurrentMetadata.Any() && MetadataSearchText.Trim() != string.Empty)
        {
            ListBoxVisibility = Visibility.Visible;
        }
        else
        {
            ListBoxVisibility = Visibility.Collapsed;
        }

        Logger.Debug(LogClass.GameMgrUiViewModels,
                $"Updating listbox visibility: New value {ListBoxVisibility}, Old vis: {oldVisibility}");
    }

    protected override void OnPropertyChanged(string? propertyName = null)
    {
        Logger.Debug(LogClass.GameMgrUiManagers, $"OnPropertyChanged called with property name: {propertyName}");
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
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
