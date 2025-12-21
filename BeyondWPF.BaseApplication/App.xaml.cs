using System.Windows;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using BeyondWPF.BaseApplication.ViewModels;
using BeyondWPF.BaseApplication.Views;
using BeyondWPF.Common.Services;
using BeyondWPF.Core.Abstractions;
using BeyondWPF.Core.Enums;
using BeyondWPF.Core.Settings;

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
                    services.AddSingleton<ISetting>(BaseSettings.Instance);

                    // ViewModels
                    services.AddSingleton<MainViewModel>();

                    // Views
                    services.AddSingleton<MainWindow>();
                    services.AddTransient<ComboBoxPage>();
                })
                .Build();
        }

        protected override async void OnStartup(StartupEventArgs e)
        {
            await _host.StartAsync();

            try
            {
                var themeService = _host.Services.GetRequiredService<IThemeService>();
                // Initialize Theme
                themeService.ApplySystemAccent();
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
