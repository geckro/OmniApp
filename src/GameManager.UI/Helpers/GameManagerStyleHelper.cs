using System.Windows;
using System.Windows.Media;

namespace GameManager.UI.Helpers;

/// <summary>
///     These properties are used programatically.
/// </summary>
public static class Styles
{
    public static SolidColorBrush WindowBackgroundColor { get; set; } = null!;
    public static SolidColorBrush DataGridAlternatingRowBackgroundColor { get; set; } = null!;
    public static SolidColorBrush DataGridRowBackgroundColor { get; set; } = null!;
    public static SolidColorBrush DataGridColumnHeaderBackgroundColor { get; set; } = null!;
    public static FontFamily DataGridColumnHeaderFontFamily { get; set; } = null!;
    public static FontWeight DataGridColumnHeaderFontWeight { get; set; }
    public static FontFamily DataGridRowFontFamily { get; set; } = null!;
    public static FontWeight DataGridRowFontWeight { get; set; }
    public static SolidColorBrush ButtonColor { get; set; } = null!;
    public static SolidColorBrush TextBoxBackgroundColor { get; set; } = null!;
    public static SolidColorBrush ListBoxBackgroundColor { get; set; } = null!;
}

/// <summary>
///     These properties are used for XAML controls.
/// </summary>
public class GameManagerStyleHelper
{
    public static SolidColorBrush WindowBackgroundColor => Styles.WindowBackgroundColor;
    public static SolidColorBrush DataGridAlternatingRowBackgroundColor => Styles.DataGridAlternatingRowBackgroundColor;
    public static SolidColorBrush DataGridRowBackgroundColor => Styles.DataGridRowBackgroundColor;
    public static SolidColorBrush DataGridColumnHeaderBackgroundColor => Styles.DataGridColumnHeaderBackgroundColor;
    public static FontFamily DataGridColumnHeaderFontFamily => Styles.DataGridColumnHeaderFontFamily;
    public static FontWeight DataGridColumnHeaderFontWeight => Styles.DataGridColumnHeaderFontWeight;
    public static FontFamily DataGridRowFontFamily => Styles.DataGridRowFontFamily;
    public static FontWeight DataGridRowFontWeight => Styles.DataGridRowFontWeight;
    public static SolidColorBrush ButtonColor => Styles.ButtonColor;
    public static SolidColorBrush TextBoxBackgroundColor => Styles.TextBoxBackgroundColor;
    public static SolidColorBrush ListBoxBackgroundColor => Styles.ListBoxBackgroundColor;

    public GameManagerStyleHelper()
    {
        Styles.WindowBackgroundColor = new SolidColorBrush(Color.FromRgb(255, 255, 255));

        Styles.DataGridColumnHeaderBackgroundColor = new SolidColorBrush(Color.FromRgb(210, 210, 210));
        Styles.DataGridAlternatingRowBackgroundColor = new SolidColorBrush(Color.FromRgb(230, 230, 230));
        Styles.DataGridRowBackgroundColor = new SolidColorBrush(Color.FromRgb(240, 240, 240));

        Styles.ButtonColor = new SolidColorBrush(Color.FromRgb(230, 230, 230));

        Styles.DataGridColumnHeaderFontFamily = new FontFamily("Bitter");
        Styles.DataGridColumnHeaderFontWeight = FontWeights.DemiBold;
        Styles.DataGridRowFontFamily = new FontFamily("Segoe UI");
        Styles.DataGridRowFontWeight = FontWeights.Normal;

        Styles.TextBoxBackgroundColor = new SolidColorBrush(Color.FromRgb(230, 230, 230));;
        Styles.ListBoxBackgroundColor = new SolidColorBrush(Color.FromRgb(230, 230, 230));;
    }

}
