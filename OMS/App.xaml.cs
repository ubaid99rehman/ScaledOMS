using DevExpress.Xpf.Core;
using Microsoft.Extensions.DependencyInjection;

using System.Windows;

namespace OMS
{
    public partial class App : Application
    {
        public App()
        {
            CompatibilitySettings.UseLightweightThemes = true;
            ApplicationThemeHelper.Preload(PreloadCategories.Core);
        }
        
        protected override void OnStartup(StartupEventArgs e)
        {
            var mainWindow = AppServiceProvider.GetServiceProvider().GetRequiredService<LoadingWindow>();
            mainWindow.Show();
            base.OnStartup(e);
        }
    }


}
