using DevExpress.Mvvm;
using OMS.Core.Models;
using System;
using System.Collections.ObjectModel;
using System.Windows.Threading;

namespace OMS.ViewModels
{
    public class OrderBookModel : ViewModelBase
    {
        private string _symbol;
        public string SelectedStockSymbol
        {
            get { return _symbol; }
            set 
            { 
                if(SetProperty(ref _symbol, value,nameof(Stock)))
                {
                    AddInitialTrades();
                }
            }
        }

        private ObservableCollection<OrderBook> _stockBuyingOrders;
        public ObservableCollection<OrderBook> StockBuyingOrders
        {
            get { return _stockBuyingOrders; }
            set
            {
                _stockBuyingOrders = value;
            }
        }

        private ObservableCollection<OrderBook> _stockSellingOrders;
        public ObservableCollection<OrderBook> StockSellingOrders
        {
            get { return _stockSellingOrders; }
            set
            {
                _stockSellingOrders = value;
            }
        }

        public OrderBookModel()
        {
            AddInitialTrades();

        }

        public void AddInitialTrades()
        {
           
        }

        private void OnTradeTimerTick(object sender, EventArgs e)
        {
            
        }

    }
}
