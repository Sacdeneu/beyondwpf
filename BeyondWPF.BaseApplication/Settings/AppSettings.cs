using BeyondWPF.Core.Settings;
using CommunityToolkit.Mvvm.ComponentModel;

namespace BeyondWPF.BaseApplication.Settings
{
    /// <summary>
    /// Application-specific settings.
    /// </summary>
    public class AppSettings : BaseSettings
    {
        private string _theme = "Light";

        /// <summary>
        /// Gets or sets the current application theme.
        /// </summary>
        public string Theme
        {
            get => _theme;
            set => SetProperty(ref _theme, value);
        }

        private bool _isRoundedCorners = true;

        /// <summary>
        /// Gets or sets a value indicating whether the application borders should have rounded corners.
        /// </summary>
        public bool IsRoundedCorners
        {
            get => _isRoundedCorners;
            set => SetProperty(ref _isRoundedCorners, value);
        }

        private bool _useAccentColor = true;

        /// <summary>
        /// Gets or sets a value indicating whether to use the system accent color.
        /// </summary>
        public bool UseAccentColor
        {
            get => _useAccentColor;
            set => SetProperty(ref _useAccentColor, value);
        }

        // Window Persistence
        private bool _saveWindowPosition = true;
        public bool SaveWindowPosition
        {
            get => _saveWindowPosition;
            set => SetProperty(ref _saveWindowPosition, value);
        }

        private double _windowTop = 100;
        public double WindowTop
        {
            get => _windowTop;
            set => SetProperty(ref _windowTop, value);
        }

        private double _windowLeft = 100;
        public double WindowLeft
        {
            get => _windowLeft;
            set => SetProperty(ref _windowLeft, value);
        }

        private double _windowWidth = 900;
        public double WindowWidth
        {
            get => _windowWidth;
            set => SetProperty(ref _windowWidth, value);
        }

        private double _windowHeight = 600;
        public double WindowHeight
        {
            get => _windowHeight;
            set => SetProperty(ref _windowHeight, value);
        }

        private string _windowState = "Normal";
        public string WindowState
        {
            get => _windowState;
            set => SetProperty(ref _windowState, value);
        }
    }
}
