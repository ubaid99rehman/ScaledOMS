using System.Threading.Tasks;

namespace OMS
{
    public interface IBootStrapper
    {
        Task LoadData();
        Task LoadOrdersData();
        Task LoadStocksData();
        Task LoadAccountData();
    }
}