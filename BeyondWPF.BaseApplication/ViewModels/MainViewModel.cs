using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using BeyondWPF.Core.Abstractions;
using BeyondWPF.Core.Enums;
using System.Collections.ObjectModel;
using System.Windows.Controls;
using BeyondWPF.BaseApplication.Views;
using BeyondWPF.BaseApplication.Settings;
using Microsoft.Extensions.DependencyInjection; // For resolving pages

namespace BeyondWPF.BaseApplication.ViewModels
{
    public partial class MainViewModel : ObservableObject
    {
        private readonly IThemeService _themeService;
        private readonly IServiceProvider _serviceProvider;
        private readonly AppSettings _settings;

        [ObservableProperty]
        private string _title = "BeyondWPF Showcase";

        [ObservableProperty]
        private object _currentView;

        [ObservableProperty]
        private bool _isDarkTheme;

        [ObservableProperty]
        private System.Windows.CornerRadius _borderCornerRadius;

        public AppSettings Settings => _settings;

        public MainViewModel(IThemeService themeService, IServiceProvider serviceProvider, AppSettings settings)
        {
            _themeService = themeService;
            _serviceProvider = serviceProvider;
            _settings = settings;
            
            // Restore theme from settings
            if (Enum.TryParse<SystemTheme>(_settings.Theme, out var savedTheme))
            {
                _themeService.ApplyTheme(savedTheme);
                _isDarkTheme = savedTheme == SystemTheme.Dark;
            }
            else
            {
                _isDarkTheme = _themeService.GetCurrentTheme() == SystemTheme.Dark;
            }

            // Restore border settings
            UpdateCornerRadius();
            
            // Restore accent color settings
            _themeService.ApplySystemAccent(_settings.UseAccentColor);
            
            // Default View
            NavigateToComboBox();
        }

        private void UpdateCornerRadius()
        {
            BorderCornerRadius = _settings.IsRoundedCorners ? new System.Windows.CornerRadius(10) : new System.Windows.CornerRadius(0);
        }

        [RelayCommand]
        public void ToggleBorder()
        {
            _settings.IsRoundedCorners = !_settings.IsRoundedCorners;
            UpdateCornerRadius();
        }

        [RelayCommand]
        public void ToggleAccentColor()
        {
             // Setting is bound TwoWay to CheckBox, so it's already updated.
             // But we need to apply it.
             // Actually, if we bind Command to CheckBox command, it fires after update or before?
             // Safer to just re-read or pass the value.
             // If we use EventToCommand or just execute command on click.
             // Let's assume the Setter in Settings triggers something? No, Settings property changes don't trigger service calls automatically to keep separation.
             // We will handle it in the command.
             
             // If bound to CheckBox, the property update happens first.
             _themeService.ApplySystemAccent(_settings.UseAccentColor);
        }

        [RelayCommand]
        public void ToggleTheme()
        {
            if (IsDarkTheme)
            {
                _themeService.ApplyTheme(SystemTheme.Light);
                _settings.Theme = SystemTheme.Light.ToString();
                IsDarkTheme = false;
            }
            else
            {
                _themeService.ApplyTheme(SystemTheme.Dark);
                _settings.Theme = SystemTheme.Dark.ToString();
                IsDarkTheme = true;
            }
            
            // Re-apply accent after theme switch incase theme resources overrode it
            _themeService.ApplySystemAccent(_settings.UseAccentColor);
        }

        [RelayCommand]
        public void NavigateToComboBox()
        {
             CurrentView = _serviceProvider.GetRequiredService<ComboBoxPage>();
        }

        [RelayCommand]
        public void NavigateToButton()
        {
             CurrentView = _serviceProvider.GetRequiredService<ButtonPage>();
        }

        [RelayCommand]
        public void NavigateToTextBox()
        {
             CurrentView = _serviceProvider.GetRequiredService<TextBoxPage>();
        }

        [RelayCommand]
        public void NavigateToCheckBox()
        {
             CurrentView = _serviceProvider.GetRequiredService<CheckBoxPage>();
        }

        [RelayCommand]
        public void NavigateToRadioButton()
        {
             CurrentView = _serviceProvider.GetRequiredService<RadioButtonPage>();
        }

        [RelayCommand]
        public void NavigateToProgressBar()
        {
             CurrentView = _serviceProvider.GetRequiredService<ProgressBarPage>();
        }

        [RelayCommand]
        public void NavigateToSlider()
        {
             CurrentView = _serviceProvider.GetRequiredService<SliderPage>();
        }

        [RelayCommand]
        public void NavigateToTabControl()
        {
             CurrentView = _serviceProvider.GetRequiredService<TabControlPage>();
        }

        [RelayCommand]
        public void NavigateToListBox()
        {
             CurrentView = _serviceProvider.GetRequiredService<ListBoxPage>();
        }

        [RelayCommand]
        public void NavigateToDatePicker()
        {
             CurrentView = _serviceProvider.GetRequiredService<DatePickerPage>();
        }
    }
}
