using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OMS.Core.Services.AppServices
{
    public interface IAppService<T> 
    {
        T GetById(string key);
        ObservableCollection<T> GetAll();
        bool Delete(T entity);
        bool Update(T entity);
        bool DeleteById(int id);
        bool UpdateById(int id);
    }
}
