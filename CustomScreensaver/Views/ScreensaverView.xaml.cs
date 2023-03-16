using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace CustomScreensaver.Views
{
    /// <summary>
    /// Логика взаимодействия для ScreensaverView.xaml
    /// </summary>
    public partial class ScreensaverView : UserControl
    {
        public ScreensaverView()
        {
            InitializeComponent();
            Timer timer = new Timer(50);
            timer.Elapsed += TimeUpdate;
            timer.Start();
        }

        private void TimeUpdate(object? sender, ElapsedEventArgs e)
        {
            App.Current.Dispatcher.Invoke(() =>
            {
                Time.Text = DateTime.Now.ToShortTimeString();
                Date.Text = DateTime.Now.ToString("dddd dd.M.yyyy");
            });
        }
    }
}
