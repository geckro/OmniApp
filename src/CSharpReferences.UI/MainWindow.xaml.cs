using CSharpReferences.Core.PerformanceTests;

namespace CSharpReferences.UI;

public partial class MainWindow
{
    public MainWindow()
    {
        InitializeComponent();

        RunTests();
    }

    private static void RunTests()
    {
        TenMillion tenMillion = new();

        tenMillion.TenMillionTest();
    }
}
