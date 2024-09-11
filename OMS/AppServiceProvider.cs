using Microsoft.Extensions.DependencyInjection;
using OMS.Cache;
using OMS.Core.Services.Cache;
using OMS.Core.Services.RealtimeServices;
using OMS.Core.Services;
using OMS.ViewModels;
using System;
using OMS.Services.AppServices;
using OMS.Core.Services.AppServices;
using OMS.DataAccess.Repositories;
using OMS.SqlData.Repositories;
using OMS.Orders;
using OMS.MarketData.Stocks;

namespace OMS
{
    public static class AppServiceProvider
    {
        public static IServiceProvider ServiceProvider;

        #region Service Configuration
        public static IServiceProvider GetServiceProvider()
        {
            if (ServiceProvider == null)
            {
                ServiceProvider = ConfigureServices();
            }
            return ServiceProvider;
        }

        private static ServiceProvider ConfigureServices()
        {
            var services = new ServiceCollection();
            //Services
            services.AddSingleton<IStockRepository, StockRepository>();
            services.AddSingleton<IStockDetailRepository, StockDetailRepository>();
            services.AddSingleton<IAccountRepository, AccountRepository>();
            services.AddSingleton<IOrderRepository, OrderRepository>();

            //Cache Services
            services.AddSingleton<ICacheManager, CacheManager>();
            services.AddSingleton<ICacheService, CacheService>();
            
            //Realtime App Services
            services.AddSingleton<ISessionInfoServce, SessionInfoService>();
            services.AddSingleton<IAppTimerService, AppTimerService>();
            
            //App Services
            services.AddSingleton<IOrderService, OrderService>();
            services.AddSingleton<IAccountService, AccountService>();
            services.AddSingleton<IStockDataService, StockDataService>();


            //Market Services


            //ViewModels//
            //Orders
            services.AddSingleton<AddOrderModel>();
            services.AddSingleton<OrderBookModel>();
            services.AddSingleton<OrdersListModel>();
            services.AddSingleton<OrderHistoryViewModel>();
            services.AddSingleton<OrderModel>();

            //Main
            services.AddSingleton<MainViewModel>();
            services.AddSingleton<InformationPanelViewModel>();
            
            //Home
            services.AddSingleton<StockMarketModel>();
            services.AddSingleton<DashboardViewModel>();
            

            //Views
            services.AddSingleton<MainWindow>();
            //services.AddSingleton<OpenOrdersView>();

            services.AddSingleton<IBootStrapper, BootStrapper>();
            
            return services.BuildServiceProvider();
        }
        #endregion
    }
}
