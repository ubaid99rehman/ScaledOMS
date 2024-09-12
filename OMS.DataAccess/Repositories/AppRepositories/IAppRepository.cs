using OMS.Core.Models;
using System.Collections.Generic;

namespace OMS.DataAccess.Repositories.AppRepositories
{
    public interface IAppRepository<T> where T : class
    {
        IEnumerable<T> GetAll();
        T GetById(int id);
        bool Add(T entity);
        bool Delete(T entity);
        bool Update(T entity);
    }

    public interface IOrderRepository : IAppRepository<Order>
    {

    }
}
