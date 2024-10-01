
namespace OMS.Core.Models.Stocks
{
    public interface IStock
    {
        int ID { get; set; }
        string Symbol { get; set; }
        string Name { get; set; }
        decimal LastPrice { get; set; }
    }

}
