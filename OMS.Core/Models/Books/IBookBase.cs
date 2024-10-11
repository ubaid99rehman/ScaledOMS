using OMS.Enums;
using System;

namespace OMS.Core.Models.Books
{
    public interface IBookBase
    {
        string Symbol { get; set; }
        int Quantity { get; set; }
        decimal Price { get; set; }
        decimal Total { get; set; }
        DateTime Timestamp { get; set; }
        OrderType Type { get; set; }
    }
}
