using CustomScreensaver;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Timers;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using WpfAnimatedGif;

namespace CustomScreensaver
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public ScreensaverService ScreensaverService { get; }
        public ScreensaverViewModel ScreensaverViewModel { get; }
        public SettingsWindow SettingsWindow { get; }

        public MainWindow(ScreensaverService screensaverService,
                          ScreensaverViewModel screensaverViewModel,
                          SettingsWindow settingsWindow)
        {

            InitializeComponent();
            ScreensaverService = screensaverService;
            ScreensaverViewModel = screensaverViewModel;
            SettingsWindow = settingsWindow;
            settingsWindow.Hide();
            Loaded += (s, e) =>
            {
                ScreensaverViewModel.Settings.Update();
                ScreensaverView.DataContext = "";
                ScreensaverView.DataContext = this;
            };


            ScreensaverService.SetMainWindow(this);
            ScreensaverService.SetImage(ScreensaverView.Back);
            Win32Session win32Session = new Win32Session();
            win32Session.MachineLocked += Win32Session_MachineLocked;
            win32Session.MachineUnlocked += Win32Session_MachineUnlocked; ;
            this.Deactivated += MainWindow_Deactivated;

            Opacity = 0;

            SendInput.LockPC();
        }


        private async void MainWindow_Deactivated(object? sender, EventArgs e)
        {
            //if (ManualStop == false)
            //{
            //    HotKeyService.Stop();
            //    HideWindow();
            //    await Task.Delay(100);
            //    ShowWindow();
            //    HotKeyService.Start();
            //}
        }

        private void Win32Session_MachineUnlocked(object? sender, EventArgs e)
        {
            //if (HotKeyService.Started == false)
            //{
            //    ManualStop = false;
            //    HotKeyService.Start();
            //    ShowWindow();
            //}
        }

        private void Win32Session_MachineLocked(object? sender, EventArgs e)
        {
            //if (HotKeyService.Started == true)
            //{
            //    ManualStop = false;
            //    HotKeyService.Stop();
            //}
        }



        private void Grid_MouseDown(object sender, MouseButtonEventArgs e)
        {
        }

        internal void ShowSettings()
        {
            Dispatcher.Invoke(() =>
            {
                SettingsWindow.Show();
            });
        }
        internal void HideSettings()
        {
            Dispatcher.Invoke(() =>
            {
                SettingsWindow.Hide();
            });
        }
    }
}
