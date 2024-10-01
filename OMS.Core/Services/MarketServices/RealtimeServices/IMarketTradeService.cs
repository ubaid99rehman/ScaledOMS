using OMS.Core.Core.Models.Books;
using System;
using System.Collections.ObjectModel;


namespace OMS.Core.Services.MarketServices.RealtimeServices
{
    public interface IMarketTradeService :IMarketService<BookBase>, IRealtimeService
    {
        event Action<string> DataUpdated;
        ObservableCollection<BookBase> GetAllBySymbol(string symbol);
    }
}
