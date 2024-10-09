using System;
using System.ComponentModel;


namespace OMS.Core.Services.MarketServices.RealtimeServices
{
    public interface IMarketTradeService 
    {
        event Action<string> DataUpdated;
        ICollectionView GetAll(string symbol);
    }
}
