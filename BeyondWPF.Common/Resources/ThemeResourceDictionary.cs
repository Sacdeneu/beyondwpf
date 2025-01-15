using BeyondWPF.Common.Appearance;
using System;
using System.Windows;

namespace BeyondWPF.Common.Resources
{
    /// <summary>
    /// A specialized <see cref="ResourceDictionary"/> for managing control-specific resources.
    /// </summary>
    public class ThemeResourceDictionary : ResourceDictionary
    {

        public SystemTheme Theme
        {
            set => SetSourceBasedOnSelectedTheme(value);
        }

        public ThemeResourceDictionary()
        {
            SetSourceBasedOnSelectedTheme(SystemTheme.Light);
        }

        private void SetSourceBasedOnSelectedTheme(SystemTheme? selectedApplicationTheme)
        {
            var themeName = selectedApplicationTheme switch
            {
                SystemTheme.Dark => "Dark",
                SystemTheme.HighContrast => "HighContrast",
                _ => "Light"
            };

            Source = new Uri($"{ApplicationThemeManager.ThemesDictionaryPath}{themeName}.xaml", UriKind.Absolute);
        }
    }
}
