using GameManager.Core.Data;
using System.Windows;
using System.Windows.Controls;

namespace GameManager.UI.Helpers;

public static class MetadataHelper
{
    public static void AddNewMetadata<T, TU>(TextBox metadataTextBox, TU metadataData)
        where T : IMetadata
        where TU : IMetadataData<T>, new()
    {
        string metadata = metadataTextBox.Text;

        if (string.IsNullOrWhiteSpace(metadata))
        {
            MessageBox.Show(
                "Please enter valid metadata.",
                "Error",
                MessageBoxButton.OK,
                MessageBoxImage.Error
            );
            return;
        }

        T newMetadata = (T)Activator.CreateInstance(typeof(T), metadata)!;
        metadataData.Add(newMetadata ?? throw new InvalidOperationException());

        MessageBox.Show($"Added {newMetadata.Name} in {newMetadata.GetType()}");
    }
}
