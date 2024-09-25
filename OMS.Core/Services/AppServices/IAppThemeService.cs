using OMS.Core.Models.Themes;
using System.Collections.Generic;

namespace OMS.Core.Services.AppServices
{
    public interface IAppThemeService
    {
        ThemeModel GetAppliedTheme();

        void SaveAppliedTheme(ThemeModel theme);

        bool SaveTheme(ThemeModel model);

        List<ThemeModel> GetThemes();

        List<string> GetThemeNames();
    }
}
