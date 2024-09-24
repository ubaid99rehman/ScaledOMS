using DevExpress.Mvvm;
using DevExpress.Mvvm.Native;
using DevExpress.Xpf.Core;
using OMS.Core.Models.Themes;
using OMS.Enums;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Configuration;
using System.IO;
using System.Windows.Media;
using System.Xml.Linq;

namespace OMS.VM.Settings
{
    public class AppThemeModel : ViewModelBase
    {
        private ObservableCollection<string> _themes;
        public ObservableCollection<string> Themes
        {
            get { return _themes; } 
            set { SetProperty(ref _themes, value, nameof(Themes)); }
        }

        private ObservableCollection<ThemeModel> _appThemes;
        public ObservableCollection<ThemeModel> AppThemes
        {
            get { return _appThemes; }
            set { SetProperty(ref _appThemes, value, nameof(AppThemes)); }
        }

        private ThemeModel _selectedTheme;
        public ThemeModel SelectedTheme
        {
            get { return _selectedTheme; }
            set { SetProperty(ref _selectedTheme, value, nameof(SelectedTheme)); }
        }

        private ThemeModel _appliedTheme;
        public ThemeModel AppliedTheme
        {
            get { return _appliedTheme; }
            set { SetProperty(ref _appliedTheme, value, nameof(AppliedTheme)); }
        }

        public AppThemeModel()
        {
            _themes = new ObservableCollection<string>();
            _appThemes = new ObservableCollection<ThemeModel>();
            _selectedTheme = new ThemeModel();
        }

        private void LoadAppThemes()
        {
            string _themeDirectoryPath = ConfigurationManager.AppSettings["ThemeDirectory"];

            var themeList = new List<ThemeModel>();
            var themeFiles = Directory.GetFiles(_themeDirectoryPath, "*.xml");

            foreach (var themeFile in themeFiles)
            {
                var ThemeModel = LoadThemeFromXml(themeFile);
                themeList.Add(ThemeModel);
            }
            AppThemes = themeList.ToObservableCollection<ThemeModel>();
        }

        public void SaveAppliedTheme(ThemeModel theme)
        {
            Configuration config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
            config.AppSettings.Settings["ThemeName"].Value = theme.ThemeName;
            config.AppSettings.Settings["TextBackground"].Value = theme.TextBackground.Color.ToString();
            config.AppSettings.Settings["TextForeground"].Value = theme.TextForeground.Color.ToString();
            config.AppSettings.Settings["TextBoxBackground"].Value = theme.TextBoxBackground.Color.ToString();
            config.AppSettings.Settings["TextBoxForeground"].Value = theme.TextBoxForeground.Color.ToString();
            config.AppSettings.Settings["ButtonBackground"].Value = theme.ButtonBackground.Color.ToString();
            config.AppSettings.Settings["ButtonForeground"].Value = theme.ButtonForeground.Color.ToString();
            config.AppSettings.Settings["TitleBarBackground"].Value = theme.TitleBarBackground.Color.ToString();
            config.AppSettings.Settings["TitleBarForeground"].Value = theme.TitleBarForeground.Color.ToString();
            config.AppSettings.Settings["FontFamily"].Value = theme.FontFamily.ToString();
            config.AppSettings.Settings["FontWeight"].Value = theme.FontWeight.ToString();
            config.AppSettings.Settings["FontSize"].Value = theme.FontSize.ToString();
            config.Save(ConfigurationSaveMode.Modified);
            ConfigurationManager.RefreshSection("appSettings");
        }

        public void GetAppliedTheme()
        {
           AppliedTheme = new ThemeModel
           {
               ThemeName = ConfigurationManager.AppSettings["ThemeName"],
               TextBackground = new SolidColorBrush((Color)ColorConverter.ConvertFromString(ConfigurationManager.AppSettings["TextBackground"])),
               TextForeground = new SolidColorBrush((Color)ColorConverter.ConvertFromString(ConfigurationManager.AppSettings["TextForeground"])),
               TextBoxBackground = new SolidColorBrush((Color)ColorConverter.ConvertFromString(ConfigurationManager.AppSettings["TextBoxBackground"])),
               TextBoxForeground = new SolidColorBrush((Color)ColorConverter.ConvertFromString(ConfigurationManager.AppSettings["TextBoxForeground"])),
               ButtonBackground = new SolidColorBrush((Color)ColorConverter.ConvertFromString(ConfigurationManager.AppSettings["ButtonBackground"])),
               ButtonForeground = new SolidColorBrush((Color)ColorConverter.ConvertFromString(ConfigurationManager.AppSettings["ButtonForeground"])),
               TitleBarBackground = new SolidColorBrush((Color)ColorConverter.ConvertFromString(ConfigurationManager.AppSettings["TitleBarBackground"])),
               TitleBarForeground = new SolidColorBrush((Color)ColorConverter.ConvertFromString(ConfigurationManager.AppSettings["TitleBarForeground"])),
               FontFamily = (FontFamilyEnum)Enum.Parse(typeof(FontFamilyEnum), ConfigurationManager.AppSettings["FontFamily"]),
               FontWeight = (FontWeightEnum)Enum.Parse(typeof(FontWeightEnum), ConfigurationManager.AppSettings["FontWeight"]),
               FontSize = (FontSizeEnum)Enum.Parse(typeof(FontSizeEnum), ConfigurationManager.AppSettings["FontSize"])
           };
        }

        private static ThemeModel LoadThemeFromXml(string themeFilePath)
        {
            var xmlDoc = XDocument.Load(themeFilePath);

            var themeName = xmlDoc.Root.Element("ThemeName")?.Value;
            var textBackground = (SolidColorBrush)new BrushConverter().ConvertFromString(xmlDoc.Root.Element("TextBackground")?.Value);
            var textForeground = (SolidColorBrush)new BrushConverter().ConvertFromString(xmlDoc.Root.Element("TextForeground")?.Value);
            var textBoxBackground = (SolidColorBrush)new BrushConverter().ConvertFromString(xmlDoc.Root.Element("TextBoxBackground")?.Value);
            var textBoxForeground = (SolidColorBrush)new BrushConverter().ConvertFromString(xmlDoc.Root.Element("TextBoxForeground")?.Value);
            var buttonBackground = (SolidColorBrush)new BrushConverter().ConvertFromString(xmlDoc.Root.Element("ButtonBackground")?.Value);
            var buttonForeground = (SolidColorBrush)new BrushConverter().ConvertFromString(xmlDoc.Root.Element("ButtonForeground")?.Value);
            var titleBarBackground = (SolidColorBrush)new BrushConverter().ConvertFromString(xmlDoc.Root.Element("TitleBarBackground")?.Value);
            var titleBarForeground = (SolidColorBrush)new BrushConverter().ConvertFromString(xmlDoc.Root.Element("TitleBarForeground")?.Value);
            var fontFamily = (FontFamilyEnum)Enum.Parse(typeof(FontFamilyEnum), xmlDoc.Root.Element("FontFamily")?.Value ?? "Calibri");
            var fontWeight = (FontWeightEnum)Enum.Parse(typeof(FontWeightEnum), xmlDoc.Root.Element("FontWeight")?.Value ?? "Normal");
            var fontSize = (FontSizeEnum)Enum.Parse(typeof(FontSizeEnum), xmlDoc.Root.Element("FontSize")?.Value ?? "Normal");

            var model = new ThemeModel(themeName, textBackground, textForeground, textBoxBackground, textBoxForeground, buttonBackground, buttonForeground, titleBarBackground, titleBarForeground, fontFamily, fontWeight, fontSize);
            return model;
        }
    }
}
