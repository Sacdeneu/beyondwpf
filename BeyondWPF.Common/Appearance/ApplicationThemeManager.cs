
using BeyondWPF.Common.Windows;
using System.Windows;

namespace BeyondWPF.Common.Appearance
{

    public static class ApplicationThemeManager
    {
        private static SystemTheme _cachedApplicationTheme = SystemTheme.Unknown;

        internal const string LibraryNamespace = "BeyondWPF.Common;";

        internal const string ThemesDictionaryPath = "pack://application:,,,/BeyondWPF.Common;component/Resources/Themes/";

        /// <summary>
        /// Event triggered when the application's theme is changed.
        /// </summary>
        public static event ThemeChangedEvent? Changed;

        /// <summary>
        /// Gets a value that indicates whether the application is currently using the high contrast theme.
        /// </summary>
        /// <returns><see langword="true"/> if application uses high contrast theme.</returns>
        public static bool IsHighContrast() => _cachedApplicationTheme == SystemTheme.HighContrast;

        /// <summary>
        /// Gets a value that indicates whether the Windows is currently using the high contrast theme.
        /// </summary>
        /// <returns><see langword="true"/> if system uses high contrast theme.</returns>
        public static bool IsSystemHighContrast() => SystemThemeManager.HighContrast;

        /// <summary>
        /// Changes the current application theme.
        /// </summary>
        /// <param name="applicationTheme">Theme to set.</param>
        /// <param name="backgroundEffect">Whether the custom background effect should be applied.</param>
        /// <param name="updateAccent">Whether the color accents should be changed.</param>
        public static void Apply(
            SystemTheme applicationTheme,
            WindowBackdropType backgroundEffect = WindowBackdropType.Mica,
            bool updateAccent = true
        )
        {
            if (updateAccent)
            {
                ApplicationAccentColorManager.Apply(
                    ApplicationAccentColorManager.GetColorizationColor(),
                    applicationTheme,
                    false
                );
            }

            if (applicationTheme == SystemTheme.Unknown)
            {
                return;
            }

            ResourceDictionaryManager appDictionaries = new(LibraryNamespace);

            string themeDictionaryName = "Light";

            switch (applicationTheme)
            {
                case SystemTheme.Dark:
                    themeDictionaryName = "Dark";
                    break;
                case SystemTheme.HighContrast:
                    themeDictionaryName = ApplicationThemeManager.GetSystemTheme() switch
                    {
                        SystemTheme.HighContrast => "HighContrast",
                        _ => "HighContrast",
                    };
                    break;
            }

            bool isUpdated = appDictionaries.UpdateDictionary(
                "theme",
                new Uri(ThemesDictionaryPath + themeDictionaryName + ".xaml", UriKind.Absolute)
            );

            System.Diagnostics.Debug.WriteLine(
                $"INFO | {typeof(ApplicationThemeManager)} tries to update theme to {themeDictionaryName} ({applicationTheme}): {isUpdated}",
                nameof(ApplicationThemeManager)
            );

            if (!isUpdated)
            {
                return;
            }

            SystemThemeManager.UpdateSystemThemeCache();

            _cachedApplicationTheme = applicationTheme;

            Changed?.Invoke(applicationTheme, ApplicationAccentColorManager.SystemAccent);

            if (Application.Current.MainWindow is Window mainWindow)
            {
                //WindowBackgroundManager.UpdateBackground(mainWindow, applicationTheme, backgroundEffect);
            }
        }

        /// <summary>
        /// Applies Resources in the <paramref name="frameworkElement"/>.
        /// </summary>
        public static void Apply(FrameworkElement frameworkElement)
        {
            if (frameworkElement is null)
            {
                return;
            }

            ResourceDictionary[] resourcesRemove = frameworkElement
                .Resources.MergedDictionaries.Where(e => e.Source is not null)
                .Where(e => e.Source.ToString().ToLower().Contains(LibraryNamespace))
                .ToArray();

            foreach (ResourceDictionary? resource in Application.Current.Resources.MergedDictionaries)
            {
                System.Diagnostics.Debug.WriteLine(
                    $"INFO | {typeof(ApplicationThemeManager)} Add {resource.Source}",
                    "Wpf.Ui.Appearance"
                );
                frameworkElement.Resources.MergedDictionaries.Add(resource);
            }

            foreach (ResourceDictionary resource in resourcesRemove)
            {
                System.Diagnostics.Debug.WriteLine(
                    $"INFO | {typeof(ApplicationThemeManager)} Remove {resource.Source}",
                    "Wpf.Ui.Appearance"
                );

                _ = frameworkElement.Resources.MergedDictionaries.Remove(resource);
            }

            foreach (System.Collections.DictionaryEntry resource in Application.Current.Resources)
            {
                System.Diagnostics.Debug.WriteLine(
                    $"INFO | {typeof(ApplicationThemeManager)} Copy Resource {resource.Key} - {resource.Value}",
                    "Wpf.Ui.Appearance"
                );
                frameworkElement.Resources[resource.Key] = resource.Value;
            }
        }

        public static void ApplySystemTheme()
        {
            ApplySystemTheme(true);
        }

        public static void ApplySystemTheme(bool updateAccent)
        {
            SystemThemeManager.UpdateSystemThemeCache();

            SystemTheme systemTheme = GetSystemTheme();

            SystemTheme themeToSet = SystemTheme.Light;

            if (systemTheme is SystemTheme.Dark)
            {
                themeToSet = SystemTheme.Dark;
            }
            else if (
                systemTheme is SystemTheme.HighContrast
            )
            {
                themeToSet = SystemTheme.HighContrast;
            }

            Apply(themeToSet, updateAccent: updateAccent);
        }

        /// <summary>
        /// Gets currently set application theme.
        /// </summary>
        /// <returns><see cref="ApplicationTheme.Unknown"/> if something goes wrong.</returns>
        public static SystemTheme GetAppTheme()
        {
            if (_cachedApplicationTheme == SystemTheme.Unknown)
            {
                FetchApplicationTheme();
            }

            return _cachedApplicationTheme;
        }

        /// <summary>
        /// Gets currently set system theme.
        /// </summary>
        /// <returns><see cref="SystemTheme.Unknown"/> if something goes wrong.</returns>
        public static SystemTheme GetSystemTheme()
        {
            return SystemThemeManager.GetCachedSystemTheme();
        }

        /// <summary>
        /// Gets a value that indicates whether the application is matching the system theme.
        /// </summary>
        /// <returns><see langword="true"/> if the application has the same theme as the system.</returns>
        public static bool IsAppMatchesSystem()
        {
            SystemTheme appApplicationTheme = GetAppTheme();
            SystemTheme sysTheme = GetSystemTheme();

            return appApplicationTheme switch
            {
                SystemTheme.Dark
                    => sysTheme is SystemTheme.Dark,
                SystemTheme.Light
                    => sysTheme is SystemTheme.Light,
                _ => appApplicationTheme == SystemTheme.HighContrast && SystemThemeManager.HighContrast
            };
        }

        /// <summary>
        /// Checks if the application and the operating system are currently working in a dark theme.
        /// </summary>
        public static bool IsMatchedDark()
        {
            SystemTheme appApplicationTheme = GetAppTheme();
            SystemTheme sysTheme = GetSystemTheme();

            if (appApplicationTheme != SystemTheme.Dark)
            {
                return false;
            }

            return sysTheme is SystemTheme.Dark;
        }

        /// <summary>
        /// Checks if the application and the operating system are currently working in a light theme.
        /// </summary>
        public static bool IsMatchedLight()
        {
            SystemTheme appApplicationTheme = GetAppTheme();
            SystemTheme sysTheme = GetSystemTheme();

            if (appApplicationTheme != SystemTheme.Light)
            {
                return false;
            }

            return sysTheme is SystemTheme.Light;
        }

        /// <summary>
        /// Tries to guess the currently set application theme.
        /// </summary>
        private static void FetchApplicationTheme()
        {
            ResourceDictionaryManager appDictionaries = new(LibraryNamespace);
            ResourceDictionary? themeDictionary = appDictionaries.GetDictionary("theme");

            if (themeDictionary == null)
            {
                return;
            }

            string themeUri = themeDictionary.Source.ToString().Trim().ToLower();

            if (themeUri.Contains("light"))
            {
                _cachedApplicationTheme = SystemTheme.Light;
            }

            if (themeUri.Contains("dark"))
            {
                _cachedApplicationTheme = SystemTheme.Dark;
            }

            if (themeUri.Contains("highcontrast"))
            {
                _cachedApplicationTheme = SystemTheme.HighContrast;
            }
        }
    }
}
