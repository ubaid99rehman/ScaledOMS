namespace OMS
{
    public interface IBootStrapper
    {
        void LoadData();
        void LoadOrdersData();
        void LoadStocksData();
        void LoadAccountData();
    }
}