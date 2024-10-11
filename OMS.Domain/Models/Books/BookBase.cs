using OMS.Enums;
using System;

namespace OMS.Core.Models.Books
{
    public abstract class BookBase : BaseModel, IBookBase
    {
        #region Private Members
        private string _symbol;
        private decimal _price;
        private int _quantity;
        private decimal _total;
        private DateTime _time;
        private OrderType _type; 
        #endregion

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
    }
}

