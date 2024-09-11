using OMS.Core.Services;
using OMS.Core.Services.AppServices;
using System;

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
        }
    }
}
