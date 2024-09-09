using System.Collections.ObjectModel;


namespace OMS.Core.Services
{
    public interface IMarketService<T>
    {
        T GetById(string key);
        ObservableCollection<T> GetAll();
    }
}
