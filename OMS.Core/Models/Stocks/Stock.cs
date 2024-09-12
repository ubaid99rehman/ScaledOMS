using OMS.Core.Models.Stocks;
using System;

namespace OMS.Core.Models
{
    public class Stock : BaseModel, IStock
    {
        private int _id;
        private string _symbol;
        private string _name;
        private decimal _lastPrice;

        public int ID
        {
            get => _id;
            set => SetProperty(ref _id, value);
        }

        public string Symbol
        {
            get => _symbol;
            set => SetProperty(ref _symbol, value);
        }

        public string Name
        {
            get => _name;
            set => SetProperty(ref _name, value);
        }

        public decimal LastPrice
        {
            get => _lastPrice;
            set => SetProperty(ref _lastPrice, Math.Round(value, 3));
        }
    }

}
