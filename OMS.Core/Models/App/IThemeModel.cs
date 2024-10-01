using OMS.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using System.Windows;

namespace OMS.Core.Models.App
{
    public interface IThemeModel
    {
        string ThemeName { get; set; }
        SolidColorBrush ScreenBackground { get; set; }
        SolidColorBrush TextBackground { get; set; }
        SolidColorBrush TextForeground { get; set; }
        SolidColorBrush TextBoxBackground { get; set; }
        SolidColorBrush TextBoxForeground { get; set; }
        SolidColorBrush ButtonBackground { get; set; }
        SolidColorBrush ButtonForeground { get; set; }
        SolidColorBrush TitleBarBackground { get; set; }
        SolidColorBrush TitleBarForeground { get; set; }
        FontWeight FontWeight { get; set; }
        FontFamilyEnum FontFamily { get; set; }
        FontSizeEnum FontSize { get; set; }
        double FormattedFontSize { get; set; }
        string FontFamilyName { get; set; }
        double ButtonBorderThickness { get; set; }
    }
}
