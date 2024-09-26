using OMS.Enums;
using System;
using System.Windows;
using System.Windows.Media;

namespace OMS.Core.Models.Themes
{
    public class ThemeModel : BaseModel
    {
        private string _themeName;
        private SolidColorBrush _screenbackground;
        private SolidColorBrush _textBackground;
        private SolidColorBrush _textForeground;
        private SolidColorBrush _textBoxBackground;
        private SolidColorBrush _textBoxForeground;
        private SolidColorBrush _buttonBackground;
        private SolidColorBrush _buttonForeground;
        private SolidColorBrush _titleBarBackground;
        private SolidColorBrush _titleBarForeground;
        private FontFamilyEnum _fontFamily;
        private FontWeight _fontWeight;
        private FontSizeEnum _fontSize;
        private double _formattedFontSize;
        private double _buttonBorderThickness;
        private string _fontFamilyName;

        public ThemeModel()
        {
            TextBackground = new SolidColorBrush(Colors.Black);
            TextForeground = new SolidColorBrush(Colors.White);
            TextBoxBackground = new SolidColorBrush(Colors.Black);
            TextBoxForeground = new SolidColorBrush(Colors.White);
            ButtonBackground = new SolidColorBrush(Colors.Black);
            ButtonForeground = new SolidColorBrush(Colors.White);
            TitleBarBackground = new SolidColorBrush(Colors.LightSkyBlue);
            TitleBarForeground = new SolidColorBrush(Colors.White);
            FontFamily = FontFamilyEnum.Calibri;
            FontWeight = FontWeights.Normal;
            FontSize = FontSizeEnum.Normal;
            ButtonBorderThickness = 1;
        }

        public ThemeModel(
            FontWeight fontWeight,
            string themeName = "Default Theme",
            SolidColorBrush screenBackground = null,
            SolidColorBrush textBackground = null,
            SolidColorBrush textForeground = null,
            SolidColorBrush textBoxBackground = null,
            SolidColorBrush textBoxForeground = null,
            SolidColorBrush buttonBackground = null,
            SolidColorBrush buttonForeground = null,
            SolidColorBrush titleBarBackground = null,
            SolidColorBrush titleBarForeground = null,
            FontFamilyEnum fontFamily = FontFamilyEnum.Calibri,
            FontSizeEnum fontSize = FontSizeEnum.Normal,
            double buttonBorderThickness = 1)
        {
            ThemeName = themeName;
            ScreenBackground = screenBackground ?? new SolidColorBrush(Colors.White);
            TextBackground = textBackground ?? new SolidColorBrush(Colors.Black);
            TextForeground = textForeground ?? new SolidColorBrush(Colors.White);
            TextBoxBackground = textBoxBackground ?? new SolidColorBrush(Colors.Black);
            TextBoxForeground = textBoxForeground ?? new SolidColorBrush(Colors.White);
            ButtonBackground = buttonBackground ?? new SolidColorBrush(Colors.Black);
            ButtonForeground = buttonForeground ?? new SolidColorBrush(Colors.White);
            TitleBarBackground = titleBarBackground ?? new SolidColorBrush(Colors.LightSkyBlue);
            TitleBarForeground = titleBarForeground ?? new SolidColorBrush(Colors.White);
            FontFamily = fontFamily;
            FontWeight = fontWeight;
            FontSize = fontSize;
            ButtonBorderThickness = buttonBorderThickness;
        }

        public string ThemeName
        {
            get => _themeName;
            set
            {
                SetProperty(ref _themeName, value, nameof(ThemeName));
            }
        }

        public SolidColorBrush ScreenBackground
        {
            get => _screenbackground;
            set => SetProperty(ref _screenbackground, value, nameof(ScreenBackground));
        }

        public SolidColorBrush TextBackground
        {
            get => _textBackground;
            set => SetProperty(ref _textBackground, value, nameof(TextBackground));
        }

        public SolidColorBrush TextForeground
        {
            get => _textForeground;
            set => SetProperty(ref _textForeground, value, nameof(TextForeground));
        }

        public SolidColorBrush TextBoxBackground
        {
            get => _textBoxBackground;
            set => SetProperty(ref _textBoxBackground, value, nameof(TextBoxBackground));
        }

        public SolidColorBrush TextBoxForeground
        {
            get => _textBoxForeground;
            set => SetProperty(ref _textBoxForeground, value, nameof(TextBoxForeground));
        }

        public SolidColorBrush ButtonBackground
        {
            get => _buttonBackground;
            set => SetProperty(ref _buttonBackground, value, nameof(ButtonBackground));
        }

        public SolidColorBrush ButtonForeground
        {
            get => _buttonForeground;
            set => SetProperty(ref _buttonForeground, value, nameof(ButtonForeground));
        }

        public SolidColorBrush TitleBarBackground
        {
            get => _titleBarBackground;
            set
            {
                SetProperty(ref _titleBarBackground, value, nameof(TitleBarBackground));
            }
        }

        public SolidColorBrush TitleBarForeground
        {
            get => _titleBarForeground;
            set
            {
                SetProperty(ref _titleBarForeground, value, nameof(TitleBarForeground));
            }
        }

        public FontWeight FontWeight
        {
            get => _fontWeight;
            set => SetProperty(ref _fontWeight, value, nameof(FontWeight));
        }

        public FontFamilyEnum FontFamily
        {
            get => _fontFamily;
            set 
            {
                SetProperty(ref _fontFamily, value, nameof(FontFamily));
                FontFamilyName = value.ToString();
            }
        }

        public FontSizeEnum FontSize
        {
            get => _fontSize;
            set 
            { 
                SetProperty(ref _fontSize, value, nameof(FontSize));
                FormattedFontSize = UpdateFormatSize(value);
            }
        }

        private double UpdateFormatSize(FontSizeEnum value)
        {
            switch (FontSize)
            {
                case FontSizeEnum.Normal:
                    return 12;
                case FontSizeEnum.Small:
                    return 8.0;
                case FontSizeEnum.Large:
                    return 16.0;
                case FontSizeEnum.ExtraLarge:
                    return 20.0;
                default:
                    return 12.0;
            };
        }

        public double FormattedFontSize
        {
            get => _formattedFontSize;
            set { SetProperty(ref _formattedFontSize, value, nameof(FormattedFontSize)); }
        }

        public string FontFamilyName
        {
            get => _fontFamilyName;
            set
            {
                SetProperty(ref _fontFamilyName, value, nameof(FontFamilyName));
            }
        }
        
        public double ButtonBorderThickness
        { 
            get => _buttonBorderThickness;
            set { SetProperty(ref _buttonBorderThickness, value, nameof(ButtonBorderThickness)); }
        }
    }
}
