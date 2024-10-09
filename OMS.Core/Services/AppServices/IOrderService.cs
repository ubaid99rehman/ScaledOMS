using OMS.Core.Models.Orders;
using System.ComponentModel;

namespace OMS.Core.Services.AppServices
{
    public interface IOrderService : IAppService<IOrder>, IReadonlyService<IOrder>
    {
        bool LoadOrders();
        bool CancelOrder(IOrder selectedOrder);

        //Order Status
        ICollectionView GetOpenOrders();                       
        ICollectionView GetCancelledOrders();                  
        ICollectionView GetFulfilledOrders();                  
        
        //User and Account
        ICollectionView GetOrdersByUser(int userId);           
        ICollectionView GetOrdersByAccount(int accountId);     
        IOrder GetLastOrderByUser();
        
        //Stocks
        ICollectionView GetOrdersByStock(string stockSymbol);
    }

}
