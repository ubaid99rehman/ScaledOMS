using System;

namespace OMS.Core.Models
{
    public interface IBaseModel
    {
        string FormatNumber(int? value);
        string FormatNumber(decimal? value);
        string FormatNumber(double? value);
        string FormatDate(DateTime date);
        string FormatTime(DateTime date);
        string FormatTimeStamp(DateTime date);
    }
}
