using DevExpress.Xpf.Core;
using Microsoft.Extensions.DependencyInjection;

using System.Windows;

namespace OMS
{
    public partial class App : Application
    {
        public App()
        {
            CompatibilitySettings.AllowThemePreload = true;
            CompatibilitySettings.UseLightweightThemes = true;
            ApplicationThemeHelper.ApplicationThemeName = Theme.Office2013DarkGrayFullName;
            ApplicationThemeHelper.Preload(PreloadCategories.Charts,
                PreloadCategories.Controls,
                PreloadCategories.Docking,
                PreloadCategories.Grid,
                PreloadCategories.LayoutControl,
                PreloadCategories.Ribbon);
        }
        
        protected override void OnStartup(StartupEventArgs e)
        {
            var mainWindow = AppServiceProvider.GetServiceProvider().GetRequiredService<MainWindow>();
            mainWindow.Show();
            base.OnStartup(e);
        }
    }


}
