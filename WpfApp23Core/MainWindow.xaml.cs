using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using NOTIFY = Notifications.Wpf.Core;

namespace WpfApp23Core
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly NOTIFY.NotificationManager notificationManager = new NOTIFY.NotificationManager();

        public MainWindow()
        {
            InitializeComponent();

            // test
        }

        private async void Button_Click(object sender, RoutedEventArgs e)
        {
            var content = new NOTIFY.NotificationContent
            {
                Type = NOTIFY.NotificationType.Information,
                Title = "Hoge Title",
                Message = "Hello World of Hoge",
            };

            await notificationManager.ShowAsync(content, expirationTime: TimeSpan.FromSeconds(2));
        }
    }
}
