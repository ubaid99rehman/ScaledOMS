using System;

namespace OMS.Core.Models.Trade
{
    //Demo Data Trade
    public interface IStockTrade
    {
        int TradeID { get; set; }
        DateTime TradeDate { get; set; }
        string Symbol { get; set; }
        string TradeType { get; set; }
        string AccountID { get; set; }
        decimal Price { get; set; }
        int Quantity { get; set; }
        decimal Total { get; set; }
    }
}
