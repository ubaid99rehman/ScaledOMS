using System.Configuration;

namespace OMS.Common.Helper
{
    public static class ConfigurationHelper
    {
        public static string GetAppSetting(string key)
        {
            return ConfigurationManager.AppSettings[key];
        }
    }
}
