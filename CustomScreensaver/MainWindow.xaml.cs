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
        internal HotKeyService HotKeyService { get; }
        public bool ManualStop { get; private set; }

        private List<Key> showCombination = new List<Key>() {
                Key.RightCtrl,
                Key.NumPad7,
            };

        private List<Key> hideCombination = new List<Key>() {
                Key.RightCtrl,
                Key.NumPad8,
            };

        private List<Key> changeBackCombination = new List<Key>() {
                Key.Escape
            };

        private List<Key> breakwindowswitch = new List<Key>() {
                Key.LeftAlt,
                Key.Tab
            };

        private List<Key> showTimeCombination = new List<Key>() {
                Key.F
            };

        public MainWindow()
        {
            InitializeComponent();

            HotKeyService = new HotKeyService();

            HotKeyService.SetCombination(showCombination, () =>
            {
                ManualStop = false;
                ShowWindow();
            });

            HotKeyService.SetCombination(hideCombination, () =>
            {
                ManualStop = true;
                HideWindow();
            });

            HotKeyService.SetCombination(changeBackCombination, ChangeBack);

            HotKeyService.SetCombination(breakwindowswitch, BreakSwitch);
            HotKeyService.SetCombination(showTimeCombination, ShowTime);

            HotKeyService.Start();

            Win32Session win32Session = new Win32Session();
            win32Session.MachineLocked += Win32Session_MachineLocked;
            win32Session.MachineUnlocked += Win32Session_MachineUnlocked; ;


            Opacity = 0;

            Mouse.OverrideCursor = Cursors.None;

            IsHitTestVisible = false;
            Timer timer = new Timer(50);
            timer.Elapsed += Timer_Elapsed;
            timer.Start();

            this.Deactivated += MainWindow_Deactivated;
            this.Unloaded += MainWindow_Unloaded;
            this.LostFocus += MainWindow_LostFocus;
            this.LostKeyboardFocus += MainWindow_LostKeyboardFocus;

            SendInput.LockPC();
        }

        public async void MoveMouseRandomly()
        {
            while (ManualStop == false)
            {
                SendInput.MoveMouseRandomly();
                await Task.Delay(1000);
            }
        }

        private void ShowTime()
        {
            Dispatcher.Invoke(() =>
            {
                if (ManualStop == false)
                    TextStack.Visibility = TextStack.Visibility == Visibility.Collapsed ? Visibility.Visible : Visibility.Collapsed;
            });
        }

        private void BreakSwitch()
        {
            if (ManualStop == false)
            {
                Debug.WriteLine("BreakSwitch");
                if (HotKeyService.Started == true)
                {
                    SendInput.StopSwitchWindows();
                }
            }
        }

        private void MainWindow_LostKeyboardFocus(object sender, KeyboardFocusChangedEventArgs e)
        {
            Debug.WriteLine("MainWindow_LostKeyboardFocus");
        }

        private void MainWindow_LostFocus(object sender, RoutedEventArgs e)
        {
            Debug.WriteLine("MainWindow_LostKeyboardFocus");
        }

        private void MainWindow_Unloaded(object sender, RoutedEventArgs e)
        {
            Debug.WriteLine("MainWindow_Unloaded");
        }

        private async void MainWindow_Deactivated(object? sender, EventArgs e)
        {
            if (ManualStop == false)
            {
                HotKeyService.Stop();
                Debug.WriteLine("MainWindow_Deactivated");
                HideWindow();
                await Task.Delay(100);
                ShowWindow();
                HotKeyService.Start();
            }
        }

        private void Win32Session_MachineUnlocked(object? sender, EventArgs e)
        {
            if (HotKeyService.Started == false)
            {
                ManualStop = false;
                App.SendMessage("MachineUnlocked");
                HotKeyService.Start();
                ShowWindow();
            }
        }

        private void Win32Session_MachineLocked(object? sender, EventArgs e)
        {
            if (HotKeyService.Started == true)
            {
                ManualStop = false;
                App.SendMessage("MachineLocked");
                HotKeyService.Stop();
            }
        }

        private void Timer_Elapsed(object? sender, ElapsedEventArgs e)
        {
            Dispatcher.Invoke(() =>
            {
                Time.Text = DateTime.Now.ToShortTimeString();
                var mp = SendInput.GetMousePosition();
                var height = SystemParameters.FullPrimaryScreenHeight / ushort.MaxValue;
                var width = ushort.MaxValue / SystemParameters.FullPrimaryScreenWidth;
                Debug.WriteLine($"{ushort.MaxValue}");
                Debug.WriteLine($"{SystemParameters.PrimaryScreenWidth}-{SystemParameters.PrimaryScreenHeight}");
                Debug.WriteLine($"{width}-{height}");
            });
        }

        Random Random = new Random();
        private void ChangeBack()
        {
            Dispatcher.Invoke(() =>
            {
                var files = Directory.GetFiles("Backgrounds", "*");
                List<string> imageFiles = new List<string>();
                foreach (string filename in files)
                {
                    if (Regex.IsMatch(filename, @"\.jpg$|\.png$|\.gif$"))
                        imageFiles.Add(filename);
                }

                Back.Visibility = Random.Next(10) == 5 ? Visibility.Collapsed : Visibility.Visible;

                var path = System.IO.Path.Combine(AppContext.BaseDirectory, imageFiles[Random.Next(imageFiles.Count)]);

                if (Regex.IsMatch(path, @"\.gif$"))
                {
                    var image = new BitmapImage();
                    image.BeginInit();
                    image.UriSource = new Uri(path);
                    image.EndInit();
                    ImageBehavior.SetAnimatedSource(Back, image);
                    ImageBehavior.SetRepeatBehavior(Back, RepeatBehavior.Forever);
                }
                else
                {
                    Back.ClearValue(Image.SourceProperty);
                    Back.Source = new BitmapImage(new Uri(path));
                }
            });
        }


        private void ShowWindow()
        {
            Dispatcher.Invoke(() =>
            {
                MoveMouseRandomly();
                Show();
                Opacity = 1;
                IsHitTestVisible = true;
            });
        }
        private void HideWindow()
        {
            Dispatcher.Invoke(() =>
            {
                Hide();
                Opacity = 0;
                IsHitTestVisible = false;
            });
        }

        private void Grid_MouseDown(object sender, MouseButtonEventArgs e)
        {
        }
    }
}
