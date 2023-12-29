using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Windows;
using System.Xml.Serialization;

namespace CustomScreensaver
{
    public partial class SettingsService : ViewModelBase
    {
        public SettingsService()
        {
            HorizontalAlignmentItems = new System.Collections.Generic.List<HorizontalAlignment>() { HorizontalAlignment.Left, HorizontalAlignment.Right, HorizontalAlignment.Center };
            VerticalAlignmentItems = new System.Collections.Generic.List<VerticalAlignment>() { VerticalAlignment.Top, VerticalAlignment.Bottom, VerticalAlignment.Center };
            Update();
        }

        public void Update()
        {
            var settings = Properties.Settings.Default;
            ImageOpacity = settings.ImageOpacity;
            TextBlockHorizontalAlignment = settings.TextBlockHorizontalAlignment;
            TextBlockVerticalAlignment = settings.TextBlockVerticalAlignment;
            BackgroundDarkness = settings.BackgroundDarkness;
            ImageOpacity = settings.ImageOpacity;
            BlurValue = settings.BlurValue;
            DateFontSize = settings.DateFontSize;
            TimeFontSize = settings.TimeFontSize;
            TextBlockMargin = settings.TextBlockMargin;
        }

        [RelayCommand]
        public void Save()
        {
            var settings = Properties.Settings.Default;
            settings.ImageOpacity = ImageOpacity;
            settings.TextBlockHorizontalAlignment = TextBlockHorizontalAlignment;
            settings.TextBlockVerticalAlignment = TextBlockVerticalAlignment;
            settings.BackgroundDarkness = BackgroundDarkness;
            settings.ImageOpacity = ImageOpacity;
            settings.BlurValue = BlurValue;
            settings.DateFontSize = DateFontSize;
            settings.TimeFontSize = TimeFontSize;
            settings.TextBlockMargin = TextBlockMargin;
            settings.Save();

        }

        internal void ToggleTextBlockVisibility()
        {
            TextBlockVisibility = TextBlockVisibility == Visibility.Collapsed ? Visibility.Visible : Visibility.Collapsed;
        }

        #region Property Type:[Visibility] Name:[TextBlockVisibility] FieldName:[_textBlockVisibility] 
        public Visibility TextBlockVisibility
        {
            get => _textBlockVisibility; set
            {
                _textBlockVisibility = value;
                OnPropertyChanged(nameof(TextBlockVisibility));
            }
        }
        private Visibility _textBlockVisibility;
        #endregion

        #region Property Type:[double] Name:[TimeFontSize] FieldName:[_timeFontSize] 
        public double TimeFontSize
        {
            get => _timeFontSize; set
            {
                _timeFontSize = value;
                OnPropertyChanged(nameof(TimeFontSize));
            }
        }

        public List<HorizontalAlignment> HorizontalAlignmentItems { get; private set; }
        public List<VerticalAlignment> VerticalAlignmentItems { get; private set; }

        private double _timeFontSize;
        #endregion

        #region Property Type:[double] Name:[DateFontSize] FieldName:[_dateFontSize] 
        public double DateFontSize
        {
            get => _dateFontSize; set
            {
                _dateFontSize = value;
                OnPropertyChanged(nameof(DateFontSize));
            }
        }
        private double _dateFontSize;
        #endregion

        #region Property Type:[double] Name:[BlurValue] FieldName:[_blurValue] 
        public double BlurValue
        {
            get => _blurValue; set
            {
                _blurValue = value;
                OnPropertyChanged(nameof(BlurValue));
            }
        }
        private double _blurValue;
        #endregion

        #region Property Type:[double] Name:[ImageOpacity] FieldName:[_imageOpacoty] 
        public double ImageOpacity
        {
            get => _imageOpacoty; set
            {
                _imageOpacoty = value;
                OnPropertyChanged(nameof(ImageOpacity));
            }
        }
        private double _imageOpacoty;
        #endregion

        #region Property Type:[double] Name:[BackgroundDarkness] FieldName:[_backgroundDarkness] 
        public double BackgroundDarkness
        {
            get => _backgroundDarkness; set
            {
                _backgroundDarkness = value;
                OnPropertyChanged(nameof(BackgroundDarkness));
            }
        }
        private double _backgroundDarkness;
        #endregion

        #region Property Type:[VerticalAlignment] Name:[TextBlockVerticalAlignment] FieldName:[_textBlockVerticalAlignment] 
        public VerticalAlignment TextBlockVerticalAlignment
        {
            get => _textBlockVerticalAlignment; set
            {
                _textBlockVerticalAlignment = value;
                OnPropertyChanged(nameof(TextBlockVerticalAlignment));
            }
        }
        private VerticalAlignment _textBlockVerticalAlignment;
        #endregion

        #region Property Type:[HorizontalAlignment] Name:[TextBlockAlignment] FieldName:[_textBlockAlignment] 
        public System.Windows.HorizontalAlignment TextBlockHorizontalAlignment
        {
            get => _textBlockAlignment; set
            {
                _textBlockAlignment = value;
                OnPropertyChanged(nameof(TextBlockHorizontalAlignment));
            }
        }
        private HorizontalAlignment _textBlockAlignment = System.Windows.HorizontalAlignment.Center;

        #endregion

        #region Property Type:[double] Name:[TextBlockMargin] FieldName:[_textBlockMargin] 
        public double TextBlockMargin
        {
            get => _textBlockMargin; set
            {
                _textBlockMargin = value;
                OnPropertyChanged(nameof(TextBlockMargin));
            }
        }
        private double _textBlockMargin;
        #endregion
    }

}
