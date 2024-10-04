using OMS.Core.Models.Account;
using OMS.Core.Models.Orders;
using OMS.Enums;
using System;

namespace OMS.Core.Models.Trade
{
    //Orders Related Trade
    public interface ITrade
    {
        int TradeID { get; set; }
        int OrderID { get; set; }
        int TradeType { get; set; }
        int Quantity { get; set; }
        decimal Price { get; set; }
        decimal Total { get; set; }
        string AccountID { get; set; }
        DateTime TradeDate { get; set; }
        string Symbol { get; set; }

        IAccount Accounts { get; set; }
        OrderType Order_Types { get; set; }
        IOrder Orders { get; set; }
    }
}
