using DevExpress.Xpf.Core;
using DevExpress.Xpf.Docking;
using Microsoft.Extensions.DependencyInjection;
using OMS.Cache;
using OMS.Core.Services.Cache;
using OMS.Logging;
using OMS.Orders;
using OMS.ViewModels;
using System.Windows;

namespace OMS
{
    public partial class MainWindow : ThemedWindow
    {
        IBootStrapper BootStrapper;
        ICacheService CacheService;
        
        public MainWindow(IBootStrapper bootStrapper, ICacheService cacheService)
        {
            InitializeComponent();
            CacheService = cacheService;
            BootStrapper = bootStrapper;
            
            var model = AppServiceProvider.GetServiceProvider().GetRequiredService<MainViewModel>();
            documentManagerService = (TabbedDocumentUIService)model.DocumentManagerService;
            this.DataContext = model;

            LogHelper.LogInfo("Loading Services Data....");
            BootStrapper.LoadServices();
            LogHelper.LogInfo("Services Data Loaded");
        }

        private void InfoIcon_Click(object sender, RoutedEventArgs e)
        {
            InfoPopup.IsOpen = !InfoPopup.IsOpen;

        }

        private void NewOrder_Click(object sender, DevExpress.Xpf.Bars.ItemClickEventArgs e)
        {
            if(CacheService.ContainsKey("NewOrderWindowOpen"))
            {
                bool isOpened = CacheService.Get<bool>("NewOrderWindowOpen");
        
                if(!isOpened)
                {
                    AddOrder windo = new AddOrder(CacheService);
                    windo.Show();
                }
            }
            else
            {
                CacheService.Set("NewOrderWindowOpen",true);
                AddOrder windo = new AddOrder(CacheService);
                windo.Show();
            }
        }
    }
}
