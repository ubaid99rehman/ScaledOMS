using OMS.Core.Models.Stocks;
using System;
using System.Collections.ObjectModel;

namespace OMS.Core.Services.MarketServices.RealtimeServices
{
    public interface IStockDataService
    {
        event Action DataUpdated;
        IStock GetStock(string symbol);
        ObservableCollection<IStock> GetStocks();
        
        IStockDetail GetStockDetail(string symbol);
        ObservableCollection<IStockDetail> GetStockDetails();

        ObservableCollection<string> GetStockSymbols();
    }

}
