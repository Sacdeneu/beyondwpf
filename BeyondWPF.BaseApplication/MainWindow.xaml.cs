
using System.Windows;

namespace BeyondWPF.BaseApplication
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly ViewModels.MainViewModel _viewModel;

        public MainWindow(ViewModels.MainViewModel viewModel)
        {
            InitializeComponent();
            _viewModel = viewModel;
            DataContext = viewModel;
            
            SourceInitialized += MainWindow_SourceInitialized;
            Closing += MainWindow_Closing;
        }

        private void MainWindow_SourceInitialized(object? sender, EventArgs e)
        {
            if (_viewModel.Settings.SaveWindowPosition)
            {
                // Important: must set to Manual, otherwise CenterScreen (from XAML) might override
                WindowStartupLocation = WindowStartupLocation.Manual;

                Top = _viewModel.Settings.WindowTop;
                Left = _viewModel.Settings.WindowLeft;
                Width = _viewModel.Settings.WindowWidth;
                Height = _viewModel.Settings.WindowHeight;

                if (Enum.TryParse<WindowState>(_viewModel.Settings.WindowState, out var state))
                {
                    WindowState = state;
                }
            }
        }

        private void MainWindow_Closing(object? sender, System.ComponentModel.CancelEventArgs e)
        {
            if (_viewModel.Settings.SaveWindowPosition)
            {
                _viewModel.Settings.WindowState = WindowState.ToString();
                // Save restore bounds if maximized
                if (WindowState == WindowState.Normal)
                {
                    _viewModel.Settings.WindowTop = Top;
                    _viewModel.Settings.WindowLeft = Left;
                    _viewModel.Settings.WindowWidth = Width;
                    _viewModel.Settings.WindowHeight = Height;
                }
                else
                {
                    _viewModel.Settings.WindowTop = RestoreBounds.Top;
                    _viewModel.Settings.WindowLeft = RestoreBounds.Left;
                    _viewModel.Settings.WindowWidth = RestoreBounds.Width;
                    _viewModel.Settings.WindowHeight = RestoreBounds.Height;
                }
                
                // Force immediate save because the auto-save timer (500ms) won't fire before app exit
                _viewModel.Settings.SaveSettings();
            }
        }

        private void Close_Click(object sender, RoutedEventArgs e) => Close();

        private void Maximize_Click(object sender, RoutedEventArgs e)
        {
            WindowState = WindowState == WindowState.Maximized ? WindowState.Normal : WindowState.Maximized;
        }

        private void Minimize_Click(object sender, RoutedEventArgs e) => WindowState = WindowState.Minimized;
    }
}