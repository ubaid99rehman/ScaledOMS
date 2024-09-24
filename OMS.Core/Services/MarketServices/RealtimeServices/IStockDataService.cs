using OMS.Core.Models;
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
