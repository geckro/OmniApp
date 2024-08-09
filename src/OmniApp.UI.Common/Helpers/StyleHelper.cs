using System.Windows;
using System.Windows.Media;

namespace OmniApp.UI.Common.Helpers;

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

    #region Base Styles

    /// <summary>
    ///     The base font weight for any resource.
    ///     If any other font weights are not specified, it will use this weight and add or subtract a font weight.
    /// </summary>
    public FontWeight BaseFontWeight { get; private set; }

    /// <summary>
    ///     The base font size for any resource.
    ///     If any other font weights are not specified, it will use this weight and add or subtract a font size.
    /// </summary>
    public double BaseFontSize { get; private set; }

    /// <summary>
    ///     The base font family for any resource.
    ///     If any other font weights are not specified, it will use this weight and add or subtract a font size.
    /// </summary>
    public FontFamily BaseFontFamily { get; private set; } = null!;

    /// <summary>
    ///     The base font color for any resource.
    ///     If the font color is not specified, it will use this color.
    /// </summary>
    public SolidColorBrush BaseFontColor { get; private set; } = null!;

    public SolidColorBrush BaseBackgroundColor { get; private set; } = null!;

    #endregion

    #region Game Table Styles

    public FontFamily DataGridColumnHeaderFontFamily { get; private set; } = null!;
    public FontFamily DataGridRowFontFamily { get; private set; } = null!;
    public FontWeight DataGridColumnHeaderFontWeight { get; private set; }
    public FontWeight DataGridRowFontWeight { get; private set; }
    public FontWeight DataGridRowSelectedFontWeight { get; private set; }
    public SolidColorBrush DataGridAlternatingRowBackgroundColor { get; private set; } = null!;
    public SolidColorBrush DataGridColumnHeaderBackgroundColor { get; private set; } = null!;
    public SolidColorBrush DataGridRowBackgroundColor { get; private set; } = null!;
    public SolidColorBrush DataGridRowSelectedBackgroundColor { get; private set; } = null!;
    public SolidColorBrush DataGridRowSelectedTextColor { get; private set; } = null!;
    public double DataGridColumnHeaderFontSize { get; private set; }
    public double DataGridRowFontSize { get; private set; }

    #endregion

    #region Header Styles

    public SolidColorBrush HeaderFontColor { get; private set; } = null!;
    public FontFamily HeaderFontFamily { get; private set; } = null!;
    public FontWeight HeaderFontWeight { get; private set; }
    public double HeaderFontSize { get; private set; }

    #endregion

    #region List Styles

    public SolidColorBrush ListBackgroundColor { get; private set; } = null!;

    #endregion

    #region Button Styles

    public SolidColorBrush ButtonBackgroundColor { get; private set; } = null!;
    public SolidColorBrush ButtonFontColor { get; private set; } = null!;
    public FontFamily ButtonFontFamily { get; private set; } = null!;
    public FontWeight ButtonFontWeight { get; private set; }
    public double ButtonFontSize { get; private set; }

    #endregion
    public SolidColorBrush ContextMenuBackgroundColor { get; private set; } = null!;
    public SolidColorBrush ContextMenuBorderColor { get; private set; } = null!;
    public SolidColorBrush ContextMenuSelectedColor { get; private set; } = null!;
    public SolidColorBrush ContextMenuDisabledColor { get; private set; } = null!;
    public SolidColorBrush ContextMenuForegroundColor { get; private set; } = null!;
    public SolidColorBrush TextBoxBackgroundColor { get; private set; } = null!;
    public SolidColorBrush DatePickerBackgroundColor { get; private set; } = null!;

    private void InitializeDefaultStyles()
    {
        #region Base Styles Initialization

        BaseFontWeight = FontWeights.Normal;
        BaseFontSize = 14.0d;
        BaseFontFamily = new FontFamily("Segoe UI");
        BaseFontColor = new SolidColorBrush(Colors.Black);
        BaseBackgroundColor = new SolidColorBrush(Colors.White);

        #endregion

        #region Game Table Styles Initialization

        DataGridAlternatingRowBackgroundColor = new SolidColorBrush(Color.FromRgb(230, 230, 230));
        DataGridColumnHeaderBackgroundColor = new SolidColorBrush(Color.FromRgb(210, 210, 210));
        DataGridColumnHeaderFontFamily = new FontFamily("Bitter");
        DataGridColumnHeaderFontSize = 18;
        DataGridColumnHeaderFontWeight = FontWeights.DemiBold;
        DataGridRowBackgroundColor = new SolidColorBrush(Color.FromRgb(240, 240, 240));
        DataGridRowFontFamily = BaseFontFamily;
        DataGridRowFontSize = BaseFontSize;
        DataGridRowFontWeight = BaseFontWeight;
        DataGridRowSelectedBackgroundColor = new SolidColorBrush(Color.FromRgb(200, 200, 200));
        DataGridRowSelectedFontWeight = FontWeights.Medium;
        DataGridRowSelectedTextColor = BaseFontColor;

        #endregion

        #region Header Styles Initialization

        HeaderFontFamily = BaseFontFamily;
        HeaderFontSize = BaseFontSize + 2.0d;
        HeaderFontWeight = FontWeights.Medium;
        HeaderFontColor = new SolidColorBrush(Colors.DimGray);

        #endregion

        #region List Styles Initialization

        ListBackgroundColor = new SolidColorBrush(Color.FromRgb(230, 230, 230));

        #endregion

        #region Button Styles

        ButtonBackgroundColor = new SolidColorBrush(Color.FromRgb(230, 230, 230));
        ButtonFontColor = BaseFontColor;
        ButtonFontFamily = BaseFontFamily;
        ButtonFontWeight = BaseFontWeight;
        ButtonFontSize = BaseFontSize;

        #endregion

        ContextMenuBackgroundColor = new SolidColorBrush(Color.FromRgb(230, 230, 230));
        ContextMenuBorderColor = new SolidColorBrush(Color.FromRgb(220, 220, 220));
        ContextMenuSelectedColor = new SolidColorBrush(Color.FromRgb(205, 205, 205));
        ContextMenuDisabledColor = new SolidColorBrush(Color.FromRgb(180, 180, 180));
        ContextMenuForegroundColor = BaseFontColor;
        DatePickerBackgroundColor = new SolidColorBrush(Color.FromRgb(230, 230, 230));

        TextBoxBackgroundColor = new SolidColorBrush(Color.FromRgb(230, 230, 230));
    }
}
