using OMS.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;

namespace OMS.Core.Models.Orders
{
    public interface IOrder
    {
        int? OrderID { get; set; }
        string Symbol { get; set; }
        int? Quantity { get; set; }
        decimal? Price { get; set; }
        decimal? Total { get; set; }
        OrderType? OrderType { get; set; }
        DateTime? OrderDate { get; set; }
        OrderStatus Status { get; set; }
        int? AccountID { get; set; }
        int? AddedBy { get; set; }
        DateTime? CreatedDate { get; set; }
        DateTime? LasUpdatedDate { get; set; }
        //NumericFormatted Members
        string FormattedQuantity { get;}
        string FormattedPrice { get;}
        string FormattedTotal{ get; }
    }
}
