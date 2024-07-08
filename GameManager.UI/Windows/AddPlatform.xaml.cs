using System.Windows;
using GameManager.Core.Data;
using GameManager.UI.Helpers;

namespace GameManager.UI.Windows;

public partial class AddPlatform
{
    public AddPlatform()
    {
        InitializeComponent();
        ListBoxHelper listBoxHelper = new();
        listBoxHelper.UpdatePlatformListBox(PlatformListBox);
    }

    private void AddNewPlatform_Click(object sender, RoutedEventArgs e)
    {
        MetadataHelper.AddNewMetadata<Platform, PlatformData>(PlatformBox, new PlatformData());
    }
}