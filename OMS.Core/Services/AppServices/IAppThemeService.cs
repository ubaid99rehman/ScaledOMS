using OMS.Core.Models.App;
using System.Collections.Generic;

namespace OMS.Core.Services.AppServices
{
    public interface IAppThemeService
    {
        IThemeModel GetAppliedTheme();
        void SaveAppliedTheme(IThemeModel theme);
        bool SaveTheme(IThemeModel model);
        List<IThemeModel> GetThemes();
        List<string> GetThemeNames();
    }
}
