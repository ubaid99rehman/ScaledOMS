using System.Collections.Generic;

namespace OMS.DataAccess.Repositories.AppRepositories
{
    public interface IAppReadRepository<T> where T : class
    {
        IEnumerable<T> GetAll();
        T GetById(int id);
    }
}
