using OMS.Core.Models.Trade;
using OMS.Core.Models;
using System;

namespace OMS.Domain.Models.Trade
{
    public class StockTrade : BaseModel, IStockTrade
    {
        #region Private Members
        private int _tradeID;
        private DateTime _tradeDate;
        private string _symbol;
        private string _tradeType;
        private string _accountID;
        private decimal _price;
        private int _quantity;
        private decimal _total;
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
        public string TradeType
        {
            get { return _tradeType; }
            set
            {
                SetProperty(ref _tradeType, value);
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
    }
}
