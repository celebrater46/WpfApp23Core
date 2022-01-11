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
using System.Text.RegularExpressions;
using System.Windows.Threading;

namespace WpfApp23Core
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly NOTIFY.NotificationManager notificationManager = new NOTIFY.NotificationManager();
        private DispatcherTimer timer;
        private static readonly int notifyCount = 8;
        
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

        private void DisplayTime_OnGotFocus(object sender, RoutedEventArgs e)
        {
            (sender as TextBox).SelectAll();
        }

        private void StartButton_OnClick(object sender, RoutedEventArgs e)
        {
            if (Regex.IsMatch(displayTime.Text, "^[0-9]{4}$") == false)
            {
                MessageBox.Show("Type a 4-digit number correctly. Ex: 1945", 
                    "Error", 
                    MessageBoxButton.OK, 
                    MessageBoxImage.Error);
                return;
            }

            if (timer == null)
            {
                timer = new DispatcherTimer(DispatcherPriority.SystemIdle);
                timer.Interval = TimeSpan.FromMilliseconds(200);
                timer.Tick += new EventHandler(OnTimer);
            }
            
            SetControlStatus(isRunning: true);
            timer.Start();
        }

        private void OnTimer(object? sender, EventArgs e)
        {
            if (DateTime.Now.ToString("HHmm") == displayTime.Text)
            {
                timer?.Stop();
                SetControlStatus(isRunning: false);

                string title = "Notification";
                string msg = "The time has come.";

                var contentMoment = new NOTIFY.NotificationContent
                {
                    Type = NOTIFY.NotificationType.Success,
                    Title = title,
                    Message = msg,
                };

                for (int i = 0; i < notifyCount - 1; i++)
                {
                    notificationManager.ShowAsync(contentMoment, expirationTime: TimeSpan.FromSeconds(2));
                }

                var contentStay = new NOTIFY.NotificationContent
                {
                    Type = NOTIFY.NotificationType.Information,
                    Title = title,
                    Message = msg,
                };

                notificationManager.ShowAsync(contentStay, expirationTime: TimeSpan.FromSeconds(5));
            }
        }

        private void SetControlStatus(bool isRunning)
        {
            displayTime.IsEnabled = !isRunning;
            startButton.IsEnabled = !isRunning;
            cancelButton.IsEnabled = isRunning;
        }

        private void CancelButton_OnClick(object sender, RoutedEventArgs e)
        {
            SetControlStatus(isRunning: false);
            timer?.Stop();
        }

        private void MainWindow_OnLoaded(object sender, RoutedEventArgs e)
        {
            // displayTime.Text = DateTime.Now.AddMinutes(1).ToString("HHmm");
            displayTime.Text = DateTime.Now.AddMinutes(10).ToString("HHmm");
            displayTime.SelectAll();
        }
    }
}
