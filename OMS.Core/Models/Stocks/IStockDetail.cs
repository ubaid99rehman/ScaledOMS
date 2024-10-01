
namespace OMS.Core.Models.Stocks
{
    public interface IStockDetail : IStock
    {
        decimal Volume24H { get; set; }
        decimal Change24H { get; set; }
        decimal High24H { get; set; }
        decimal Low24H { get; set; }
        string FormattedLastPrice { get; }
        string FormattedChange {  get; }
        string FormattedVolume { get; }
        string FormattedHigh {  get; }
        string FormattedLow { get; }
    }
}
