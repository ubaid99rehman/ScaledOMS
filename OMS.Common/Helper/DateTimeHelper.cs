using OMS.Common.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OMS.Common.Helper
{
    public static class DateTimeHelper
    {
        public static DateTime GetStartTime(TimePeriod period, int multiplier)
        {
            switch (period)
            {
                case TimePeriod.Minutes: return DateTime.Now.AddMinutes(multiplier);   
                case TimePeriod.Hours: return DateTime.Now.AddHours(multiplier);       
                case TimePeriod.Days: return DateTime.Now.AddDays(multiplier);         
                case TimePeriod.Month: return DateTime.Now.AddMonths(multiplier);      
                case TimePeriod.Years: return DateTime.Now.AddYears(multiplier);       
                case TimePeriod.Max: return new DateTime(2000, 1, 1);           
                default: return DateTime.Now;
            }
        }

        public static TimeSpan GetTimeSpanForInterval(TimeInterval interval)
        {
            switch (interval)
            {
                case TimeInterval.Minute: return TimeSpan.FromMinutes(1);
                case TimeInterval.Hour: return TimeSpan.FromHours(1);
                case TimeInterval.Day: return TimeSpan.FromDays(1);
                case TimeInterval.Month: return TimeSpan.FromDays(30); 
                case TimeInterval.Year: return TimeSpan.FromDays(365); 
                default: return TimeSpan.FromMinutes(1);
            }
        }

    }
}
