using DevExpress.Xpf.Core;
using DevExpress.Xpf.Docking;
using Microsoft.Extensions.DependencyInjection;
using OMS.Cache;
using OMS.Core.Services.Cache;
using OMS.Logging;
using OMS.Orders;
using OMS.ViewModels;
using System.Configuration;
using System.IO;
using System.Windows;

namespace OMS
{
    public partial class MainWindow : ThemedWindow
    {
        IBootStrapper BootStrapper;
        ICacheService CacheService;

        private string layoutFilePath;

        public MainWindow(IBootStrapper bootStrapper, ICacheService cacheService)
        {
            InitializeComponent();

            layoutFilePath = ConfigurationManager.AppSettings["LayoutFilePath"];

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

        private void ThemedWindow_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (this.DataContext is MainViewModel model)
            {
                model.SaveOpenedDocumentsState();
            }
        }

        private void ThemedWindow_Loaded(object sender, RoutedEventArgs e)
        {
            if(this.DataContext is MainViewModel model)
            {
                model.RestoreOpenedDocumentsState();
            }
        }
    }
}
