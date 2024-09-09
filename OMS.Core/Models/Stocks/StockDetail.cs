using OMS.Common.Helpers;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OMS.Core.Models
{
    public class StockDetail : NumericValueFormatter,INotifyPropertyChanged
    {
        private string _symbol;
        private decimal _lastPrice;
        private decimal _volume24H;
        private decimal _change24H;
        private decimal _high24H;
        private decimal _low24H;
        
        public string Symbol
        {
            get => _symbol;
            set
            {
                if (_symbol != value)
                {
                    _symbol = value;
                    OnPropertyChanged(nameof(Symbol));
                }
            }
        }

        public decimal LastPrice
        {
            get => _lastPrice;
            set
            {
                if (_lastPrice != value)
                {
                    _lastPrice = Math.Round(value, 3);
                    OnPropertyChanged(nameof(LastPrice));
                }
            }
        }

        public decimal Volume24H
        {
            get => _volume24H;
            set
            {
                if (_volume24H != value)
                {
                    _volume24H = Math.Round(value, 3);
                    OnPropertyChanged(nameof(Volume24H));
                }
            }
        }
        
        public decimal Change24H
        {
            get => _change24H;
            set
            {
                if (_change24H != value)
                {
                    _change24H = Math.Round(value, 3);
                    OnPropertyChanged(nameof(Change24H));
                }
            }
        }

        public decimal Low24H
        {
            get => _low24H;
            set
            {
                if (_low24H != value)
                {
                    _low24H = Math.Round(value, 3);
                    OnPropertyChanged(nameof(Low24H));
                }
            }
        }

        public decimal High24H
        {
            get => _high24H;
            set
            {
                if (_high24H != value)
                {
                    _high24H = Math.Round(value, 3);
                    OnPropertyChanged(nameof(High24H));
                }
            }
        }

        public StockDetail(int value)
        {
            Symbol = "-";
            LastPrice = value; 
            Volume24H = value; 
            Change24H = value;
            High24H = value;
            Low24H = value; 
        }
        public StockDetail()
        {

        }

        #region Event Handler
        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

            switch (propertyName)
            {
                case nameof(LastPrice):
                    OnPropertyChanged(nameof(FormattedLastPrice));
                    break;
                case nameof(Volume24H):
                    OnPropertyChanged(nameof(FormattedVolume24H));
                    break;
                case nameof(Low24H):
                    OnPropertyChanged(nameof(FormattedLow24H));
                    break;
                case nameof(High24H):
                    OnPropertyChanged(nameof(FormattedHigh24H));
                    break;
            }
        }
        #endregion


        #region Numeric Formatters
        public string FormattedLastPrice => FormatNumber(_lastPrice);
        public string FormattedVolume24H => FormatNumber(_volume24H);
        public string FormattedLow24H => FormatNumber(_low24H);
        public string FormattedHigh24H => FormatNumber(_high24H);
        #endregion

    }
}
