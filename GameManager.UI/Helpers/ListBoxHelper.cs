using System.Reflection;
using System.Windows.Controls;
using GameManager.Core.Data;

namespace GameManager.UI.Helpers;

public class ListBoxHelper
{
    private readonly GenreData _genreData = new();
    private readonly PlatformData _platformData = new();

    private static void UpdateListBox<T>(
        ListBox listBox,
        IEnumerable<T> metadataList
    )
        where T : IMetadata
    {
        listBox.Items.Clear();
        foreach (T metadata in metadataList)
        {
            string displayText = metadata.Name;

            PropertyInfo? categoryProperty = metadata.GetType().GetProperty("Category");
            if (categoryProperty != null)
            {
                string category = (categoryProperty.GetValue(metadata) as string);
                if (!string.IsNullOrEmpty(category))
                {
                    displayText = $"{category} - {metadata.Name}";
                }
            }

            listBox.Items.Add(displayText);
        }
    }

    public void UpdateGenreListBox(ListBox listBox)
    {
        UpdateListBox(listBox, _genreData.Deserialize());
    }

    public void UpdatePlatformListBox(ListBox listBox)
    {
        UpdateListBox(listBox, _platformData.Deserialize());
    }
}