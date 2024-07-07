using System.Windows;
using GameManager.Core.Data;

namespace GameManager.UI.Windows;

public partial class AddPlatform
{
    public AddPlatform()
    {
        InitializeComponent();
    }

    private void AddNewPlatform_Click(object sender, RoutedEventArgs e)
    {
        string platformToAdd = PlatformBox.Text;

        if (string.IsNullOrWhiteSpace(platformToAdd)) return;

        Platform newPlatform = new(platformToAdd);
        PlatformData platformData = new();

        platformData.Add(newPlatform);
    }
}