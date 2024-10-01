using System.Collections.ObjectModel;


namespace OMS.Core.Services
{
    public interface IMarketService<T>
    {
        T GetById(int key);
        ObservableCollection<T> GetAll();
    }
}
