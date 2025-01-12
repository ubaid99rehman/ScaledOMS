﻿using DevExpress.Mvvm;
using DevExpress.Mvvm.Native;
using OMS.Core.Models.App;
using OMS.Core.Models.Themes;
using OMS.Core.Services.AppServices;
using OMS.Enums;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Media;

namespace OMS.VM.Settings
{
    public class AppThemeModel : ViewModelBase
    {
        IAppThemeService _appThemeService;

        #region Private Members
        private Color _textBackground;
        private Color _textForeground;
        private Color _textBoxBackground;
        private Color _textBoxForeground;
        private Color _buttonBackground;
        private Color _buttonForeground;
        private Color _titleBarBackground;
        private Color _titleBarForeground;
        private string _themeName;
        private string _appliedThemeName;
        private ObservableCollection<string> _themes;
        private IThemeModel _selectedTheme;
        private ObservableCollection<IThemeModel> _appThemes;
        #endregion

        #region Public Members
        public Color TextBackground
        {
            get => _textBackground;
            set
            {
                SetProperty(ref _textBackground, value, nameof(TextBackground));
                SelectedTheme.TextBackground = new SolidColorBrush(value);
            }
        }
        public Color TextForeground
        {
            get => _textForeground;
            set
            {
                SetProperty(ref _textForeground, value, nameof(TextForeground));
                SelectedTheme.TextForeground = new SolidColorBrush(value);
            }
        }
        public Color TextBoxBackground
        {
            get => _textBoxBackground;
            set
            {
                SetProperty(ref _textBoxBackground, value, nameof(TextBoxBackground));
                SelectedTheme.TextBoxBackground = new SolidColorBrush(value);
            }
        }
        public Color TextBoxForeground
        {
            get => _textBoxForeground;
            set
            {
                SetProperty(ref _textBoxForeground, value, nameof(TextBoxForeground));
                SelectedTheme.TextBoxForeground = new SolidColorBrush(value);
            }
        }
        public Color ButtonBackground
        {
            get => _buttonBackground;
            set
            {
                SetProperty(ref _buttonBackground, value, nameof(ButtonBackground));
                SelectedTheme.ButtonBackground = new SolidColorBrush(value);
            }
        }
        public Color ButtonForeground
        {
            get => _buttonForeground;
            set
            {
                SetProperty(ref _buttonForeground, value, nameof(ButtonForeground));
                SelectedTheme.ButtonForeground = new SolidColorBrush(value);
            }
        }
        public Color TitleBarBackground
        {
            get => _titleBarBackground;
            set
            {
                SetProperty(ref _titleBarBackground, value, nameof(TitleBarBackground));
                SelectedTheme.TitleBarBackground = new SolidColorBrush(value);
            }
        }
        public Color TitleBarForeground
        {
            get => _titleBarForeground;
            set
            {
                SetProperty(ref _titleBarForeground, value, nameof(TitleBarForeground));
                SelectedTheme.TitleBarForeground = new SolidColorBrush(value);
            }
        }
        public string ThemeName
        {
            get { return _themeName; }
            set
            {
                SetProperty(ref _themeName, value, nameof(ThemeName));
                ChangeSelectedTheme();
            }
        }
        public string AppliedThemeName
        {
            get { return _appliedThemeName; }
            set
            {
                SetProperty(ref _appliedThemeName, value, nameof(AppliedThemeName));
                ChangeSelectedTheme();
            }
        }
        public ObservableCollection<string> Themes
        {
            get { return _themes; }
            set { SetProperty(ref _themes, value, nameof(Themes)); }
        }
        public IThemeModel SelectedTheme
        {
            get { return _selectedTheme; }
            set
            {
                SetProperty(ref _selectedTheme, value, nameof(SelectedTheme));
                InitColors();
            }
        }
        public ObservableCollection<IThemeModel> AppThemes
        {
            get { return _appThemes; }
            set { SetProperty(ref _appThemes, value, nameof(AppThemes)); }
        } 
        public ObservableCollection<FontFamilyEnum> FontFamilyOptions { get; set; }
        public ObservableCollection<FontSizeEnum> FontSizeOptions { get; set; }
        public ObservableCollection<FontWeight> FontWeightOptions { get; set; }
        public ObservableCollection<double> ButtonBorderThicknessOptions { get; set; }
        #endregion

        //Constructor
        public AppThemeModel(IAppThemeService appThemeService)
        {
            _themes = new ObservableCollection<string>();
            _appThemes = new ObservableCollection<IThemeModel>();
            _selectedTheme = new ThemeModel();
            _appThemeService = appThemeService;
            FontFamilyOptions = new ObservableCollection<FontFamilyEnum>
            {
                FontFamilyEnum.Arial,
                FontFamilyEnum.Calibri,
                FontFamilyEnum.CourierNew,
                FontFamilyEnum.Georgia,
                FontFamilyEnum.SegoeUI,
                FontFamilyEnum.TimesNewRoman,
                FontFamilyEnum.Verdana,
                FontFamilyEnum.ComicSansMS,
                FontFamilyEnum.Tahoma
            };
            FontSizeOptions = new ObservableCollection<FontSizeEnum>
            {
                FontSizeEnum.Small,
                FontSizeEnum.Normal,
                FontSizeEnum.Large,
                FontSizeEnum.ExtraLarge
            };
            FontWeightOptions = new ObservableCollection<FontWeight>
            {
                FontWeights.Black,
                FontWeights.Bold,
                FontWeights.ExtraBlack,
                FontWeights.ExtraBold,
                FontWeights.ExtraLight,
                FontWeights.Heavy,
                FontWeights.Light,
                FontWeights.Medium,
                FontWeights.Normal,
                FontWeights.Regular,
                FontWeights.SemiBold,
                FontWeights.Thin,
                FontWeights.UltraBlack,
                FontWeights.UltraBold,
                FontWeights.UltraLight,
            };
            ButtonBorderThicknessOptions = new ObservableCollection<double>()
            {
                1.0,
                2.0,
                3.0,
                4.0,
                5.0,
            };
            InitTheme();
        }

        //Priavte Methods 
        private void InitTheme()
        {
            LoadThemes();
            ThemeName = Themes.FirstOrDefault();
            AppliedThemeName = _appThemeService.GetAppliedTheme().ThemeName;
            SelectedTheme = AppThemes.First();
            InitColors();
        }
        private void LoadThemes()
        {
            AppThemes = _appThemeService.GetThemes().ToObservableCollection();
            Themes = _appThemeService.GetThemeNames().ToObservableCollection();
        }
        private void InitColors()
        {
            if (SelectedTheme != null)
            {
                TextBackground = SelectedTheme.TextBackground.Color;
                TextForeground = SelectedTheme.TextForeground.Color;
                TextBoxBackground = SelectedTheme.TextBoxBackground.Color;
                TextBoxForeground = SelectedTheme.TextBoxForeground.Color;
                ButtonBackground = SelectedTheme.ButtonBackground.Color;
                ButtonForeground = SelectedTheme.ButtonForeground.Color;
                TitleBarBackground = SelectedTheme.TitleBarBackground.Color;
                TitleBarForeground = SelectedTheme.TitleBarForeground.Color;
            }
        }
        private void ChangeSelectedTheme()
        {
            if (!string.IsNullOrEmpty(ThemeName)) 
            {
                var theme = AppThemes.Where(t => t.ThemeName == ThemeName).FirstOrDefault();
                if (theme != null) 
                {
                    SelectedTheme = theme;
                }
            }
        }

        //Public Methods
        public IThemeModel GetSelectedTheme()
        {
            return _appThemeService.GetThemes().Where(t => t.ThemeName == ThemeName).FirstOrDefault();
        }
        public void SaveAppliedTheme()
        {
            _appThemeService.SaveAppliedTheme(GetSelectedTheme());
            AppliedThemeName = _appThemeService.GetAppliedTheme().ThemeName;
        }
        public bool SaveTheme(out string message)
        {
            if (SelectedTheme != null)
            {
                if(!Themes.Contains(SelectedTheme.ThemeName))
                {
                    bool themeAdded = _appThemeService.SaveTheme(SelectedTheme);
                    if(themeAdded)
                    {
                        LoadThemes();
                        message = $"New Theme added as: {SelectedTheme.ThemeName}.";
                        return true;
                    }
                }
                message = $"A Theme with name {SelectedTheme.ThemeName} already exists.";
                return false;
            }
            message = $"No Theme to Save.";
            return false;
        }
    }
}
