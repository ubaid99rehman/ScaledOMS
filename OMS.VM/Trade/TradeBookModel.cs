using DevExpress.Mvvm;
using OMS.Core.Enums;
using OMS.Core.Models;
using System;
using System.Collections.ObjectModel;

using System.Windows.Threading;

namespace OMS.ViewModels
{
    public class TradeBookModel : ViewModelBase
    {
        private string _stock;
        public string SelectedStockSymbol
        {
            get { return _stock; }
            set
            {
                if (SetProperty(ref _stock, value, nameof(SelectedStockSymbol)))
                {

                }
            }
        }

        private ObservableCollection<TradeBook> _stockTrades;
        public ObservableCollection<TradeBook> StockTrades
        {
            get { return _stockTrades; }
            set
            {
                _stockTrades = value;
            }
        }

        public TradeBookModel()
        {
        }

        private void AddInitialTrades()
        {
           
        }

    }

}
