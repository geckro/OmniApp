using System.Windows;
using System.Windows.Controls;
using GameManager.Core.Data;

namespace GameManager.UI.Helpers;

public static class MetadataHelper
{
    public static void AddNewMetadata<T, TU>(TextBox metadataTextBox, TU metadataData)
        where T : IMetadata
        where TU : IMetadataData<T>, new()
    {
        string metadata = metadataTextBox.Text;

        if (string.IsNullOrWhiteSpace(metadata)) return;

        T newMetadata = (T)Activator.CreateInstance(typeof(T), metadata);
        metadataData.Add(newMetadata);

        MessageBox.Show($"Added {newMetadata.Name} in {newMetadata.GetType()}");
    }
}