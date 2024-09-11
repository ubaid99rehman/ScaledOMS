using OMS.Core.Models;
using OMS.Core.Services.AppServices;
using System;
using System.Collections.ObjectModel;

namespace OMS.Core.Services
{
    public interface IOrderService : IAppService<Order>
    {
        event Action DataUpdated;
        void CancelOrder(Order selectedOrder, out string message);
        Order GetLastOrderByUser();
        
        //Order Status
        ObservableCollection<Order> GetOpenOrders();                       
        ObservableCollection<Order> GetCancelledOrders();                  
        ObservableCollection<Order> GetFulfilledOrders();                  
        
        //User and Account
        ObservableCollection<Order> GetOrdersByUser(int userId);           
        ObservableCollection<Order> GetOrdersByAccount(int accountId);     
        
        //Stocks
        ObservableCollection<Order> GetOrdersByStock(string stockSymbol);
        ObservableCollection<Order> GetOpenOrdersByStock(string stockSymbol);
        
    }

}
