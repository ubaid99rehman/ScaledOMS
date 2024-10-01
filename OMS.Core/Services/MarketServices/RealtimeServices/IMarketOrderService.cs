using OMS.Core.Core.Models.Books;
using System;
using System.Collections.ObjectModel;

namespace OMS.Core.Services.MarketServices.RealtimeServices
{
    public interface IMarketOrderService :  IMarketService<BookBase>, IRealtimeService
    {
        event Action<string> DataUpdated;
        ObservableCollection<BookBase> GetBuyOrders(string symbol);
        ObservableCollection<BookBase> GetSellOrders(string symbol);
    }
}
