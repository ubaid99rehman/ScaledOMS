using OMS.Core.Models.Orders;
using System;
using System.Collections.ObjectModel;

namespace OMS.Core.Services.AppServices
{
    public interface IOrderService : IAppService<IOrder>
    {
        event Action<int> DataUpdated;
        void CancelOrder(IOrder selectedOrder, out string message);
        IOrder GetLastOrderByUser();
        
        //Order Status
        ObservableCollection<IOrder> GetOpenOrders();                       
        ObservableCollection<IOrder> GetCancelledOrders();                  
        ObservableCollection<IOrder> GetFulfilledOrders();                  
        
        //User and Account
        ObservableCollection<IOrder> GetOrdersByUser(int userId);           
        ObservableCollection<IOrder> GetOrdersByAccount(int accountId);     
        
        //Stocks
        ObservableCollection<IOrder> GetOrdersByStock(string stockSymbol);
        ObservableCollection<IOrder> GetOpenOrdersByStock(string stockSymbol);
        
    }

}
