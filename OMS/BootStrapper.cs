using OMS.Core.Core.Models.User;
using OMS.Core.Services.AppServices;
using OMS.Core.Services.Cache;
using OMS.Core.Services.MarketServices.RealtimeServices;

namespace OMS
{
    public class BootStrapper : IBootStrapper
    {
        ICacheService _cacheService;
        IOrderService _orderService;
        IStockDataService _stockDataService;
        IAccountService _accountService;


        public BootStrapper( IOrderService orderService, IStockDataService stockDataService,
            IAccountService accountService, ICacheService cacheService) 
        {
            _orderService = orderService;
            _stockDataService = stockDataService;
            _accountService = accountService;
            _cacheService = cacheService;
        }

        public void LoadServices()
        {
            _cacheService.Set("accounts", _accountService.GetAll());
            _cacheService.Set("accountsList", _accountService.GetAccountsList());
            _cacheService.Set("CurrentUser", new User{UserID =1 });
        }
    }
}
