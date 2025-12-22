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
           
           // We need to re-apply accent because theme switch might reset resources or we want to persist the current accent state.
           // However, the interface requires us to pass the state. 
           // In a real app we might inject settings here or store state.
           // For now, let's assume we call ApplySystemAccent explicitly from ViewModel after theme switch if needed, 
           // or we overload ApplyTheme to accept the flag. 
           // BUT, to keep it simple and assuming ViewModel handles it:
           // We DO NOT call ApplySystemAccent here to avoid dependency on Settings.
           // The ViewModel is responsible for re-applying accent if needed.
        }

        public SystemTheme GetCurrentTheme()
        {
            // Simplified Registry check
             var rawAppsUseLightTheme = Microsoft.Win32.Registry.GetValue(RegistryKeyPath, RegistryValueAppsUseLightTheme, 1);
             return (rawAppsUseLightTheme is 0) ? SystemTheme.Dark : SystemTheme.Light;
        }

        public void ApplySystemAccent(bool isEnabled)
        {
            Color accentColor;

            if (isEnabled)
            {
                // Use System Accent or Fallback Blue
                accentColor = SystemParameters.WindowGlassColor;
                if (accentColor.A == 0) accentColor = Color.FromRgb(0, 120, 215); // Default Blue
            }
            else
            {
                // Neutral Gray
                accentColor = Color.FromRgb(153, 153, 153); // #999999
            }

            Application.Current.Resources["SystemAccentColor"] = accentColor;
            Application.Current.Resources["SystemAccentBrush"] = new SolidColorBrush(accentColor);
        }
    }
}
