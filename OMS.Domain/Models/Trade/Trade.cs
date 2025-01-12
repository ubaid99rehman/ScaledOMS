﻿using OMS.Core.Models.Account;
using OMS.Core.Models.Orders;
using OMS.Enums;
using System;
namespace OMS.Core.Models.Trade
{
    public class Trade : BaseModel, ITrade
    {
        #region Private Members
        private int _tradeID;
        private DateTime _tradeDate;
        private string _symbol;
        private string _accountID;
        private decimal _price;
        private int _quantity;
        private decimal _total;
        public int _orderID;
        public IAccount _accounts;
        public OrderType _order_Types;
        public IOrder _orders;
        int _tradeType;
        #endregion

        public int TradeID 
        { 
            get => _tradeID;
            set
            {
                SetProperty(ref _tradeID, value);
            }
        }
        public DateTime TradeDate
        {
            get => _tradeDate;
            set
            {
                SetProperty(ref _tradeDate, value);
            }
        }
        public string Symbol
        {
            get { return _symbol; }
            set
            {
                SetProperty(ref _symbol, value);
            }
        }
        public string AccountID
        {
            get { return _accountID; }
            set
            {
                SetProperty(ref _accountID, value);
            }
        }
        public decimal Price
        {
            get { return _price; }
            set
            {
                SetProperty(ref _price, value);
            }
        }
        public int Quantity
        {
            get { return _quantity; }
            set
            {
                SetProperty(ref _quantity, value);
            }
        }
        public decimal Total
        {
            get { return _total; }
            set
            {
                SetProperty(ref _total, value);
            }
        }
        public int OrderID
        {
            get => _orderID;
            set
            {
                SetProperty(ref _orderID, value);
            }
        }
        public IAccount Accounts
        {
            get => _accounts;
            set
            {
                SetProperty(ref _accounts, value);
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
        public IOrder Orders
        {
            get => _orders;
            set
            {
                SetProperty(ref _orders, value);
            }
        }
        public int TradeType
        {
            get => _tradeType;
            set
            {
                SetProperty(ref _tradeType, value);
            }
        }
    }
}
