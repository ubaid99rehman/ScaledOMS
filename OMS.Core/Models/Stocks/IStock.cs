using System;

namespace OMS.Core.Models.Stocks
{
    public interface IStock
    {
        int ID { get; set; }
        string Name { get; set; }
        string Symbol { get; set; }
        DateTime AddedDate { get; set; }
        decimal LastPrice { get; set; }
    }
}