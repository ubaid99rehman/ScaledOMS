using OMS.Core.Models.Orders;
using System;
using System.Collections.Generic;

namespace OMS.DataAccess.Repositories.AppRepositories
{
    public interface IOrderRepository
    {
        IEnumerable<IOrder> GetUserOrders(int id);
        IEnumerable<IOrder> GetRecentOrders(int orderID);
        IEnumerable<IOrder> GetAll();
        IOrder GetByGuid(Guid guid);
        IOrder GetById(int id);
        IOrder Add(IOrder entity);
        IOrder Update(IOrder entity);
        IOrder CancelOrder(IOrder entity);
    }
}
