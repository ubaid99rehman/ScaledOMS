using OMS.Core.Models.Account;
using OMS.Core.Models.Orders;
using OMS.Core.Models.Trade;
using OMS.Enums;
using System;
using System.Collections.Generic;

namespace OMS.Core.Models
{
    public class Order : BaseModel, IOrder
    {
        #region Private Members
        public int? ID { get; set; }
        public int ParentID { get; set; } = 0;
        private int? _orderID;
        private DateTime? _orderDate;
        private string _symbol;
        private int _orderType;
        private int? _quantity;
        private decimal? _price;
        private decimal? _total;
        private int _status;
        private int? _accountID;
        private int? _addedBy;
        private DateTime _createdDate;
        private DateTime _lastupdatedDate;
        private DateTime _expirationDate;
        private Guid _orderGuid;
        private IAccount _account;
        private OrderStatus _order_Statuses;
        private OrderType _order_Types;
        private ICollection<ITrade> _trades;
        #endregion

        //Constructor
        public Order() { _orderID = -1; }

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
                    _quantity = value;
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
                    _total = (decimal)Math.Round((double)value, 3);
                    OnPropertyChanged(nameof(Total));
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
        public DateTime CreatedDate
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
        public DateTime LasUpdatedDate
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
        public DateTime ExpirationDate
        {
            get => _expirationDate;
            set
            {
                SetProperty(ref _expirationDate, value);
            }
        }
        public Guid OrderGuid
        {
            get => _orderGuid;
            set
            {
                if (_orderGuid != value)
                {
                    SetProperty(ref _orderGuid, value);
                }
            }
        }
        public int OrderType
        {
            get => _orderType;
            set
            {
                SetProperty(ref _orderType, value);
            }
        }
        public int Status
        {
            get => _status;
            set
            {
                if (_status != value)
                {
                    SetProperty(ref _status, value);
                }
            }
        }
        public IAccount Account
        {
            get { return _account; }
            set
            {
                SetProperty(ref _account, value);
            }
        }
        public OrderStatus Order_Statuses
        {
            get { return _order_Statuses; }
            set
            {
                SetProperty(ref _order_Statuses, value);
            }
        }
        public OrderType Order_Types
        {
            get => _order_Types;
            set
            {
                SetProperty(ref _order_Types, value);
            }

        }
        public ICollection<ITrade> Trades
        {
            get => _trades;
            set
            {
                SetProperty(ref _trades, value);
            }
        }
        
        public string FormattedQuantity => FormatNumber(_quantity);
        public string FormattedPrice => FormatNumber(_price);
        public string FormattedTotal => FormatNumber(_total);
    }
}
