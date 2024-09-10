﻿using Microsoft.Extensions.DependencyInjection;
using OMS.Cache;
using OMS.Core.Services.Cache;
using OMS.Core.Services.RealtimeServices;
using OMS.Core.Services;
using OMS.ViewModels;
using System;
using OMS.Services.AppServices;

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
            services.AddSingleton<ICacheManager, CacheManager>();
            services.AddSingleton<ICacheService, CacheService>();
            services.AddSingleton<ISessionInfoServce, SessionInfoService>();
            services.AddSingleton<IAppTimerService, AppTimerService>();

            //Views
            services.AddSingleton<MainWindow>();

            //ViewModels//
            /////////////
            services.AddSingleton<MainViewModel>();
            services.AddSingleton<InformationPanelViewModel>();
            services.AddSingleton<DashboardViewModel>();
            

            //Orders
            services.AddSingleton<AddOrderModel>();
            services.AddSingleton<OrderBookModel>();
            services.AddSingleton<OrdersListModel>();
            services.AddSingleton<OrderHistoryViewModel>();
            services.AddSingleton<OrderModel>();
            
            return services.BuildServiceProvider();
        }
        #endregion
    }
}
