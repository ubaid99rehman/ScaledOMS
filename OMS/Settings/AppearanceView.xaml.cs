using Microsoft.Extensions.DependencyInjection;
using OMS.Helpers;
using OMS.VM.Settings;
using System.Windows;
using System.Windows.Controls;

namespace OMS.Settings
{
    public partial class AppearanceView : UserControl
    {
        //Constructor
        public AppearanceView()
        {
            InitializeComponent();
            this.DataContext = AppServiceProvider.GetServiceProvider().GetRequiredService<AppThemeModel>();
        }

        //Button Click Events
        private void ApplyTheme_Click(object sender, RoutedEventArgs e)
        {
            if (DataContext is AppThemeModel model)
            {
                var theme = model.GetSelectedTheme();
                AppThemeHelper.ChangeTheme(theme);
                model.SaveAppliedTheme();
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
