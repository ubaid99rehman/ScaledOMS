using System;
using System.ComponentModel;

namespace OMS.Core.Models.Stocks
{
    public class StockTradingData : INotifyPropertyChanged
    {


        //private string _symbol;
        private decimal _volume;
        private decimal _high;
        private decimal _low;
        private decimal _open;
        private decimal _close;
        private DateTime _recordedTime;



        public StockTradingData(DateTime date, decimal open, decimal high, decimal low, decimal close, decimal volume)
        {
            RecordedTime= date;
            Open= open;
            High = high;
            Low= low;
            Close= close;
            Volume= volume;
        }

        public StockTradingData() { }

        //public string Symbol
        //{
        //    get => _symbol;
        //    set
        //    {
        //        if (_symbol != value)
        //        {
        //            _symbol = value;
        //            OnPropertyChanged(nameof(Symbol));
        //        }
        //    }
        //}

        public decimal Volume
        {
            get => _volume;
            set
            {
                if (_volume != value)
                {
                    _volume = Math.Round(value, 3);
                    OnPropertyChanged(nameof(Volume));
                }
            }
        }

        public decimal High
        {
            get => _high;
            set
            {
                if (_high != value)
                {
                    _high = Math.Round(value, 3);
                    OnPropertyChanged(nameof(High));
                }
            }
        }

        public decimal Low
        {
            get => _low;
            set
            {
                if (_low != value)
                {
                    _low = Math.Round(value, 3);
                    OnPropertyChanged(nameof(Low));
                }
            }
        }

        public decimal Open
        {
            get => _open;
            set
            {
                if (_open != value)
                {
                    _open = Math.Round(value, 3);
                    OnPropertyChanged(nameof(Open));
                }
            }
        }

        public decimal Close
        {
            get => _close;
            set
            {
                if (_close != value)
                {
                    _close = Math.Round(value, 3);
                    OnPropertyChanged(nameof(Close));
                }
            }
        }

        public DateTime RecordedTime
        {
            get => _recordedTime;
            set
            {
                if (_recordedTime != value)
                {
                    _recordedTime =value;
                    OnPropertyChanged(nameof(RecordedTime));
                }
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
