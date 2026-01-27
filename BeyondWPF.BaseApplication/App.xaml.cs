using System.Windows;
using Microsoft.Toolkit.Uwp.Notifications;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using BeyondWPF.BaseApplication.ViewModels;
using BeyondWPF.BaseApplication.Views;
using BeyondWPF.Common.Services;
using BeyondWPF.Core.Abstractions;
using BeyondWPF.Core.Enums;
using BeyondWPF.Core.Settings;
using BeyondWPF.BaseApplication.Settings;
using BeyondWPF.Common.ViewModels;
using BeyondWPF.Common.Logging;
using Microsoft.Extensions.Logging;
using System;

namespace BeyondWPF.BaseApplication
{
    public partial class App : Application
    {
        private readonly IHost _host;

        public App()
        {
            _host = Host.CreateDefaultBuilder()
                .ConfigureLogging(logging =>
                {
                    logging.ClearProviders();
                    logging.AddProvider(new WpfLoggerProvider(LogService.Instance));
                })
                .ConfigureServices((context, services) =>
                {
                    // Core Services
                    services.AddSingleton<IThemeService, ThemeService>();
                    services.AddSingleton<IDialogService, DialogService>();
                    services.AddSingleton<INotificationService, NativeNotificationService>();
                    services.AddSingleton(LoadingService.Instance);
                    services.AddSingleton(LogService.Instance);
                    
                    // Settings
                    var settings = new AppSettings();
                    settings.LoadSettings<AppSettings>();
                    services.AddSingleton<ISetting>(settings);
                    services.AddSingleton(settings);

                    // ViewModels
                    services.AddSingleton<MainViewModel>();
                    services.AddSingleton<LogConsoleViewModel>();
                    services.AddTransient<AsyncExampleViewModel>();
                    services.AddTransient<FormExampleViewModel>();
                    services.AddSingleton<ViewModelsPage>(s => new ViewModelsPage(
                        s.GetRequiredService<AsyncExampleViewModel>(),
                        s.GetRequiredService<FormExampleViewModel>()));
                    
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
                    services.AddTransient<ListViewPage>();
                    services.AddTransient<DatePickerPage>();
                    services.AddTransient<DialogsPage>();
                    services.AddTransient<ContextMenuPage>();
                    services.AddTransient<NotificationsPage>();

                    LogService.Instance.Debug("Services registered successfully.");
                })
                .Build();
        }

        protected override async void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
            
            LogService.Instance.Info("BeyondWPF Application starting up...");

            // Listen to notification activation
            ToastNotificationManagerCompat.OnActivated += toastArgs =>
            {
                Application.Current.Dispatcher.Invoke(() =>
                {
                    if (Application.Current.MainWindow != null)
                    {
                        if (Application.Current.MainWindow.WindowState == WindowState.Minimized)
                            Application.Current.MainWindow.WindowState = WindowState.Normal;
                        
                        Application.Current.MainWindow.Activate();
                    }
                });
            };

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
