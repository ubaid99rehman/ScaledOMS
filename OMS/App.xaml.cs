using DevExpress.Xpf.Core;
using Microsoft.Extensions.DependencyInjection;
using OMS.Core.Services.AppServices;
using OMS.Helpers;
using System.Windows;

namespace OMS
{
    public partial class App : Application
    {
        //Constructor
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

        #region Methods
        //Startup Method
        protected override void OnStartup(StartupEventArgs e)
        {
            ApplyTheme();
            LoadMainWindow();
            base.OnStartup(e);
        }
        private void ApplyTheme()
        {
            //Applying Theme
            var themeService = AppServiceProvider.GetServiceProvider().GetRequiredService<IAppThemeService>();
            var theme = themeService.GetAppliedTheme();
            if (theme != null)
            {
                AppThemeHelper.ChangeTheme(theme);
            }
        }
        private void LoadMainWindow()
        {
            var mainWindow = AppServiceProvider.GetServiceProvider().GetRequiredService<LoadingWindow>();
            mainWindow.Show();
        }
        #endregion
    }
}