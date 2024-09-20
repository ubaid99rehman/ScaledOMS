﻿using Microsoft.Extensions.DependencyInjection;
using OMS.Cache;
using OMS.Core.Services.Cache;
using OMS.ViewModels;
using System;
using OMS.Services.AppServices;
using OMS.Core.Services.AppServices;
using OMS.DataAccess.Repositories;
using OMS.SqlData.Repositories;
using OMS.MarketData.Stocks;
using OMS.Core.Services.MarketServices.RealtimeServices;
using OMS.Core.Services.AppServices.RealtimeServices;
using OMS.DataAccess.Repositories.MarketRepositories;
using OMS.DataAccess.Repositories.AppRepositories;
using OMS.Services.MarketServices.RealtimeServices;
using OMS.VM.Trades;

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

            #region Repositories
            //App Services
            services.AddSingleton<IUserRepository, UserRepository>();
            services.AddSingleton<IAccountRepository, AccountRepository>();
            services.AddSingleton<IOrderRepository, OrderRepository>();
            services.AddSingleton<IUserRepository, UserRepository>();

            //Market Servces
            services.AddSingleton<IStockRepository, StockRepository>();
            services.AddSingleton<IStockDetailRepository, StockDetailRepository>();
            services.AddSingleton<IStockTradeDataRepository, StockTradeDataRepository>();
            services.AddSingleton<IMarketOrderRepository, MarketOrderRepository>();
            services.AddSingleton<IMarketTradeRepository, MarketTradeRepository>();
            #endregion

            #region Cache 
            services.AddSingleton<ICacheManager, CacheManager>();
            services.AddSingleton<ICacheService, CacheService>();
            #endregion

            #region Services
            //App Services
            services.AddSingleton<ISessionInfoServce, SessionInfoService>();
            services.AddSingleton<IAppTimerService, AppTimerService>();
            services.AddSingleton<IOrderService, OrderService>();
            services.AddSingleton<IAccountService, AccountService>();
            services.AddSingleton<IAuthService, AuthService>();
            
            //MarketServices
            services.AddSingleton<IStockDataService, StockDataService>();
            services.AddSingleton<IStockTradeDataService, StockTradeDataService>();
            services.AddSingleton<IMarketTradeService, MarketTradeService>();
            services.AddSingleton<IMarketOrderService, MarketOrderService>();
            services.AddSingleton<IBootStrapper, BootStrapper>();
            #endregion

            #region ViewModels
            //Loading
            services.AddSingleton<LoadingViewModel>();

            //Orders
            services.AddSingleton<AddOrderModel>();
            services.AddTransient<OrderBookModel>();
            services.AddTransient<OrderModel>();
            services.AddSingleton<OrdersListModel>();
            services.AddSingleton<OrderHistoryViewModel>();

            //Main
            services.AddSingleton<MainViewModel>();
            services.AddSingleton<InformationPanelViewModel>();

            //Home
            services.AddTransient<StockMarketModel>();
            services.AddSingleton<DashboardViewModel>();

            //Trade
            services.AddSingleton<TradeViewModel>();
            #endregion

            #region Views
            //Views
            services.AddSingleton<MainWindow>();
            services.AddSingleton<LoadingWindow>();
            //Controls
            services.AddTransient<IContextMenuHelper, ContextMenuHelper>(); 
            #endregion

            return services.BuildServiceProvider();
        }
        #endregion
    }
}
