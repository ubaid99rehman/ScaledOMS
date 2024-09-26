using DevExpress.Xpf.Core;
using Microsoft.Extensions.DependencyInjection;
using OMS.Core.Services.AppServices;
using OMS.Helpers;
using System.Windows;

namespace OMS
{
    public partial class App : Application
    {
        public App()
        {
            CompatibilitySettings.UseLightweightThemes = true;
            CompatibilitySettings.AllowThemePreload = true;
            ApplicationThemeHelper.Preload(PreloadCategories.Core,
                PreloadCategories.Charts,
                PreloadCategories.Controls,
                PreloadCategories.Docking,
                PreloadCategories.Grid,
                PreloadCategories.LayoutControl,
                PreloadCategories.Ribbon);
        }
        
        protected override void OnStartup(StartupEventArgs e)
        {
            //Applying Theme
            var themeService = AppServiceProvider.GetServiceProvider().GetRequiredService<IAppThemeService>();
            var theme = themeService.GetAppliedTheme();
            if (theme != null)
            {
                AppThemeHelper.ChangeTheme(theme);
            }

            ThemeManager.ApplicationThemeName = Theme.DeepBlueFullName;
            //Displaying Window
            var mainWindow = AppServiceProvider.GetServiceProvider().GetRequiredService<MainWindow>();
            mainWindow.Show();

            base.OnStartup(e);
        }
    }


}
