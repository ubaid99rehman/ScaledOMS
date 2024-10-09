using System.Collections.ObjectModel;


namespace OMS.Core.Services
{
    public interface IReadonlyService<T>
    {
        T GetById(int key);
        ObservableCollection<T> GetAll();
    }
}
