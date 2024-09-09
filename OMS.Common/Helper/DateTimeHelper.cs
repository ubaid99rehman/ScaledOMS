using DevExpress.Xpf.Charts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OMS.Common.Helpers
{
    public static class DateTimeHelper
    {
        public static TimeSpan ConvertInterval(ChartIntervalItem interval, int intervalsCount)
        {
            return GetInterval(interval.MeasureUnit, interval.MeasureUnitMultiplier * intervalsCount);
        }

        public static DateTime GetInitialDate(ChartIntervalItem interval)
        {
            DateTime now = DateTime.Now;
            switch (interval.MeasureUnit)
            {
                case DateTimeMeasureUnit.Second:
                    DateTime roundSeconds = now.AddMilliseconds(-now.Millisecond);
                    return roundSeconds.AddSeconds(-roundSeconds.Second % interval.MeasureUnitMultiplier);
                case DateTimeMeasureUnit.Minute:
                    return now.Date.AddHours(now.Hour).AddMinutes(now.Minute - now.Minute % interval.MeasureUnitMultiplier);
                case DateTimeMeasureUnit.Hour:
                    return now.Date.AddHours(now.Hour - now.Hour % interval.MeasureUnitMultiplier);
                case DateTimeMeasureUnit.Day:
                    return now.Date;
                case DateTimeMeasureUnit.Week:
                    return now.Date.AddDays(-(7 + (now.DayOfWeek - DayOfWeek.Monday)) % 7);
                case DateTimeMeasureUnit.Month:
                    return new DateTime(now.Year, now.Month, 1);
            }
            return DateTime.Now;
        }
        public static TimeSpan GetInterval(DateTimeMeasureUnit measureUnit, int multiplier)
        {
            switch (measureUnit)
            {
                case DateTimeMeasureUnit.Second:
                    return TimeSpan.FromSeconds(multiplier);
                case DateTimeMeasureUnit.Minute:
                    return TimeSpan.FromMinutes(multiplier);
                case DateTimeMeasureUnit.Hour:
                    return TimeSpan.FromHours(multiplier);
                case DateTimeMeasureUnit.Day:
                    return TimeSpan.FromDays(multiplier);
                case DateTimeMeasureUnit.Week:
                    return TimeSpan.FromDays(multiplier * 7);
                case DateTimeMeasureUnit.Month:
                    return TimeSpan.FromDays(multiplier * 30);
            }
            return TimeSpan.Zero;
        }
    }
}
