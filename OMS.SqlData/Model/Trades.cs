//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace OMS.SqlData.Model
{
    using System;
    using System.Collections.Generic;
    
    public partial class Trades
    {
        public int TradeID { get; set; }
        public int OrderID { get; set; }
        public int TradeType { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
        public decimal Total { get; set; }
        public int AccountID { get; set; }
        public System.DateTime TradeDate { get; set; }
        public string Symbol { get; set; }
    
        public virtual Accounts Accounts { get; set; }
        public virtual Order_Types Order_Types { get; set; }
        public virtual Orders Orders { get; set; }
    }
}
