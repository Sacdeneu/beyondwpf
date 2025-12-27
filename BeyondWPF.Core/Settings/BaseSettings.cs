using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.IO;
using System.Reflection;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace BeyondWPF.Core.Settings
{
    /// <summary>
    /// Base class for application settings with JSON persistence and auto-save capabilities.
    /// </summary>
    public abstract class BaseSettings : ObservableObject, ISetting
    {
        private string? _appName;
        private readonly JsonSerializerOptions _jsonOptions = new() { WriteIndented = true };
        private System.Threading.Timer? _autoSaveTimer;
        private bool _isDirty;

        /// <summary>
        /// Gets or sets the application name used for folder creation.
        /// </summary>
        [JsonIgnore]
        public string AppName
        {
            get => _appName ??= Assembly.GetEntryAssembly()?.GetName().Name ?? "AppNameNotFound";
            set => SetProperty(ref _appName, value);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="BaseSettings"/> class.
        /// </summary>
        public BaseSettings()
        {
            PropertyChanged += BaseSettings_PropertyChanged;
        }

        private void BaseSettings_PropertyChanged(object? sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            // Debounce save
            _isDirty = true;
            _autoSaveTimer?.Dispose();
            _autoSaveTimer = new System.Threading.Timer(AutoSaveCallback, null, 500, System.Threading.Timeout.Infinite);
        }

        private void AutoSaveCallback(object? state)
        {
            if (_isDirty)
            {
                try
                {
                    SaveSettings();
                    _isDirty = false;
                }
                catch (Exception ex)
                {
                    System.Diagnostics.Debug.WriteLine($"AutoSave Failed: {ex.Message}");
                }
            }
        }

        /// <inheritdoc />
        public void LoadSettings<T>() where T : BaseSettings
        {
            try
            {
                string appDataFolderPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "BeyondWPF", AppName);
                string settingsFilePath = Path.Combine(appDataFolderPath, "settings.json");

                if (File.Exists(settingsFilePath))
                {
                    string json = File.ReadAllText(settingsFilePath);
                    var loaded = JsonSerializer.Deserialize<T>(json);

                    if (loaded != null)
                    {
                        var properties = typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance);
                        foreach (var prop in properties)
                        {
                            if (prop.CanWrite && prop.GetCustomAttribute<JsonIgnoreAttribute>() == null)
                            {
                                var value = prop.GetValue(loaded);
                                if (value != null)
                                {
                                    prop.SetValue(this, value);
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error loading settings: {ex.Message}");
            }
        }

        /// <inheritdoc />
        public virtual void SaveSettings()
        {
            try
            {
                string appDataFolderPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "BeyondWPF", AppName);

                if (!Directory.Exists(appDataFolderPath))
                    Directory.CreateDirectory(appDataFolderPath);

                string settingsFilePath = Path.Combine(appDataFolderPath, "settings.json");

                string json = JsonSerializer.Serialize(this, GetType(), _jsonOptions);
                File.WriteAllText(settingsFilePath, json);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error saving settings: {ex.Message}");
            }
        }
    }
}
