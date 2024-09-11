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
        T GetById(int key);
        ObservableCollection<T> GetAll();
        bool Add(T entity);
        bool Update(T entity);
    }
}
