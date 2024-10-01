using OMS.Core.Models.Themes;
using OMS.Core.Services.AppServices;
using System.Collections.Generic;
using System.Xml.Linq;
using System.Configuration;
using System.IO;
using System.Windows.Media;
using OMS.Enums;
using System;
using OMS.Logging;
using System.Windows;
using OMS.Core.Models.App;

namespace OMS.Services
{
    public class AppThemeService : IAppThemeService
    {
        string _themeDirectoryPath;
        List<string> ThemeNames;
        List<IThemeModel> AppThemes;
        
        //Constructor
        public AppThemeService() 
        {
            _themeDirectoryPath = ConfigurationManager.AppSettings["ThemeDirectory"];
            AppThemes = new List<IThemeModel>();
            ThemeNames = new List<string>();
            LoadThemes();
        }
          
        //Public Access Methods
        public IThemeModel GetAppliedTheme()
        {
            var fontWeightConverter = new FontWeightConverter();
            var fontWeight = ConfigurationManager.AppSettings["FontWeight"].ToString() ?? "Normal";
            return new ThemeModel
            {
                ThemeName = ConfigurationManager.AppSettings["ThemeName"],
                ScreenBackground = new SolidColorBrush((Color)ColorConverter.ConvertFromString(ConfigurationManager.AppSettings["ScreenBackground"])),
                TitleBarBackground = new SolidColorBrush((Color)ColorConverter.ConvertFromString(ConfigurationManager.AppSettings["TitleBarBackground"])),
                TitleBarForeground = new SolidColorBrush((Color)ColorConverter.ConvertFromString(ConfigurationManager.AppSettings["TitleBarForeground"])),
                TextBackground = new SolidColorBrush((Color)ColorConverter.ConvertFromString(ConfigurationManager.AppSettings["TextBackground"])),
                TextForeground = new SolidColorBrush((Color)ColorConverter.ConvertFromString(ConfigurationManager.AppSettings["TextForeground"])),
                TextBoxBackground = new SolidColorBrush((Color)ColorConverter.ConvertFromString(ConfigurationManager.AppSettings["TextBoxBackground"])),
                TextBoxForeground = new SolidColorBrush((Color)ColorConverter.ConvertFromString(ConfigurationManager.AppSettings["TextBoxForeground"])),
                ButtonBackground = new SolidColorBrush((Color)ColorConverter.ConvertFromString(ConfigurationManager.AppSettings["ButtonBackground"])),
                ButtonForeground = new SolidColorBrush((Color)ColorConverter.ConvertFromString(ConfigurationManager.AppSettings["ButtonForeground"])),
                ButtonBorderThickness =Double.Parse(ConfigurationManager.AppSettings["ButtonBorderThickness"]),
                FontFamily = (FontFamilyEnum)Enum.Parse(typeof(FontFamilyEnum), ConfigurationManager.AppSettings["FontFamily"]),
                FontSize = (FontSizeEnum)Enum.Parse(typeof(FontSizeEnum), ConfigurationManager.AppSettings["FontSize"]),
                FontWeight = (FontWeight)fontWeightConverter.ConvertFromString(fontWeight)
            };
        }
        public void SaveAppliedTheme(IThemeModel theme)
        {
            Configuration config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
            config.AppSettings.Settings["ThemeName"].Value = theme.ThemeName;
            config.AppSettings.Settings["ScreenBackground"].Value = theme.ScreenBackground.Color.ToString();
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
            config.AppSettings.Settings["ButtonBorderThickness"].Value = theme.ButtonBorderThickness.ToString();
            config.Save(ConfigurationSaveMode.Modified);
            ConfigurationManager.RefreshSection("appSettings");
        }
        public List<IThemeModel> GetThemes()
        {
            LoadThemes();
            return AppThemes;
        }
        public List<string> GetThemeNames()
        {
            return ThemeNames;
        }
        public bool SaveTheme(IThemeModel model)
        {
            try
            {
                var themeFilePath = Path.Combine(_themeDirectoryPath, $"{model.ThemeName}.xml");

                var xmlContent = new XDocument(
                    new XDeclaration("1.0", "utf-8", "yes"),
                    new XElement("Theme",
                        new XElement("ThemeName", model.ThemeName),
                        new XElement("ScreenBackground", model.ScreenBackground),
                        new XElement("TitleBarBackground", model.TitleBarBackground),
                        new XElement("TitleBarForeground", model.TitleBarForeground),
                        new XElement("FontFamily", model.FontFamily.ToString()),
                        new XElement("FontWeight", model.FontWeight.ToString()),
                        new XElement("FontSize", model.FontSize.ToString()),
                        new XElement("TextBackground", model.TextBackground),
                        new XElement("TextForeground", model.TextForeground),
                        new XElement("TextBoxBackground", model.TextBoxBackground),
                        new XElement("TextBoxForeground", model.TextBoxForeground),
                        new XElement("ButtonBackground", model.ButtonBackground),
                        new XElement("ButtonForeground", model.ButtonForeground),
                        new XElement("ButtonBorderThickness", model.ButtonBorderThickness.ToString())
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

        //Private Methods
        private IThemeModel LoadThemeFromXml(string themeFilePath)
        {
            var xmlDoc = XDocument.Load(themeFilePath);

            var themeName = xmlDoc.Root.Element("ThemeName")?.Value;
            var screenBackground = (SolidColorBrush)new BrushConverter().ConvertFromString(xmlDoc.Root.Element("ScreenBackground")?.Value);
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
            var buttonBorderThickness = Double.Parse(xmlDoc.Root.Element("ButtonBorderThickness")?.Value ?? "1.0");
            var model = new ThemeModel(fontWeight, themeName, screenBackground, textBackground, textForeground, textBoxBackground, textBoxForeground, buttonBackground, buttonForeground, titleBarBackground, titleBarForeground, fontFamily, fontSize, buttonBorderThickness);
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
