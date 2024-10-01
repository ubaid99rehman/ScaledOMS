using System.Configuration;

namespace OMS.Helpers
{
    public static class DbHelper
    {
        public readonly static string Connection = ConfigurationManager.ConnectionStrings["OMS"].ConnectionString;
    }
}
