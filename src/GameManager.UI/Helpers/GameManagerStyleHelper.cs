using System.Windows;
using System.Windows.Media;

namespace GameManager.UI.Helpers;

public class StyleHelper
{
    private static StyleHelper? _instance;
    public static StyleHelper Instance => _instance ??= new StyleHelper();
    public StyleHelper()
    {
        if (_instance != null)
        {
            return;
        }

        _instance = this;
        InitializeDefaultStyles();
    }

    public SolidColorBrush WindowBackgroundColor { get; private set; } = null!;
    public SolidColorBrush DataGridAlternatingRowBackgroundColor { get; private set; } = null!;
    public SolidColorBrush DataGridRowBackgroundColor { get; set; } = null!;
    public SolidColorBrush DataGridColumnHeaderBackgroundColor { get; set; } = null!;
    public FontFamily DataGridColumnHeaderFontFamily { get; set; } = null!;
    public FontWeight DataGridColumnHeaderFontWeight { get; set; }
    public FontFamily DataGridRowFontFamily { get; set; } = null!;
    public FontWeight DataGridRowFontWeight { get; set; }
    public SolidColorBrush ButtonColor { get; private set; } = null!;
    public SolidColorBrush TextBoxBackgroundColor { get; private set; } = null!;
    public SolidColorBrush ListBoxBackgroundColor { get; private set; } = null!;
    public SolidColorBrush ContextMenuBackgroundColor { get; private set; } = null!;
    public int DataGridColumnHeaderFontSize { get; set; }
    public int DataGridRowFontSize { get; set; }

    public void InitializeDefaultStyles()
    {
        WindowBackgroundColor = new SolidColorBrush(Color.FromRgb(255, 255, 255));
        DataGridColumnHeaderBackgroundColor = new SolidColorBrush(Color.FromRgb(210, 210, 210));
        DataGridAlternatingRowBackgroundColor = new SolidColorBrush(Color.FromRgb(230, 230, 230));
        DataGridRowBackgroundColor = new SolidColorBrush(Color.FromRgb(240, 240, 240));
        ButtonColor = new SolidColorBrush(Color.FromRgb(230, 230, 230));
        DataGridColumnHeaderFontFamily = new FontFamily("Bitter");
        DataGridColumnHeaderFontWeight = FontWeights.DemiBold;
        DataGridColumnHeaderFontSize = 18;
        DataGridRowFontFamily = new FontFamily("Segoe UI");
        DataGridRowFontWeight = FontWeights.Normal;
        DataGridRowFontSize = 16;
        TextBoxBackgroundColor = new SolidColorBrush(Color.FromRgb(230, 230, 230));
        ListBoxBackgroundColor = new SolidColorBrush(Color.FromRgb(230, 230, 230));
        ContextMenuBackgroundColor = new SolidColorBrush(Color.FromRgb(230, 230, 230));
    }
}
