using System.Windows;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using BeyondWPF.BaseApplication.ViewModels;
using BeyondWPF.BaseApplication.Views;
using BeyondWPF.Common.Services;
using BeyondWPF.Core.Abstractions;
using BeyondWPF.Core.Enums;
using BeyondWPF.Core.Settings;
using BeyondWPF.BaseApplication.Settings;

namespace BeyondWPF.BaseApplication
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private readonly IHost _host;

        public App()
        {
            _host = Host.CreateDefaultBuilder()
                .ConfigureServices((context, services) =>
                {
                    // Core Services
                    services.AddSingleton<IThemeService, ThemeService>();
                    services.AddSingleton<IDialogService, DialogService>();
                    
                    // Settings
                    var settings = new AppSettings();
                    settings.LoadSettings<AppSettings>();
                    services.AddSingleton<ISetting>(settings);
                    services.AddSingleton(settings); // Allow direct injection of AppSettings

                    // ViewModels
                    services.AddSingleton<MainViewModel>();

                    // Views
                    services.AddSingleton<MainWindow>();
                    services.AddTransient<ComboBoxPage>();
                    services.AddTransient<ButtonPage>();
                    services.AddTransient<TextBoxPage>();
                    services.AddTransient<CheckBoxPage>();
                    services.AddTransient<RadioButtonPage>();
                    services.AddTransient<ProgressBarPage>();
                    services.AddTransient<SliderPage>();
                    services.AddTransient<TabControlPage>();
                    services.AddTransient<ListBoxPage>();
                    services.AddTransient<DatePickerPage>();
                    services.AddTransient<DialogsPage>();
                    // Register other pages here as we add them
                })
                .Build();
        }

        protected override async void OnStartup(StartupEventArgs e)
        {
            await _host.StartAsync();

            try
            {
                var themeService = _host.Services.GetRequiredService<IThemeService>();
                var settings = _host.Services.GetRequiredService<AppSettings>();
                
                // Initialize Theme
                themeService.ApplySystemAccent(settings.UseAccentColor);
                themeService.ApplyTheme(SystemTheme.Light);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error initializing theme: {ex.Message}");
            }

            var mainWindow = _host.Services.GetRequiredService<MainWindow>();
            mainWindow.Show();

            base.OnStartup(e);
        }

        protected override async void OnExit(ExitEventArgs e)
        {
            using (_host)
            {
                await _host.StopAsync();
            }
            base.OnExit(e);
        }
    }
}
