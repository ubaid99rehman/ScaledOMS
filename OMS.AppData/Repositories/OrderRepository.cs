using OMS.Core.Models;
using System.Collections.Generic;
using System.Data.SqlClient;
using OMS.DataAccess.Repositories.AppRepositories;
using OMS.Core.Models.Orders;
using OMS.SqlData.Model;
using System.Linq;
using AutoMapper;
using OMS.Core.Core.Models.User;
using System;

namespace OMS.SqlData.Repositories
{
    public class OrderRepository : IOrderRepository
    {
        OMSContext Context;
        IMapper Mapper;

        //Constructor
        public OrderRepository(IMapper mapper)
        {
            Context = new OMSContext();
            Mapper = mapper;
        }

        //Public Data Access Methods Implementation
        public IEnumerable<IOrder> GetAll()
        {
            var ordersList = Context.Orders.ToList();
            if (ordersList.Count < 0 || ordersList == null)
            {
                return new List<IOrder>();
            }
            ICollection<IOrder> orders = new List<IOrder>();
            try
            {
                 orders = ordersList.Select(o => Mapper.Map<IOrder>(o)).ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return orders;  
        }
        public IOrder GetById(int orderID)
        {
            Orders orderEntity = Context.Orders.Where(o => o.OrderID == orderID).FirstOrDefault();
            if (orderEntity == null)
            {
                return new Order();
            }
            var order = Mapper.Map<IOrder>(orderEntity);
            return order;
        }
        public bool Add(IOrder order)
        {
            Orders orderEntity = Mapper.Map<Orders>(order);
            orderEntity.Accounts = Context.Accounts.Where(o => o.AccountID == order.AccountID).FirstOrDefault();
            orderEntity.Order_Statuses = Context.Order_Statuses.Where(o => o.ID == order.Status).FirstOrDefault();
            orderEntity.Order_Types = Context.Order_Types.Where(o => o.ID == order.OrderType).FirstOrDefault();

            Context.Orders.Add(orderEntity);
            try
            {
                Context.SaveChanges();
            }
            catch (System.Data.Entity.Validation.DbEntityValidationException dbEx)
            {
                Exception raise = dbEx;
                foreach (var validationErrors in dbEx.EntityValidationErrors)
                {
                    foreach (var validationError in validationErrors.ValidationErrors)
                    {
                        string message = string.Format("{0}:{1}",
                            validationErrors.Entry.Entity.ToString(),
                            validationError.ErrorMessage);
                        raise = new InvalidOperationException(message, raise);
                    }
                }
                
                throw raise;
            }
            catch (System.Data.Entity.Infrastructure.DbUpdateException dbEx)
            {
                throw dbEx;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            Orders addedOrder = Context.Orders.Where(o => o.OrderGuid == order.OrderGuid).FirstOrDefault();
            if(addedOrder !=null)
            {
                return true;
            }
            return false;
        }
        public bool Update(IOrder order)
        {
            bool isUpdated = false;
            Orders toUpdate;
            try
            {
                toUpdate = Mapper.Map<Orders>(order);
            }
            catch(Exception ex)
            {
                throw ex; 
            }
            var userEntity = Context.Orders.Where(o => o.OrderID == order.OrderID).FirstOrDefault();
            if (userEntity != null)
            {
                // Manually update properties
                userEntity.Quantity = toUpdate.Quantity;
                userEntity.Total = toUpdate.Total;
                userEntity.Status = toUpdate.Status;
                userEntity.AccountID = toUpdate.AccountID;
                //userEntity.Accounts = toUpdate.Accounts;
                userEntity.ExpirationDate = toUpdate.ExpirationDate;
                userEntity.LastUpdatedDate = toUpdate.LastUpdatedDate;

                // Save changes
                int result = Context.SaveChanges();
                if(result > 0)
                {
                    isUpdated = true;
                }
            }
            return isUpdated;
        }
        public bool Delete(IOrder order)
        {
            bool isDeleted = false;
            //Remove Order
            Orders orderEntity = Context.Orders.Where(o => o.OrderID == order.OrderID).FirstOrDefault();
            if (orderEntity != null)
            {
                Context.Orders.Remove(orderEntity);
                Context.SaveChanges();
            }
            //Check if Order is removed
            orderEntity = Context.Orders.Where(o => o.OrderID == order.OrderID).FirstOrDefault();
            if (orderEntity == null)
            {
                isDeleted = true;
            }
            return isDeleted;
        }
    }
}
