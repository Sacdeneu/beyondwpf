
using BeyondWPF.Common.Controls;
using BeyondWPF.Common.Windows;

namespace BeyondWPF.Common.Interop;

/// <summary>
/// A set of dangerous methods to modify the appearance.
/// </summary>
internal static class UnsafeReflection
{
    /// <summary>
    /// Casts <see cref="WindowBackdropType"/> to <see cref="Dwmapi.DWMSBT"/>.
    /// </summary>
    public static Dwmapi.DWMSBT Cast(WindowBackdropType backgroundType)
    {
        return backgroundType switch
        {
            WindowBackdropType.Auto => Dwmapi.DWMSBT.DWMSBT_AUTO,
            WindowBackdropType.Mica => Dwmapi.DWMSBT.DWMSBT_MAINWINDOW,
            WindowBackdropType.Acrylic => Dwmapi.DWMSBT.DWMSBT_TRANSIENTWINDOW,
            WindowBackdropType.Tabbed => Dwmapi.DWMSBT.DWMSBT_TABBEDWINDOW,
            _ => Dwmapi.DWMSBT.DWMSBT_DISABLE
        };
    }

    /// <summary>
    /// Casts <see cref="WindowCornerPreference"/> to <see cref="Dwmapi.DWM_WINDOW_CORNER_PREFERENCE"/>.
    /// </summary>
    public static Dwmapi.DWM_WINDOW_CORNER_PREFERENCE Cast(WindowCornerPreference cornerPreference)
    {
        return cornerPreference switch
        {
            WindowCornerPreference.Round => Dwmapi.DWM_WINDOW_CORNER_PREFERENCE.ROUND,
            WindowCornerPreference.RoundSmall => Dwmapi.DWM_WINDOW_CORNER_PREFERENCE.ROUNDSMALL,
            WindowCornerPreference.DoNotRound => Dwmapi.DWM_WINDOW_CORNER_PREFERENCE.DONOTROUND,
            _ => Dwmapi.DWM_WINDOW_CORNER_PREFERENCE.DEFAULT
        };
    }
}
