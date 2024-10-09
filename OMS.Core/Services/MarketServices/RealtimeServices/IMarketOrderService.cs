using System;
using System.ComponentModel;

namespace OMS.Core.Services.MarketServices.RealtimeServices
{
    public interface IMarketOrderService 
    {
        event Action<string> DataUpdated;
        ICollectionView GetBuyOrders(string symbol);
        ICollectionView GetSellOrders(string symbol);
    }
}
