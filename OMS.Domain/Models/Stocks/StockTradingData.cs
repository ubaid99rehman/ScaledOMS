using System;
using System.ComponentModel;

namespace OMS.Core.Models.Stocks
{
    public class StockTradingData : BaseModel, IStockTradingData
    {
        #region Private Members
        private decimal _volume;
        private decimal _high;
        private decimal _low;
        private decimal _open;
        private decimal _close;
        private DateTime _recordedTime; 
        #endregion

        public decimal Volume
        {
            get => _volume;
            set => SetProperty(ref _volume, Math.Round(value, 3));
        }
        public decimal High
        {
            get => _high;
            set => SetProperty(ref _high, Math.Round(value, 3));
        }
        public decimal Low
        {
            get => _low;
            set => SetProperty(ref _low, Math.Round(value, 3));
        }
        public decimal Open
        {
            get => _open;
            set => SetProperty(ref _open, Math.Round(value, 3));
        }
        public decimal Close
        {
            get => _close;
            set => SetProperty(ref _close, Math.Round(value, 3));
        }
        public DateTime RecordedTime
        {
            get => _recordedTime;
            set => SetProperty(ref _recordedTime, value);
        }

        #region Numeric Formatted Members
        public string FormattedVolume => FormatNumber(Volume);
        public string FormattedHigh => FormatNumber(High);
        public string FormattedLow => FormatNumber(Low);
        public string FormattedOpen => FormatNumber(Open);
        public string FormattedClose => FormatNumber(Close);
        #endregion

    }


}
