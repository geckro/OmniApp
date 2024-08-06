using System.Windows;
using System.Windows.Media;

namespace GameManager.UI.Helpers;

public class StyleHelper
{
    private static StyleHelper? _instance;

    public StyleHelper()
    {
        if (_instance != null)
        {
            return;
        }

        _instance = this;
        InitializeDefaultStyles();
    }

    public static StyleHelper Instance => _instance ??= new StyleHelper();

    public FontFamily DataGridColumnHeaderFontFamily { get; private set; } = null!;
    public FontFamily DataGridRowFontFamily { get; private set; } = null!;
    public FontFamily HeaderFontFamily { get; private set; } = null!;
    public FontWeight DataGridColumnHeaderFontWeight { get; private set; }
    public FontWeight DataGridRowFontWeight { get; private set; }
    public FontWeight DataGridRowSelectedFontWeight { get; private set; }
    public FontWeight HeaderFontWeight { get; private set; }
    public SolidColorBrush ButtonColor { get; private set; } = null!;
    public SolidColorBrush ContextMenuBackgroundColor { get; private set; } = null!;
    public SolidColorBrush DataGridAlternatingRowBackgroundColor { get; private set; } = null!;
    public SolidColorBrush DataGridColumnHeaderBackgroundColor { get; private set; } = null!;
    public SolidColorBrush DataGridRowBackgroundColor { get; private set; } = null!;
    public SolidColorBrush DataGridRowSelectedBackgroundColor { get; private set; } = null!;
    public SolidColorBrush DataGridRowSelectedTextColor { get; private set; } = null!;
    public SolidColorBrush ListBoxBackgroundColor { get; private set; } = null!;
    public SolidColorBrush TextBoxBackgroundColor { get; private set; } = null!;
    public SolidColorBrush WindowBackgroundColor { get; private set; } = null!;
    public int DataGridColumnHeaderFontSize { get; private set; }
    public int DataGridRowFontSize { get; private set; }
    public int HeaderFontSize { get; private set; }

    private void InitializeDefaultStyles()
    {
        ButtonColor = new SolidColorBrush(Color.FromRgb(230, 230, 230));
        ContextMenuBackgroundColor = new SolidColorBrush(Color.FromRgb(230, 230, 230));
        DataGridAlternatingRowBackgroundColor = new SolidColorBrush(Color.FromRgb(230, 230, 230));
        DataGridColumnHeaderBackgroundColor = new SolidColorBrush(Color.FromRgb(210, 210, 210));
        DataGridColumnHeaderFontFamily = new FontFamily("Bitter");
        DataGridColumnHeaderFontSize = 18;
        DataGridColumnHeaderFontWeight = FontWeights.DemiBold;
        DataGridRowBackgroundColor = new SolidColorBrush(Color.FromRgb(240, 240, 240));
        DataGridRowFontFamily = new FontFamily("Segoe UI");
        DataGridRowFontSize = 16;
        DataGridRowFontWeight = FontWeights.Normal;
        DataGridRowSelectedBackgroundColor = new SolidColorBrush(Color.FromRgb(200, 200, 200));
        DataGridRowSelectedFontWeight = FontWeights.Medium;
        DataGridRowSelectedTextColor = new SolidColorBrush(Color.FromRgb(0, 0, 0));
        HeaderFontFamily = new FontFamily("Segoe UI");
        HeaderFontSize = 18;
        HeaderFontWeight = FontWeights.Medium;
        ListBoxBackgroundColor = new SolidColorBrush(Color.FromRgb(230, 230, 230));
        TextBoxBackgroundColor = new SolidColorBrush(Color.FromRgb(230, 230, 230));
        WindowBackgroundColor = new SolidColorBrush(Color.FromRgb(255, 255, 255));
    }
}
