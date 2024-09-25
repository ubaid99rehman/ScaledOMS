using Microsoft.Extensions.DependencyInjection;
using OMS.Core.Models.Themes;
using OMS.Core.Services.AppServices;
using OMS.Helpers;
using OMS.Services;
using OMS.VM.Settings;
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
            this.DataContext = AppServiceProvider.GetServiceProvider().GetRequiredService<AppThemeModel>();
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

        }
        
        private void CreateNewTheme_Click(object sender, RoutedEventArgs e)
        {
           
        }

        private void SaveTheme(ThemeModel theme)
        {

        }

        private void ApplyTheme_Click(object sender, RoutedEventArgs e)
        {
            if(DataContext is AppThemeModel model)
            {
                var theme = model.GetSelectedTheme();
                AppThemeHelper.ChangeTheme(theme);
                model.SaveAppliedTheme();
            }
        }

        private void TextBackground_ColorChanged(object sender, RoutedEventArgs e)
        {
            var color = TextBackground.Color;
            var themeColor = ((AppThemeModel)DataContext).SelectedTheme.TextBackground;
            if (color != null)
            {
                ((AppThemeModel)DataContext).SelectedTheme.TextBackground = new System.Windows.Media.SolidColorBrush(color);
            }
        }

        private void Add_Theme_Click(object sender, RoutedEventArgs e)
        {
            if (DataContext is AppThemeModel model)
            {
                bool isAdded = model.SaveTheme(out string message);
                if(isAdded)
                {
                    MessageBox.Show(message,"Theme Added",MessageBoxButton.OK, MessageBoxImage.Information);
                }
                else
                {
                    MessageBox.Show("Error while Adding theme. "+message,"Error",MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }
    }
}
