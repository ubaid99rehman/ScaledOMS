using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OMS.Common.Helpers
{
    public abstract class NumericValueFormatter
    {
        protected string FormatNumber(int? value)
        {
            return value?.ToString("N0");
        }

        protected string FormatNumber(decimal? value)
        {
            return value?.ToString("N3");
        }

        protected string FormatNumber(double? value)
        {
            return value?.ToString("N3");
        }
    }
}
