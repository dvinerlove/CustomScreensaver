using CustomScreensaver.Views;
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
using System.Windows.Shapes;

namespace CustomScreensaver
{
    /// <summary>
    /// Логика взаимодействия для SettingsWindow.xaml
    /// </summary>
    public partial class SettingsWindow : Window
    {
        public SettingsWindow(ScreensaverService screensaverService,
                              ScreensaverViewModel viewModel)
        {
            ScreensaverViewModel = viewModel;
            _screensaverService = screensaverService;
            InitializeComponent();
            Loaded += SettingsWindow_Loaded;
            Activated += SettingsWindow_Activated;
        }

        private void SettingsWindow_Activated(object? sender, EventArgs e)
        {
            ScreensaverView.Back.Source = _screensaverService.GetScreensaverView().Back.Source;
        }

        private void SettingsWindow_Loaded(object sender, RoutedEventArgs e)
        {
            ScreensaverView.Back.Source =_screensaverService.GetScreensaverView().Back.Source;
        }

        public ScreensaverViewModel ScreensaverViewModel { get; }

        private ScreensaverService _screensaverService;
    }
}
