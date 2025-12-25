using System.Windows.Controls;
using System.Collections.ObjectModel;

namespace BeyondWPF.BaseApplication.Views
{
    public partial class ListViewPage : UserControl
    {
        public ObservableCollection<User> Users { get; set; }

        public ListViewPage()
        {
            InitializeComponent();
            Users = new ObservableCollection<User>
            {
                new User { Name = "Alice Johnson", Role = "Admin", Status = "Active", Details = "System Administrator" },
                new User { Name = "Bob Smith", Role = "User", Status = "Offline", Details = "Standard User Account" },
                new User { Name = "Charlie Brown", Role = "Manager", Status = "Active", Details = "Project Manager" },
                new User { Name = "Diana Prince", Role = "User", Status = "Active", Details = "Graphic Designer" },
                new User { Name = "Evan Wright", Role = "Support", Status = "Busy", Details = "Technical Support Specialist" }
            };
            DemoListView.ItemsSource = Users;
        }
    }

    public class User
    {
        public string Name { get; set; } = string.Empty;
        public string Role { get; set; } = string.Empty;
        public string Status { get; set; } = string.Empty;
        public string Details { get; set; } = string.Empty;
    }
}
