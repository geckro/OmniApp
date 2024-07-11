using GameManager.Core.Data;
using System.Reflection;
using System.Windows.Controls;

namespace GameManager.UI.Helpers;

public class ListBoxHelper
{
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
                string? category = categoryProperty.GetValue(metadata) as string;
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
        UpdateListBox(listBox, new DataManagerFactory().CreateData<Genre>().ReadFromJson());
    }

    public void UpdatePlatformListBox(ListBox listBox)
    {
        UpdateListBox(listBox, new DataManagerFactory().CreateData<Platform>().ReadFromJson());
    }
}
