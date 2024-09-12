using OMS.Core.Models;
using System;
using System.Collections.ObjectModel;


namespace OMS.Core.Services.MarketServices.RealtimeServices
{
    public interface IMarketOrderService :  IMarketService<OrderBook>, IRealtimeService
    {

        event Action<string> DataUpdated;
        ObservableCollection<OrderBook> GetBuyOrders(string symbol);
        ObservableCollection<OrderBook> GetSellOrders(string symbol);
    }
}
