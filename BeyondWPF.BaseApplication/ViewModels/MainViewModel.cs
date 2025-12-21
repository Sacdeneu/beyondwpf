using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using BeyondWPF.Core.Abstractions;
using BeyondWPF.Core.Enums;
using System.Collections.ObjectModel;
using System.Windows.Controls;
using BeyondWPF.BaseApplication.Views;
using Microsoft.Extensions.DependencyInjection; // For resolving pages

namespace BeyondWPF.BaseApplication.ViewModels
{
    public partial class MainViewModel : ObservableObject
    {
        private readonly IThemeService _themeService;
        private readonly IServiceProvider _serviceProvider;

        [ObservableProperty]
        private string _title = "BeyondWPF Showcase";

        [ObservableProperty]
        private object _currentView;

        [ObservableProperty]
        private bool _isDarkTheme;

        public MainViewModel(IThemeService themeService, IServiceProvider serviceProvider)
        {
            _themeService = themeService;
            _serviceProvider = serviceProvider;
            _isDarkTheme = _themeService.GetCurrentTheme() == SystemTheme.Dark;
            
            // Default View
            NavigateToComboBox();
        }

        [RelayCommand]
        public void ToggleTheme()
        {
            if (IsDarkTheme)
            {
                _themeService.ApplyTheme(SystemTheme.Light);
                IsDarkTheme = false;
            }
            else
            {
                _themeService.ApplyTheme(SystemTheme.Dark);
                IsDarkTheme = true;
            }
        }

        [RelayCommand]
        public void NavigateToComboBox()
        {
             CurrentView = _serviceProvider.GetRequiredService<ComboBoxPage>();
        }
    }
}
