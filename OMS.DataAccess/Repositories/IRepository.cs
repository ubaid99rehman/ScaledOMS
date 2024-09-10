using OMS.Core.Core.Models.User;
using OMS.Core.Enums;
using OMS.Core.Models;
using OMS.Core.Models.Account;
using OMS.Core.Models.Stocks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OMS.DataAccess.Repositories
{
    public interface IRepository<T> where T : class
    {
        IEnumerable<T> GetAll();
        T GetById(int id);
        bool Add(T entity);
        bool Delete(T entity);
        bool Update(T entity);
    }

    public interface IStockDetailRepository
    {
        IEnumerable<StockDetail> GetAll();
        StockDetail GetById(int id);
        void Add(StockDetail stockDetail);
        void Update(StockDetail stockDetail);
        void Delete(int id);
    }

    public interface IStockTradeRepository
    {
        IEnumerable<StockTradingData> GetAll();
        StockTradingData GetById(int id);
        void Add(StockTradingData stockTrade);
        void Update(StockTradingData stockTrade);
        void Delete(int id);
    }

    public interface IStockHistoryRepository
    {
        IEnumerable<StockTradingData> GetAll();
        StockTradingData GetById(int id);
        void Add(StockTradingData stockHistory);
        void Update(StockTradingData stockHistory);
        void Delete(int id);
    }

    public interface IOrderRepository
    {
        IEnumerable<Order> GetAll();
        Order GetById(int orderID);
        void Add(Order order);
        void Update(Order order);
        void Delete(int orderID);
    }

    public interface IAccountRepository
    {
        IEnumerable<Account> GetAll();
        Account GetById(int orderID);
    }

    public interface IOrderTypeRepository
    {
        IEnumerable<OrderType> GetAll();
        OrderType GetById(int id);
        void Add(OrderType orderType);
        void Update(OrderType orderType);
        void Delete(int id);
    }

    public interface IOrderStatusRepository
    {
        IEnumerable<OrderStatus> GetAll();
        OrderStatus GetById(int id);
        void Add(OrderStatus orderStatus);
        void Update(OrderStatus orderStatus);
        void Delete(int id);
    }

    public interface IUserRepository
    {
        IEnumerable<User> GetAll();
        User GetById(int id);
        void Add(User user);
        void Update(User user);
        void Delete(int id);
        bool AuthenticateUser(string username, string password, out string message, out int isDisabled, out int userID);

    }

}
