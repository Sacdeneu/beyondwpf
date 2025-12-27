namespace BeyondWPF.Core.Settings
{
    /// <summary>
    /// Interface for application settings.
    /// </summary>
    /// <summary>
    /// Interface for application settings management.
    /// </summary>
    public interface ISetting
    {
        /// <summary>
        /// Saves the current settings to the persistent store.
        /// </summary>
        void SaveSettings();

        /// <summary>
        /// Loads settings into the provided type.
        /// </summary>
        /// <typeparam name="T">The type of the settings class, must inherit from BaseSettings.</typeparam>
        void LoadSettings<T>() where T : BaseSettings;
    }
}
