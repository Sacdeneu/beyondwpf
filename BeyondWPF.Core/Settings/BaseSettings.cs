using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.IO;
using System.Reflection;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace BeyondWPF.Core.Settings
{
    /// <summary>
    /// Base class for application settings with JSON persistence.
    /// </summary>
    public class BaseSettings : ObservableObject, ISetting
    {
        private string? _appName;

        /// <summary>
        /// Gets or sets the application name.
        /// </summary>
        public string AppName
        {
            get => _appName ?? string.Empty;
            set => SetProperty(ref _appName, value);
        }

        private static BaseSettings? instance;

        /// <summary>
        /// Gets the singleton instance of BaseSettings.
        /// </summary>
        public static BaseSettings Instance => instance ??= LoadSettings();

        /// <summary>
        /// Loads settings from the JSON file.
        /// </summary>
        /// <returns>Loaded or new BaseSettings instance.</returns>
        public static BaseSettings LoadSettings()
        {
            try
            {
                string appDataFolderPath = Path.Combine(
                    Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData),
                    "BeyondWPF", // Changed from "Inversive" to match library context, can be parameterized if needed
                    Assembly.GetEntryAssembly()?.GetName().Name ?? "Unknown");

                string settingsFilePath = Path.Combine(appDataFolderPath, "settings.json");

                if (File.Exists(settingsFilePath))
                {
                    string json = File.ReadAllText(settingsFilePath);
                    var settings = JsonSerializer.Deserialize<BaseSettings>(json);
                    if (settings != null)
                    {
                        settings.SetupSettings(); // Ensure defaults are set if missing
                        return settings;
                    }
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error loading settings: {ex.Message}");
            }

            var newSettings = new BaseSettings();
            newSettings.SetupSettings();
            return newSettings;
        }

        public BaseSettings()
        {
        }

        /// <inheritdoc />
        public virtual void SetupSettings()
        {
            if (string.IsNullOrEmpty(AppName))
            {
                AppName = Assembly.GetEntryAssembly()?.GetName().Name ?? "AppNameNotFound";
            }
        }
    }
}
