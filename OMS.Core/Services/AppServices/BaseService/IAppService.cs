using System.Collections.ObjectModel;

namespace OMS.Core.Services.AppServices
{
    public interface IAppService<T>
    {
        T GetById(int key);
        ObservableCollection<T> GetAll();
        bool Add(T entity);
        bool Update(T entity);
    }
}
