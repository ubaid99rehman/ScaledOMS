using OMS.Core.Services.AppServices;
using OMS.Core.Services.Cache;
using OMS.Core.Services.MarketServices.RealtimeServices;
using System.Threading.Tasks;

namespace OMS.Services.StartupServices
{
    public class BootStrapper : IBootStrapper
    {
        //Services
        ICacheService _cacheService;
        IOrderService _orderService;
        IStockDataService _stockDataService;
        IAccountService _accountService;

        //Constructor
        public BootStrapper(IOrderService orderService, IStockDataService stockDataService,
            IAccountService accountService, ICacheService cacheService)
        {
            _orderService = orderService;
            _stockDataService = stockDataService;
            _accountService = accountService;
            _cacheService = cacheService;
        }

        //Methods
        public async Task LoadData()
        {
            await LoadAccountData();
            await LoadStocksData();
            await LoadOrdersData();
        }
        public async Task LoadOrdersData()
        {
            await Task.Delay(100);
            _cacheService.Set("Orders", _orderService.GetAll());
        }
        public async Task LoadStocksData()
        {
            await Task.Delay(100);
            _cacheService.Set("StockSymbols", _stockDataService.GetStockSymbols());
        }
        public async Task LoadAccountData()
        {
            await Task.Delay(100);
            _cacheService.Set("Accounts", _accountService.GetAll());
            _cacheService.Set("AccountsList", _accountService.GetAccountsList());
        }
    }
}
