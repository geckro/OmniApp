using GameManager.Core.Data;
using GameManager.Core.Data.MetadataConstructors;
using OmniApp.Common.Logging;
using OmniApp.UI.Common.Helpers;
using System.Windows.Controls;

namespace GameManager.UI.ViewModels;

public class AddMetadataViewModel
{
    private readonly WindowHelper _windowHelper;
    private readonly MetadataAccessor<Genre> _genreAcc;
    private readonly MetadataAccessor<Platform> _platformAcc;
    private readonly MetadataAccessor<Series> _seriesAcc;

    public AddMetadataViewModel(WindowHelper windowHelper, MetadataAccessor<Genre> genreAcc, MetadataAccessor<Platform> platformAcc, MetadataAccessor<Series> seriesAcc)
    {
        _windowHelper = windowHelper;
        _genreAcc = genreAcc;
        _platformAcc = platformAcc;
        _seriesAcc = seriesAcc;
    }

    public Game? Game { get; set; }
    public string Category { get; set; } = string.Empty;

    public void PopulateMetadataListBoxes(ListBox currentListBox, ListBox newListBox)
    {
        if (Game == null)
        {
            Logger.Warning(LogClass.GameMgrUiViewModels, "Game is null. Continuing to populate Metadata List Boxes...");
        }

        switch (Category)
        {
            case "Platform":
                PopulateListBoxes(currentListBox, newListBox, Game?.Platforms, _platformAcc);
                break;

            case "Genre":
                PopulateListBoxes(currentListBox, newListBox, Game?.Genres, _genreAcc);
                break;

            case "Series":
                PopulateListBoxes(currentListBox, newListBox, Game?.Series, _seriesAcc);
                break;
        }
    }

    private static void PopulateListBoxes<T>(ListBox currentListBox, ListBox newListBox, IEnumerable<T>? currentItemIds, MetadataAccessor<T> accessor) where T : IMetadata
    {
        HashSet<Guid> existingIds = currentItemIds?.Select(item => item.Id).ToHashSet() ?? [];

        if (currentItemIds != null)
        {
            foreach (T itemId in currentItemIds)
            {
                T? item = accessor.GetItemById(itemId.Id);
                if (item == null)
                {
                    continue;
                }

                currentListBox.Items.Add(item.Name);
                Logger.Debug(LogClass.GameMgrUiViewModels, $"Added to currentListBox: {item.Name} (ID: {item.Id})");
            }
        }

        foreach (T item in accessor.LoadMetadata())
        {
            if (!existingIds.Contains(item.Id))
            {
                newListBox.Items.Add(item.Name);
            }
        }
    }
}
