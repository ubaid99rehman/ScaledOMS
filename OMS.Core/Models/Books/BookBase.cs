using OMS.Core.Enums;
using OMS.Core.Models;
using System;
using System.ComponentModel;


namespace OMS.Core.Core.Models.Books
{
    public abstract class BookBase : BaseModel
    {
        #region Props
        private string _symbol;
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

        private decimal _price;
        public decimal Price
        {
            get => _price;
            set
            {
                if (_price != value)
                {
                    _price = (decimal)Math.Round((double)value, 3);
                    OnPropertyChanged(nameof(Price));
                }
            }
        }

        private int _quantity;
        public int Quantity
        {
            get => _quantity;
            set
            {
                if (_quantity != value)
                {
                    _quantity = value;
                    OnPropertyChanged(nameof(Quantity));
                }
            }
        }

        private decimal _total;
        public decimal Total
        {
            get => _total;
            set
            {
                if (_total != value)
                {
                    _total = (decimal)Math.Round((double)value, 3);
                    OnPropertyChanged(nameof(Total));
                }
            }
        }

        private DateTime _time;
        public DateTime Timestamp
        {
            get => _time;
            set
            {
                if (_time != value)
                {
                    _time = value;
                    OnPropertyChanged(nameof(Timestamp));
                }
            }
        }

        private OrderType _type;
        public OrderType Type
        {
            get => _type;
            set
            {
                if (_type != value)
                {
                    _type = value;
                    OnPropertyChanged(nameof(Type));
                }
            }
        } 
        #endregion

        #region Numeric Formatters
        public string GetFormattedQuantity() => FormatNumber(_quantity);
        public string GetFormattedPrice() => FormatNumber(_price);
        public string GetFormattedTotal() => FormatNumber(_total);
        #endregion
    }

}
