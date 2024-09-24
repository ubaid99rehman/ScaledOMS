using OMS.Core.Models.Themes;
using OMS.Enums;
using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Media;
using System.Xml.Linq;


namespace OMS.Common.Helper
{
    public static class AppThemeHelper
    {
        public static FontWeight GetFontWeight(FontWeightEnum fontWeightEnum)
        {
            switch (fontWeightEnum)
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
            switch (fontSizeEnum)
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
    }
}
