using System;
using System.Linq;
using System.Windows;
using System.Windows.Media;
using BeyondWPF.Core.Abstractions;
using BeyondWPF.Core.Enums;
using BeyondWPF.Common.Interop; // Assuming UnsafeNativeMethods is here

namespace BeyondWPF.Common.Services
{
    public class ThemeService : IThemeService
    {
        private const string RegistryKeyPath = @"HKEY_CURRENT_USER\SOFTWARE\Microsoft\Windows\CurrentVersion\Themes\Personalize";
        private const string RegistryValueSystemUsesLightTheme = "SystemUsesLightTheme";
        private const string RegistryValueAppsUseLightTheme = "AppsUseLightTheme";

        public void ApplyTheme(SystemTheme theme)
        {
            var dictionaries = Application.Current.Resources.MergedDictionaries;
            
            // Remove existing theme dictionaries
            // Strategy: Look for dictionaries with Source containing "Themes/" and "Light.xaml" or "Dark.xaml"
            // Or better: Tag them? For now, simplistic string matching.
            
           // Use a more robust swap logic logic similar to old manager but cleaner
            string themeName = theme switch
            {
                SystemTheme.Dark => "Dark",
                SystemTheme.HighContrast => "HighContrast",
                _ => "Light"
            };

            var uri = new Uri($"pack://application:,,,/BeyondWPF.Controls;component/Themes/{themeName}.xaml");
            
            // Basic swap: Remove old, Add new. 
            // Better: Add new, Remove old to avoid flicker? 
            // Simplified for now.
            
           var existing = dictionaries.FirstOrDefault(d => d.Source?.ToString().Contains("Themes/Light.xaml") == true || d.Source?.ToString().Contains("Themes/Dark.xaml") == true);
           if (existing != null) dictionaries.Remove(existing);
           
           dictionaries.Add(new ResourceDictionary { Source = uri });
           
           ApplySystemAccent();
        }

        public SystemTheme GetCurrentTheme()
        {
            // Simplified Registry check
             var rawAppsUseLightTheme = Microsoft.Win32.Registry.GetValue(RegistryKeyPath, RegistryValueAppsUseLightTheme, 1);
             return (rawAppsUseLightTheme is 0) ? SystemTheme.Dark : SystemTheme.Light;
        }

        public void ApplySystemAccent()
        {
            var systemAccent = SystemParameters.WindowGlassColor;
            if (systemAccent.A == 0) systemAccent = Color.FromRgb(0, 120, 215);

            Application.Current.Resources["SystemAccentColor"] = systemAccent;
            Application.Current.Resources["SystemAccentBrush"] = new SolidColorBrush(systemAccent);
        }
    }
}
