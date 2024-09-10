using OMS.Core.Models;
using OMS.Core.Services.AppServices;
using System.Collections.ObjectModel;

namespace OMS.Core.Services
{
    public interface IOrderService : IAppService<Order>
    {
        ObservableCollection<Order> GetOpenOrders();                       
        ObservableCollection<Order> GetCancelledOrders();                  
        ObservableCollection<Order> GetFulfilledOrders();                  
        ObservableCollection<Order> GetOrdersByUser(int userId);           
        ObservableCollection<Order> GetOrdersByAccount(int accountId);     
        ObservableCollection<Order> GetOrdersByStock(string stockSymbol);
        ObservableCollection<Order> GetOpenOrdersByStock(string stockSymbol);
        Order GetLastOrder();                                              
        bool AddOrder(Order order, out string message);                    
        void CancelOrder(Order selectedOrder, out string message);         
        void LoadOrders();                                                 
    }

}
