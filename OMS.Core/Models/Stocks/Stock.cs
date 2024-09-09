using OMS.Common.Helpers;
using System;
using System.ComponentModel;

namespace OMS.Core.Models
{
    public class Stock : NumericValueFormatter,INotifyPropertyChanged
    {
        private int _id;
        private string _symbol;
        private string _name;
        private decimal _lastPrice;

        public int ID
        {
            get => _id;
            set => _id = value;
        }

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

        public string Name
        {
            get => _name;
            set
            {
                if (_name != value)
                {
                    _name = value;
                    OnPropertyChanged(nameof(Name));
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

        #region Event Handler
        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion

        #region Numeric Formatters
        public string GetFormattedPrice() => FormatNumber(_lastPrice);
        #endregion

    }
}
