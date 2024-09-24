using Microsoft.Win32;
using OMS.Core.Models.Themes;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace OMS.Settings
{
    public partial class AppearanceView : UserControl
    {
        private List<ThemeModel> themes;
        private string themeDirectory;

        public AppearanceView()
        {
            InitializeComponent();
            themeDirectory = System.Configuration.ConfigurationManager.AppSettings["ThemeDirectory"];
            LoadThemes();
        }

        private void LoadThemes()
        {
            if (!Directory.Exists(themeDirectory))
                Directory.CreateDirectory(themeDirectory);

            themes = Directory.GetFiles(themeDirectory, "*.xml")
                .Select(file => LoadTheme(file))
                .Where(theme => theme != null)
                .ToList();

            ThemeComboBox.ItemsSource = themes;
        }

        private ThemeModel LoadTheme(string filePath)
        {
            //XDocument doc = XDocument.Load(filePath);
            //return new ThemeModel
            //{
            //    Name = doc.Root.Element("Name")?.Value,
            //    Foreground = doc.Root.Element("Foreground")?.Value,
            //    Background = doc.Root.Element("Background")?.Value,
            //    FontSize = double.Parse(doc.Root.Element("FontSize")?.Value ?? "12"),
            //    BarColor = doc.Root.Element("BarColor")?.Value,
            //    WindowColor = doc.Root.Element("WindowColor")?.Value
            //};
            return new ThemeModel();
        }

        private void ApplyTheme(ThemeModel theme)
        {
            //PreviewTextBlock.Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString(theme.Foreground));
            //PreviewButton.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString(theme.BarColor));
            //PreviewFontSizeBox.Text = theme.FontSize.ToString();
        }
        private void ThemeComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (ThemeComboBox.SelectedItem is ThemeModel selectedTheme)
            {
                ApplyTheme(selectedTheme);
            }
        }

        private void BrowseTheme_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "XML Files (*.xml)|*.xml";
            if (openFileDialog.ShowDialog() == true)
            {
                var theme = LoadTheme(openFileDialog.FileName);
                themes.Add(theme);
                ThemeComboBox.Items.Refresh(); 
            }
        }

        private void CreateNewTheme_Click(object sender, RoutedEventArgs e)
        {
            //var newTheme = new ThemeModel
            //{
            //    Name = "UserTheme", // Get user input
            //    Foreground = "Black", // Get user input
            //    Background = "White", // Get user input
            //    FontSize = 14, // Get user input
            //    BarColor = "Gray", // Get user input
            //    WindowColor = "LightGray" // Get user input
            //};

            //SaveTheme(newTheme);
            //LoadThemes(); 
        }

        private void SaveTheme(ThemeModel theme)
        {
            //string filePath = System.IO.Path.Combine(themeDirectory, $"{theme.Name}.xml");
            //XDocument doc = new XDocument(
            //    new XElement("Theme",
            //        new XElement("Name", theme.Name),
            //        new XElement("Foreground", theme.Foreground),
            //        new XElement("Background", theme.Background),
            //        new XElement("FontSize", theme.FontSize),
            //        new XElement("BarColor", theme.BarColor),
            //        new XElement("WindowColor", theme.WindowColor)
            //    ));

            //doc.Save(filePath);
        }

        private void ApplyTheme_Click(object sender, RoutedEventArgs e)
        {
            //if (ThemeComboBox.SelectedItem is ThemeModel selectedTheme)
            //{
            //    Application.Current.Resources["Foreground"] = new SolidColorBrush((Color)ColorConverter.ConvertFromString(selectedTheme.Foreground));
            //    Application.Current.Resources["Background"] = new SolidColorBrush((Color)ColorConverter.ConvertFromString(selectedTheme.Background));
            //    Application.Current.Resources["BarColor"] = new SolidColorBrush((Color)ColorConverter.ConvertFromString(selectedTheme.BarColor));
            //    Application.Current.Resources["WindowColor"] = new SolidColorBrush((Color)ColorConverter.ConvertFromString(selectedTheme.WindowColor));
            //}
        }
    }
}
