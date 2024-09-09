using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OMS.Helpers
{
    public static class DbHelper
    {
        public readonly static string Connection = ConfigurationManager.ConnectionStrings["OMS"].ConnectionString;

    }
}
