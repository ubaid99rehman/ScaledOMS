using DevExpress.Xpf.Charts;
using OMS.Enums;
using System;

namespace OMS.Common.Helper
{
    public static class DateTimeHelper
    {
        //Generate Start Date For Given Time Period
        public static DateTime GetStartTime(TradeTimeInterval interval, int multiplier, DateTime fromTime)
        {
            if(fromTime == null)
            {
                fromTime = DateTime.Now;
            }
            switch (interval)
            {
                case TradeTimeInterval.Minute: return fromTime.AddMinutes(-multiplier);
                case TradeTimeInterval.Hour: return fromTime.AddHours(-multiplier);
                case TradeTimeInterval.Day: return fromTime.AddDays(-multiplier);
                case TradeTimeInterval.Month: return fromTime.AddMonths(-multiplier);
                case TradeTimeInterval.Year: return fromTime.AddYears(-multiplier);
                default: throw new ArgumentException("Unsupported interval");
            }
        }
        public static TimeSpan GetTimeSpanForInterval(TradeTimeInterval interval)
        {
            switch (interval)
            {
                case TradeTimeInterval.Minute: return TimeSpan.FromMinutes(1);
                case TradeTimeInterval.Hour: return TimeSpan.FromHours(1);
                case TradeTimeInterval.Day: return TimeSpan.FromDays(1);
                case TradeTimeInterval.Month: return TimeSpan.FromDays(30); 
                case TradeTimeInterval.Year: return TimeSpan.FromDays(365); 
                default: return TimeSpan.FromMinutes(1);
            }
        }
        //Devexpress
        public static TimeSpan GetTimeSpan(ChartIntervalItem interval, int intervalsCount)
        {
            return GetTimeSpan(interval.MeasureUnit, interval.MeasureUnitMultiplier * intervalsCount);
        }
        public static TimeSpan GetTimeSpan(DateTimeMeasureUnit measureUnit, int multiplier)
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
