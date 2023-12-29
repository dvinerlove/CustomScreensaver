using CustomScreensaver.Properties;
using System;
using System.Timers;
using System.Windows.Media;
using System.Windows.Threading;

namespace CustomScreensaver
{

    public class ScreensaverDisplayFields : ViewModelBase
    {
        #region Property Type:[string] Name:[Date] FieldName:[_date] 
        public string Date
        {
            get => _date; set
            {
                _date = value;
                OnPropertyChanged(nameof(Date));
            }
        }
        private string _date = "";
        #endregion
        #region Property Type:[string] Name:[Time] FieldName:[_time] 
        public string Time
        {
            get => _time; set
            {
                _time = value;
                OnPropertyChanged(nameof(Time));
            }
        }
        private string _time = "";
        #endregion
        #region Property Type:[SettingsViewModel] Name:[settingsViewModel] FieldName:[_settingsViewModel] 
        public SettingsService Settings
        {
            get => _settingsViewModel; set
            {
                _settingsViewModel = value;
                OnPropertyChanged(nameof(Settings));
            }
        }
        private SettingsService _settingsViewModel;
        #endregion
        public ScreensaverDisplayFields(SettingsService settingsViewModel)
        {
            Settings = settingsViewModel;
        }

    }

}
