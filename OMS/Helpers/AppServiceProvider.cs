using Microsoft.Extensions.DependencyInjection;
using OMS.Cache;
using OMS.Core.Services.Cache;
using OMS.ViewModels;
using System;
using OMS.Services.AppServices;
using OMS.Core.Services.AppServices;
using OMS.SqlData.Repositories;
using OMS.Core.Services.MarketServices.RealtimeServices;
using OMS.Core.Services.AppServices.RealtimeServices;
using OMS.DataAccess.Repositories.MarketRepositories;
using OMS.DataAccess.Repositories.AppRepositories;
using OMS.Services.MarketServices.RealtimeServices;
using OMS.VM.Trades;
using OMS.Services;
using OMS.VM.Settings;
using OMS.Core.Logging;
using OMS.Logging;
using AutoMapper;
using OMS.SqlData.Mapping;
using OMS.Core.Services;
using OMS.MarketData;

namespace OMS
{
    public static class AppServiceProvider
    {
        //Service Provider
        public static IServiceProvider ServiceProvider;
        public static IServiceProvider GetServiceProvider()
        {
            if (ServiceProvider == null)
            {
                ServiceProvider = ConfigureServices();
            }
            return ServiceProvider;
        }

        //Service PRovider Instance Creation
        private static ServiceProvider ConfigureServices()
        {
            var services = new ServiceCollection();
            IMapper Mapper = InitializeMapper();

            services.AddSingleton<IMapper>(sp => InitializeMapper());

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

            #region Logging
            services.AddSingleton<ILogHelper, LogHelper>(); 
            #endregion

            #region Services
            //App Services
            services.AddSingleton<ITimerService, TimerService>();
            services.AddSingleton<ISessionInfoServce, SessionInfoService>();
            services.AddSingleton<IAppTimerService, AppTimerService>();
            services.AddSingleton<IOrderService, OrderService>();
            services.AddSingleton<IAccountService, AccountService>();
            services.AddSingleton<IPermissionService, PermissionService>();
            services.AddTransient<IAuthService, AuthService>();
            services.AddTransient<IUserService, UserService>();

            //MarketServices
            services.AddSingleton<IStockDataService, StockDataService>();
            services.AddSingleton<IStockTradeDataService, StockTradeDataService>();
            services.AddSingleton<IMarketTradeService, MarketTradeService>();
            services.AddSingleton<IMarketOrderService, MarketOrderService>();
            services.AddSingleton<IBootStrapper, BootStrapper>();
            services.AddSingleton<IAppThemeService, AppThemeService>();
            #endregion

            #region ViewModels
            //App
            services.AddSingleton<AppThemeModel>();
            services.AddSingleton<LoadingViewModel>();
            services.AddSingleton<InformationPanelViewModel>();
            services.AddSingleton<MainViewModel>();
            //Orders
            services.AddSingleton<AddOrderModel>();
            services.AddTransient<OrderBookModel>();
            services.AddTransient<OrderModel>();
            services.AddSingleton<OrdersListModel>();
            services.AddTransient<OrderHistoryViewModel>();
            //Home
            services.AddTransient<StockMarketModel>();
            services.AddTransient<DashboardViewModel>();
            //portfolio
            services.AddSingleton<AccountPortfolioViewModel>();
            //Trade
            services.AddSingleton<TradeViewModel>();
            //User
            services.AddSingleton<ProfileModel>();
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

        private static IMapper InitializeMapper()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<MappingProfile>(); 
            });

            return config.CreateMapper();
        }
    }
}
