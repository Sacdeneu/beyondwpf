using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using BeyondWPF.Core.Abstractions;
using BeyondWPF.Core.Enums;
using BeyondWPF.BaseApplication.Views;
using BeyondWPF.BaseApplication.Settings;
using BeyondWPF.Common.Services;
using Microsoft.Extensions.DependencyInjection; // For resolving pages

namespace BeyondWPF.BaseApplication.ViewModels
{
    public partial class MainViewModel : ObservableObject
    {
        private readonly IThemeService _themeService;
        private readonly IDialogService _dialogService;
        private readonly IServiceProvider _serviceProvider;
        private readonly AppSettings _settings;

        [ObservableProperty]
        private string _title = "BeyondWPF Showcase";

        [ObservableProperty]
        private object _currentView;

        [ObservableProperty]
        private LogConsoleViewModel _logConsole;

        [ObservableProperty]
        private bool _isDarkTheme;

        [ObservableProperty]
        private System.Windows.CornerRadius _borderCornerRadius;

        public AppSettings Settings => _settings;
        public object? DialogContent => _dialogService.CurrentView;
        public bool IsDialogOpen => _dialogService.IsOpen;

        public MainViewModel(IThemeService themeService, IDialogService dialogService, IServiceProvider serviceProvider, AppSettings settings)
        {
            _themeService = themeService;
            _dialogService = dialogService;
            _serviceProvider = serviceProvider;
            _settings = settings;
            _logConsole = _serviceProvider.GetRequiredService<LogConsoleViewModel>();

            LogService.Instance.Info("MainViewModel initialized.");
            LogService.Instance.Error("Log Console test: Error captured.");
            LogService.Instance.Warning("Log Console test: Warning captured.");
            LogService.Instance.Debug("Log Console test: Debug message.");


            _dialogService.PropertyChanged += (s, e) =>
            {
                if (e.PropertyName == nameof(IDialogService.CurrentView))
                {
                    OnPropertyChanged(nameof(DialogContent));
                }
                else if (e.PropertyName == nameof(IDialogService.IsOpen))
                {
                    OnPropertyChanged(nameof(IsDialogOpen));
                }
            };
            
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
             Navigate(_serviceProvider.GetRequiredService<ComboBoxPage>());
        }

        [RelayCommand]
        public void NavigateToButton()
        {
             Navigate(_serviceProvider.GetRequiredService<ButtonPage>());
        }

        [RelayCommand]
        public void NavigateToTextBox()
        {
             Navigate(_serviceProvider.GetRequiredService<TextBoxPage>());
        }

        [RelayCommand]
        public void NavigateToCheckBox()
        {
             Navigate(_serviceProvider.GetRequiredService<CheckBoxPage>());
        }

        [RelayCommand]
        public void NavigateToRadioButton()
        {
             Navigate(_serviceProvider.GetRequiredService<RadioButtonPage>());
        }

        [RelayCommand]
        public void NavigateToProgressBar()
        {
             Navigate(_serviceProvider.GetRequiredService<ProgressBarPage>());
        }

        [RelayCommand]
        public void NavigateToSlider()
        {
             Navigate(_serviceProvider.GetRequiredService<SliderPage>());
        }

        [RelayCommand]
        public void NavigateToTabControl()
        {
             Navigate(_serviceProvider.GetRequiredService<TabControlPage>());
        }

        [RelayCommand]
        public void NavigateToListBox()
        {
             Navigate(_serviceProvider.GetRequiredService<ListBoxPage>());
        }

        [RelayCommand]
        private void Navigate(object view)
        {
            CurrentView = view;
            LogService.Instance.Info($"Navigated to {view.GetType().Name}");
        }

        [RelayCommand]
        public void NavigateToListView()
        {
             Navigate(_serviceProvider.GetRequiredService<ListViewPage>());
        }

        [RelayCommand]
        public void NavigateToDatePicker()
        {
             Navigate(_serviceProvider.GetRequiredService<DatePickerPage>());
        }

        [RelayCommand]
        public void NavigateToDialogs()
        {
             CurrentView = _serviceProvider.GetRequiredService<DialogsPage>();
        }

        [RelayCommand]
        public void NavigateToContextMenu()
        {
             CurrentView = _serviceProvider.GetRequiredService<ContextMenuPage>();
        }

        [RelayCommand]
        public void NavigateToNotifications()
        {
             CurrentView = _serviceProvider.GetRequiredService<NotificationsPage>();
        }

        [RelayCommand]
        public void NavigateToViewModels()
        {
             CurrentView = _serviceProvider.GetRequiredService<ViewModelsPage>();
        }
    }
}
