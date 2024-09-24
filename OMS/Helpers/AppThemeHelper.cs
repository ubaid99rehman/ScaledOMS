using OMS.Core.Models.Themes;
using OMS.Enums;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Windows;
using System.Windows.Media;
using System.Xml.Linq;

namespace OMS.Helpers
{
    public static class AppThemeHelper
    {
        public static FontWeight GetFontWeight(FontWeightEnum fontWeightEnum)
        {
           switch(fontWeightEnum)
            {
                case FontWeightEnum.Black: return FontWeights.Black;
                case FontWeightEnum.Bold: return FontWeights.Bold;
                case FontWeightEnum.ExtraBlack: return FontWeights.ExtraBlack;
                case FontWeightEnum.ExtraLight: return FontWeights.ExtraLight;
                case FontWeightEnum.ExtraBold: return FontWeights.ExtraBold;
                case FontWeightEnum.Heavy: return FontWeights.Heavy;
                case FontWeightEnum.Light: return FontWeights.Light;
                case FontWeightEnum.Medium: return FontWeights.Medium;
                case FontWeightEnum.Normal: return FontWeights.Normal;
                case FontWeightEnum.Regular: return FontWeights.Regular;
                case FontWeightEnum.SemiBold: return FontWeights.SemiBold;
                case FontWeightEnum.UltraBold: return FontWeights.UltraBold;
                case FontWeightEnum.UltraBlack: return FontWeights.UltraBlack;
                case FontWeightEnum.UltraLight: return FontWeights.UltraLight;
                default: return FontWeights.Normal;
            };
        }

        public static double GetFontSize(FontSizeEnum fontSizeEnum)
        {
            switch( fontSizeEnum)
            {
                case FontSizeEnum.Normal:
                    return 16.0;
                case FontSizeEnum.Small:
                    return 12.0;
                case FontSizeEnum.Large:
                    return 20.0;
                case FontSizeEnum.ExtraLarge:
                    return 24.0;
                default:
                    return 16.0;
            };
        }

        private static ResourceDictionary GenerateResourceDictionary(ThemeModel ThemeModel)
        {
            var resourceDictionary = new ResourceDictionary();

            //Text
            resourceDictionary["TextBackground"] = ThemeModel.TextBackground;
            resourceDictionary["TextForeground"] = ThemeModel.TextForeground;

            //TextBox
            resourceDictionary["TextBoxBackground"] = ThemeModel.TextBoxBackground;
            resourceDictionary["TextBoxForeground"] = ThemeModel.TextBoxForeground;
            
            //Button
            resourceDictionary["ButtonBackground"] = ThemeModel.ButtonBackground;
            resourceDictionary["ButtonForeground"] = ThemeModel.ButtonForeground;
            
            //Title
            resourceDictionary["TitleBarBackground"] = ThemeModel.TitleBarBackground;
            resourceDictionary["TitleBarForeground"] = ThemeModel.TitleBarForeground;

            // Set fonts
            resourceDictionary["FontFamily"] = new FontFamily(ThemeModel.FontFamily.ToString());
            resourceDictionary["FontWeight"] = AppThemeHelper.GetFontWeight(ThemeModel.FontWeight);
            resourceDictionary["FontSize"] = AppThemeHelper.GetFontSize(ThemeModel.FontSize);

            return resourceDictionary;
        }

        public static void ChangeTheme(ThemeModel theme)
        {
            var dictioanry = GenerateResourceDictionary(theme);
            if (dictioanry != null)
            {
                App.Current.Resources.MergedDictionaries.Clear();
                App.Current.Resources.MergedDictionaries.Add(dictioanry);
            }
        }

        #region Applied Theme 
        public static ThemeModel GetAppliedTheme()
        {
            return new ThemeModel
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

        public static void SaveAppliedTheme(ThemeModel theme)
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
        #endregion

        #region Load Themes
        public static List<ThemeModel> LoadThemes()
        {
            string _themeDirectoryPath = ConfigurationManager.AppSettings["ThemeDirectory"];

            var themeList = new List<ThemeModel>();
            var themeFiles = Directory.GetFiles(_themeDirectoryPath, "*.xml");

            foreach (var themeFile in themeFiles)
            {
                var ThemeModel = LoadThemeFromXml(themeFile);
                themeList.Add(ThemeModel);
            }

            return themeList;
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
        #endregion
    }
}
