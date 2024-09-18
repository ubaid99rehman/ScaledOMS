using System;

namespace OMS.Core.Models
{
    public class Trade
    {
        public int TradeID { get; set; }
        public DateTime TradeDate { get; set; }
        public string Symbol { get; set; }
        public string TradeType { get; set; } 
        public string AccountID { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }
        public decimal Total => Price * Quantity;
    }
}
