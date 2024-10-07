using OMS.Core.Models.Account;
using OMS.Core.Models.Trade;
using OMS.Enums;
using System;
using System.Collections.Generic;

namespace OMS.Core.Models.Orders
{
    public interface IOrder
    {
        int? OrderID { get; set; }
        DateTime? OrderDate { get; set; }
        int OrderType { get; set; }
        int? Quantity { get; set; }
        decimal? Price { get; set; }
        decimal? Total { get; set; }
        int Status { get; set; }
        int? AccountID { get; set; }
        DateTime ExpirationDate { get; set; }
        DateTime LasUpdatedDate { get; set; }
        DateTime CreatedDate { get; set; }
        int? AddedBy { get; set; }
        string Symbol { get; set; }
        Guid OrderGuid { get; set; }

        IAccount Account { get; set; }
        OrderStatus Order_Statuses { get; set; }
        OrderType Order_Types { get; set; }
        ICollection<ITrade> Trades { get; set; }

        //NumericFormatted Members
        string FormattedQuantity { get;}
        string FormattedPrice { get;}
        string FormattedTotal{ get; }
    }
}
