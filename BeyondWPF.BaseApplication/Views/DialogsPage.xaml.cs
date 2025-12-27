using System.Windows;
using System.Windows.Controls;
using BeyondWPF.Core.Abstractions;

namespace BeyondWPF.BaseApplication.Views
{
    public partial class DialogsPage : UserControl
    {
        private readonly IDialogService _dialogService;

        public DialogsPage(IDialogService dialogService)
        {
            InitializeComponent();
            _dialogService = dialogService;
        }

        private void ShowDialog_Click(object sender, RoutedEventArgs e)
        {
            var content = new StackPanel
            {
                Width = 300,
                Background = (System.Windows.Media.Brush)Application.Current.Resources["ControlLightBrush"],
            };
            
            var border = new Border
            {
                Background = (System.Windows.Media.Brush)Application.Current.Resources["ControlLightBrush"],
                CornerRadius = new CornerRadius(8),
                Padding = new Thickness(20),
                Width = 350
            };

            var stack = new StackPanel();
            border.Child = stack;

            stack.Children.Add(new TextBlock 
            { 
                Text = "Modal Dialog", 
                FontSize = 18, 
                FontWeight = FontWeights.Bold, 
                Foreground = (System.Windows.Media.Brush)Application.Current.Resources["TextBrush"],
                Margin = new Thickness(0,0,0,10)
            });

            stack.Children.Add(new TextBlock 
            { 
                Text = "This is a modal dialog overlay. Background clicks are blocked.", 
                TextWrapping = TextWrapping.Wrap,
                Foreground = (System.Windows.Media.Brush)Application.Current.Resources["TextBrush"],
                Margin = new Thickness(0,0,0,20)
            });

            var closeBtn = new Button { Content = "Close" };
            closeBtn.Click += (s, args) => _dialogService.Close();
            
            stack.Children.Add(closeBtn);

            _dialogService.Show(border);
        }
    }
}
