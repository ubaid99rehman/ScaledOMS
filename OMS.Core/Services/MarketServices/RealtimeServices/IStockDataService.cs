using DevExpress.Xpf.Core;
using OMS.Core.Enums;
using OMS.Core.Models;
using OMS.Core.Models.Stocks;
using System;
using System.Collections.ObjectModel;

namespace OMS.Core.Services.MarketServices.RealtimeServices
{
    public interface IStockDataService : IRealtimeService
    {
        event Action DataUpdated;
        Stock GetStock(string symbol);
        ObservableCollection<Stock> GetStocks();
        
        StockDetail GetStockDetail(string symbol);
        ObservableCollection<StockDetail> GetStockDetails();

        ObservableCollection<string> GetStockSymbols();
    }

}
