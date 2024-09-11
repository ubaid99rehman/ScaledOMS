using OMS.Common.Helpers;
using OMS.Core.Models.Stocks;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OMS.Core.Models
{
    public class StockDetail : BaseModel, IStock
    {
        private int _id;
        private string _symbol;
        private string _name;
        private decimal _lastPrice;
        private decimal _volume24H;
        private decimal _change24H;
        private decimal _high24H;
        private decimal _low24H;

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

        public decimal Volume24H
        {
            get => _volume24H;
            set => SetProperty(ref _volume24H, Math.Round(value, 3));
        }

        public decimal Change24H
        {
            get => _change24H;
            set => SetProperty(ref _change24H, Math.Round(value, 3));
        }

        public decimal High24H
        {
            get => _high24H;
            set => SetProperty(ref _high24H, Math.Round(value, 3));
        }

        public decimal Low24H
        {
            get => _low24H;
            set => SetProperty(ref _low24H, Math.Round(value, 3));
        }

        public string FormattedLastPrice => FormatNumber(LastPrice);
        public string FormattedChange => FormatNumber(Change24H);
        public string FormattedVolume => FormatNumber(Volume24H);
        public string FormattedHigh => FormatNumber(High24H);
        public string FormattedLow => FormatNumber(Low24H);
    }

}
