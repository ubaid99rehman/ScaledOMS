using OMS.Core.Services.AppServices;
using OMS.Core.Services.Cache;
using OMS.Core.Services.MarketServices.RealtimeServices;

namespace OMS
{
    public class BootStrapper : IBootStrapper
    {
        #region Props Services
        ICacheService _cacheService;
        IOrderService _orderService;
        IStockDataService _stockDataService;
        IAccountService _accountService;
        #endregion

        #region Constructor
        public BootStrapper(IOrderService orderService, IStockDataService stockDataService,
            IAccountService accountService, ICacheService cacheService)
        {
            _orderService = orderService;
            _stockDataService = stockDataService;
            _accountService = accountService;
            _cacheService = cacheService;
        }
        #endregion

        #region Methods
        public void LoadData()
        {
            LoadAccountData();
            LoadStocksData();
            LoadOrdersData();
        }
        public void LoadOrdersData()
        {
            _cacheService.Set("Orders", _orderService.GetAll());
        }
        public void LoadStocksData()
        {
            _cacheService.Set("StockSymbols", _stockDataService.GetStockSymbols());
        }
        public void LoadAccountData()
        {
            _cacheService.Set("Accounts", _accountService.GetAll());
            _cacheService.Set("AccountsList", _accountService.GetAccountsList());
        }
        #endregion
    }
}
