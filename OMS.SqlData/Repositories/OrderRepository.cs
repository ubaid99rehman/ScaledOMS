using OMS.Core.Models;
using OMS.DataAccess.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OMS.SqlData.Repositories
{
    public class OrderRepository : IOrderRepository
    {
        public void Add(Order order)
        {
            throw new NotImplementedException();
        }

        public void Delete(int orderID)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Order> GetAll()
        {
            throw new NotImplementedException();
        }

        public Order GetById(int orderID)
        {
            throw new NotImplementedException();
        }

        public void Update(Order order)
        {
            throw new NotImplementedException();
        }
    }
}
