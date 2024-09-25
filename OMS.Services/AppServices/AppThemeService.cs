using OMS.Core.Models.Themes;
using OMS.Core.Services.AppServices;
using System.Collections.Generic;
using System.Xml.Linq;
using System.Configuration;
using System.IO;
using System.Windows.Media;
using OMS.Enums;
using System;
using System.Linq;
using OMS.Logging;
using System.Xml.Serialization;
using System.Windows;

namespace OMS.Services
{
    public class AppThemeService : IAppThemeService
    {
        string _themeDirectoryPath;

        List<string> ThemeNames;
        List<ThemeModel> AppThemes;

        public AppThemeService() 
        {
            _themeDirectoryPath = ConfigurationManager.AppSettings["ThemeDirectory"];
            AppThemes = new List<ThemeModel>();
            ThemeNames = new List<string>();
            LoadThemes();
        }

        public ThemeModel GetAppliedTheme()
        {
            var fontWeightConverter = new FontWeightConverter();
            var fontWeight = fontWeightConverter.ConvertFromString(ConfigurationManager.AppSettings["FontFamily"].ToString() ?? "Normal");
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
                FontWeight = (FontWeight)fontWeight,
                FontSize = (FontSizeEnum)Enum.Parse(typeof(FontSizeEnum), ConfigurationManager.AppSettings["FontSize"])
            };
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

        public List<ThemeModel> GetThemes()
        {
            LoadThemes();
            return AppThemes;
        }

        public List<string> GetThemeNames()
        {
            return ThemeNames;
        }

        public bool SaveTheme(ThemeModel model)
        {
            try
            {
                var themeFilePath = Path.Combine(_themeDirectoryPath, $"{model.ThemeName}.xml");

                var xmlContent = new XDocument(
                    new XDeclaration("1.0", "utf-8", "yes"),
                    new XElement("Theme",
                        new XElement("ThemeName", model.ThemeName),
                        new XElement("TextBackground", model.TextBackground),
                        new XElement("TextForeground", model.TextForeground),
                        new XElement("TextBoxBackground", model.TextBoxBackground),
                        new XElement("TextBoxForeground", model.TextBoxForeground),
                        new XElement("ButtonBackground", model.ButtonBackground),
                        new XElement("ButtonForeground", model.ButtonForeground),
                        new XElement("TitleBarBackground", model.TitleBarBackground),
                        new XElement("TitleBarForeground", model.TitleBarForeground),
                        new XElement("FontFamily", model.FontFamily.ToString()),
                        new XElement("FontWeight", model.FontWeight.ToString()),
                        new XElement("FontSize", model.FontSize.ToString())
                    )
                );

                xmlContent.Save(themeFilePath);
                return true;
            }
            catch (Exception ex)
            {
                LogHelper.LogError(ex.ToString(), ex);
                return false;
            }
        }

        private ThemeModel LoadThemeFromXml(string themeFilePath)
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
            var fontWeightConverter = new FontWeightConverter();
            var fontWeight = (FontWeight)fontWeightConverter.ConvertFromString(xmlDoc.Root.Element("FontWeight")?.Value ?? "Normal");

            var fontSize = (FontSizeEnum)Enum.Parse(typeof(FontSizeEnum), xmlDoc.Root.Element("FontSize")?.Value ?? "Normal");

            var model = new ThemeModel(fontWeight, themeName, textBackground, textForeground, textBoxBackground, textBoxForeground, buttonBackground, buttonForeground, titleBarBackground, titleBarForeground, fontFamily, fontSize );
            return model;
        }

        private void LoadThemes()
        {
            AppThemes.Clear();
            ThemeNames.Clear();
            var themeFiles = Directory.GetFiles(_themeDirectoryPath, "*.xml");
            foreach (var themeFile in themeFiles)
            {
                var ThemeModel = LoadThemeFromXml(themeFile);
                AppThemes.Add(ThemeModel);
                ThemeNames.Add(ThemeModel.ThemeName);
            }
        }
    }
}
