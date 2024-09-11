using OMS.Core.Core.Models.User;
using OMS.Core.Models;
using OMS.Core.Models.Account;
using System.Collections.Generic;

namespace OMS.DataAccess.Repositories
{
    //Market Services
    public interface IMarketRepository<T> where T: class
    {
        IEnumerable<T> GetAll();
        T GetById(int id);
    }

    public interface IStockRepository : IMarketRepository<Stock>
    {
        IEnumerable<string> GetStockSymbols();
        Stock GetBySymbol(string symbol);
    }

    public interface IStockDetailRepository : IMarketRepository<StockDetail>
    {
        StockDetail GetBySymbol(string symbol);
    }

    //App Services
    //Read Only Repo 
    public interface IAppReadRepository<T> where T : class
    {
        IEnumerable<T> GetAll();
        T GetById(int id);
    }

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

    public interface IAccountRepository : IAppReadRepository<Account>
    {

    }
    
    public interface IUserRepository : IAppReadRepository<User>
    {
        bool AuthenticateUser(string username, string password, out string message, out int isDisabled, out int userID);
    }

}
