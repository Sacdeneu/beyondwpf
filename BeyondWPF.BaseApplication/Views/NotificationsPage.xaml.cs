using System.Windows;
using System.Windows.Controls;
using BeyondWPF.Core.Abstractions;

namespace BeyondWPF.BaseApplication.Views
{
    /// <summary>
    /// Interaction logic for NotificationsPage.xaml
    /// </summary>
    public partial class NotificationsPage : UserControl
    {
        private readonly INotificationService _notificationService;

        public NotificationsPage(INotificationService notificationService)
        {
            InitializeComponent();
            _notificationService = notificationService;
        }

        private void SendInfo_Click(object sender, RoutedEventArgs e)
        {
            _notificationService.Show("Information", "Ceci est une notification d'information.", NotificationType.Info);
        }

        private void SendSuccess_Click(object sender, RoutedEventArgs e)
        {
            _notificationService.Show("Succès", "L'opération a été complétée avec succès !", NotificationType.Success);
        }

        private void SendWarning_Click(object sender, RoutedEventArgs e)
        {
            _notificationService.Show("Avertissement", "Attention, quelque chose pourrait ne pas aller.", NotificationType.Warning);
        }

        private void SendError_Click(object sender, RoutedEventArgs e)
        {
            _notificationService.Show("Erreur", "Une erreur critique est survenue.", NotificationType.Error);
        }

        private void SendCustom_Click(object sender, RoutedEventArgs e)
        {
            _notificationService.Show(TitleTextBox.Text, MessageTextBox.Text, NotificationType.Info);
        }
    }
}
