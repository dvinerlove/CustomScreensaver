using CustomScreensaver.Views;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media.Imaging;

namespace CustomScreensaver
{
    public class ScreensaverService
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

        private List<Key> settingsCombination = new List<Key>() {
                Key.RightCtrl,
                Key.NumPad1,
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


        private MainWindow _mainWindow;
        Random Random = new Random();
        private ScreensaverDisplayFields _screensaverViewModel;
        private SettingsService _settingsViewModel;
        private List<HotKeyCombination> _hotKeyCombinations = new List<HotKeyCombination>();
        private Image _image;
        public ScreensaverService(HotKeyService hotKeyService, SettingsService settingsViewModel, ScreensaverDisplayFields screensaverViewModel)
        {
            _screensaverViewModel = screensaverViewModel;
            _settingsViewModel = settingsViewModel;
            HotKeyService = hotKeyService;

        }
        public void SetMainWindow(MainWindow mainWindow)
        {
            _mainWindow = mainWindow;
            _mainWindow.IsHitTestVisible = false;
            SetHotKeys();
        }

        public void SetImage(Image image)
        {
            _image = image;
        }

        private void SetHotKeys()
        {
            _hotKeyCombinations.Add(new HotKeyCombination()
            {
                Name = "showCombination",
                Action = ActionType.Show,
                Keys = showCombination
            });

            _hotKeyCombinations.Add(new HotKeyCombination()
            {
                Name = "hideCombination",
                Action = ActionType.Hide,
                Keys = hideCombination
            });
            _hotKeyCombinations.Add(new HotKeyCombination()
            {
                Name = "settingsCombination",
                Action = ActionType.OpenSettings,
                Keys = settingsCombination
            });
            _hotKeyCombinations.Add(new HotKeyCombination()
            {
                Name = "changeBackCombination",
                Action = ActionType.ChangeBackground,
                Keys = changeBackCombination
            });
            _hotKeyCombinations.Add(new HotKeyCombination()
            {
                Name = "ToggleText",
                Action = ActionType.ToggleText,
                Keys = showTimeCombination
            });

            foreach (var item in _hotKeyCombinations)
            {
                App.Current.Dispatcher.Invoke(() =>
                {
                    HotKeyService.SetCombination(item.Keys, () =>
                    {
                        _mainWindow.HideSettings();
                        switch (item.Action)
                        {
                            case ActionType.Show:
                                ManualStop = false;
                                ShowWindow();
                                break;
                            case ActionType.Hide:
                                ManualStop = true;
                                HideWindow();

                                break;
                            case ActionType.ToggleText:
                                ShowTime();
                                break;
                            case ActionType.OpenSettings:
                                ManualStop = true;
                                HideWindow();
                                _mainWindow.ShowSettings();
                                break;
                            case ActionType.ChangeBackground:
                                ChangeBack();
                                break;
                            default:
                                break;
                        }

                    });
                });

            }

            HotKeyService.SetCombination(breakwindowswitch, BreakSwitch);

            HotKeyService.Start();
        }

        private void ShowWindow()
        {
            App.Current.Dispatcher.Invoke(() =>
             {
                 MoveMouseRandomly();
                 _mainWindow.Show();
                 _mainWindow.Opacity = 1;
                 _mainWindow.IsHitTestVisible = true;
                 _mainWindow.WindowState = WindowState.Maximized;
                 Mouse.OverrideCursor = Cursors.None;
             });
        }
        private void HideWindow()
        {
            App.Current.Dispatcher.Invoke(() =>
            {
                Mouse.OverrideCursor = null;
                _mainWindow.Hide();
                _mainWindow.Opacity = 0;
                _mainWindow.IsHitTestVisible = false;
            });
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
            App.Current.Dispatcher.Invoke(() =>
             {
                 if (ManualStop == false)
                 { _settingsViewModel.ToggleTextBlockVisibility(); }
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




        private void ChangeBack()
        {
            App.Current.Dispatcher.Invoke(() =>
             {
                 var files = Directory.GetFiles("Backgrounds", "*");
                 List<string> imageFiles = new List<string>();
                 foreach (string filename in files)
                 {
                     if (Regex.IsMatch(filename, @"\.jpg$|\.png$|\.gif$"))
                         imageFiles.Add(filename);
                 }

                 _image.Visibility = Random.Next(10) == 5 ? Visibility.Collapsed : Visibility.Visible;

                 var path = System.IO.Path.Combine(AppContext.BaseDirectory, imageFiles[Random.Next(imageFiles.Count)]);

                 _image.Visibility = Visibility.Collapsed;
                 //BackGif.Visibility = Visibility.Collapsed;

                 if (Regex.IsMatch(path, @"\.gif$"))
                 {
                     //var image = new BitmapImage();
                     //image.BeginInit();
                     //image.UriSource = new Uri(path);
                     //image.EndInit();
                     //ImageBehavior.SetAnimatedSource(BackGif, image);
                     //ImageBehavior.SetRepeatBehavior(BackGif, RepeatBehavior.Forever);
                     //BackGif.Visibility = Visibility.Visible;
                 }
                 else
                 {
                     _image.ClearValue(Image.SourceProperty);
                     _image.Source = new BitmapImage(new Uri(path));
                     _image.Visibility = Visibility.Visible;
                 }
             });
        }

        internal ScreensaverView GetScreensaverView()
        {
            return _mainWindow.ScreensaverView;
        }
    }

}
