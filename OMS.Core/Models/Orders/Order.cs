using OMS.Enums;
using System;
using System.ComponentModel;


namespace OMS.Core.Models
{
    public class Order : BaseModel
    {

        public Order(){ _orderID = 0; }
        public int? ID { get; set; }
        public int ParentID { get; set; } = 0;
        private int? _orderID;
        private DateTime? _orderDate;
        private string _symbol;
        private OrderType? _orderType;
        private int? _quantity;
        private decimal? _price;
        private decimal? _total;
        private OrderStatus _status;
        private int? _accountID;
        private int? _addedBy;
        private DateTime? _createdDate;
        private DateTime? _lastupdatedDate;

        public int? OrderID
        {
            get => _orderID;
            set
            {
                if (_orderID != value)
                {
                    _orderID = value;
                    OnPropertyChanged(nameof(OrderID));
                }
            }
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

        public int? Quantity
        {
            get => _quantity;
            set
            {
                if (_quantity != value)
                {
                    _quantity =  value;
                    OnPropertyChanged(nameof(Quantity));
                }
            }
        }

        public decimal? Price
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

        public decimal? Total
        {
            get => _total;
            set
            {
                if (_total != value)
                {
                    _total = (decimal) Math.Round((double)value, 3);
                    OnPropertyChanged(nameof(Total));
                }
            }
        }

        public OrderType? OrderType
        {
            get => _orderType;
            set
            {
                if (_orderType != value)
                {
                    _orderType = value;
                    OnPropertyChanged(nameof(OrderType));
                }
            }
        }

        public DateTime? OrderDate
        {
            get => _orderDate;
            set
            {
                if (_orderDate != value)
                {
                    _orderDate = value;
                    OnPropertyChanged(nameof(OrderDate));
                }
            }
        }

        public OrderStatus Status
        {
            get => _status;
            set
            {
                if (_status != value)
                {
                    _status = value;
                    OnPropertyChanged(nameof(Status));
                }
            }
        }

        public int? AccountID
        {
            get => _accountID;
            set
            {
                if (_accountID != value)
                {
                    _accountID = value;
                    OnPropertyChanged(nameof(AccountID));
                }
            }
        }

        public int? AddedBy
        {
            get => _addedBy;
            set
            {
                if (_addedBy != value)
                {
                    _addedBy = value;
                    OnPropertyChanged(nameof(AddedBy));
                }
            }
        }

        public DateTime? CreatedDate
        {
            get => _createdDate;
            set
            {
                if (_createdDate != value)
                {
                    _createdDate = value;
                    OnPropertyChanged(nameof(CreatedDate));
                }
            }
        }
                
        public DateTime? LasUpdatedDate
        {
            get => _lastupdatedDate;
            set
            {
                if (_lastupdatedDate != value)
                {
                    _lastupdatedDate = value;
                    OnPropertyChanged(nameof(LasUpdatedDate));
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
        public string GetFormattedQuantity() => FormatNumber(_quantity);
        public string GetFormattedPrice() => FormatNumber(_price);
        public string GetFormattedTotal() => FormatNumber(_total);
        #endregion
    }
}
