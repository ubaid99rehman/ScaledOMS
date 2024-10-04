using DevExpress.Mvvm;
using DevExpress.Mvvm.DataAnnotations;
using DevExpress.Xpf.Docking;
using DevExpress.Xpf.Docking.Native;
using OMS.Core.Logging;
using OMS.Core.Models.App;
using OMS.Core.Models.Permissions;
using OMS.Core.Services.AppServices.RealtimeServices;
using OMS.Core.Services.Cache;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Xml.Serialization;

namespace OMS.ViewModels
{
    public partial class MainViewModel : ViewModelBase
    {
        // Define visibility properties based on role permissions
        private bool _CanViewDashboard;
        private bool _CanViewMarketWatch;
        private bool _CanViewManageOrders;
        private bool _CanViewOrderHistory;
        private bool _CanViewPortfolio;
        private bool _CanViewAddOrder;
        private bool _CanViewEditOrder;
        private bool _CanViewOpenOrders;

        public bool CanViewDashboard
        {
            get { return _CanViewDashboard; }
            set { SetProperty(ref _CanViewDashboard, value, nameof(CanViewDashboard)); }
        }
        public bool CanViewMarketWatch
        {
            get { return _CanViewMarketWatch; }
            set { SetProperty(ref _CanViewMarketWatch, value, nameof(CanViewMarketWatch)); }
        }
        public bool CanViewManageOrders
        {
            get { return _CanViewManageOrders; }
            set { SetProperty(ref _CanViewManageOrders, value, nameof(CanViewManageOrders)); }
        }
        public bool CanViewOrderHistory
        {
            get { return _CanViewOrderHistory; }
            set { SetProperty(ref _CanViewOrderHistory, value, nameof(CanViewOrderHistory)); }
        }
        public bool CanViewOpenOrders
        {
            get { return _CanViewOpenOrders; }
            set { SetProperty(ref _CanViewOpenOrders, value, nameof(CanViewOpenOrders)); }
        }
        public bool CanViewAddOrder
        {
            get { return _CanViewAddOrder; }
            set { SetProperty(ref _CanViewAddOrder, value, nameof(CanViewAddOrder)); }
        }
        public bool CanViewEditOrder
        {
            get { return _CanViewEditOrder; }
            set { SetProperty(ref _CanViewEditOrder, value, nameof(CanViewEditOrder)); }
        }
        public bool CanViewPortfolio
        {
            get { return _CanViewPortfolio; }
            set { SetProperty(ref _CanViewPortfolio, value, nameof(CanViewPortfolio)); }
        }

        //Theme Layout File
        private string fileName;
        //Landing Page Displayed 
        private bool landingPageLoaded;

        //Services
        private IAppTimerService AppTimerService;
        private ILogHelper Logger;
        private ICacheService CacheService;
        //Navigation Service
        [ServiceProperty(Key = "documentManagerService")]
        public virtual IDocumentManagerService DocumentManagerService
        {
            get { return GetService<IDocumentManagerService>(); }
        }

        //Private Members
        private string _title;
        private bool _isLoadingScreen;

        //Public Members
        public IAppTime CurrentTime
        {
            get { return AppTimerService.GetCurrentDateTime(); }
        }
        public string Title
        {
            get { return _title; }
            set { SetProperty(ref _title, value, nameof(Title)); }
        }
        public bool IsLoadingScreen
        {
            get
            {
                return this._isLoadingScreen;
            }

            set
            {
                SetProperty(ref _isLoadingScreen, value, nameof(IsLoadingScreen));
            }
        }

        //Constructor
        public MainViewModel(IAppTimerService timerService, ICacheService cacheService, ILogHelper logHelper )
        {
            Logger = logHelper;
            fileName = ConfigurationManager.AppSettings["LayoutFilePath"];
            landingPageLoaded = false;
            CacheService = cacheService;
            AppTimerService = timerService;

            AppTimerService.StartSession();
            Title = "OMS";
            //Permissions
            CanViewMarketWatch = false;
            CanViewOrderHistory = false;
            CanViewOpenOrders = false;
            CanViewAddOrder = false;
            CanViewEditOrder = false;
            CanViewManageOrders = false;

            SetRolePermissions();
        }

        private void SetRolePermissions()
        {
            ObservableCollection<IPermission> userPermissions = CacheService.Get<ObservableCollection<IPermission>>("UserPermissions");
            var permissions = userPermissions.Where(p => p.CanRead).ToList();
            foreach (IPermission permission in permissions)
            {
                switch (permission.Screen.ScreenName)
                {
                    case "StockMarketView": CanViewMarketWatch = true;  break;
                    case "OrderHistoryView": CanViewOrderHistory = true;  break;
                    case "OpenOrdersView": CanViewOpenOrders = true;  break;
                    case "AddOrderView": CanViewAddOrder = true;  break;
                    case "EditOrderView": CanViewEditOrder = true;  break;
                    case "ManageOrderView": CanViewManageOrders = true; break;
                    default: break;
                }
            }
        }

        #region LoadCanViewPortfolio ing Decorator
        private void Navigating()
        {
            IsLoadingScreen = true;
        }
        private void Navigated()
        {
            IsLoadingScreen = false;
        }
        #endregion

        #region Views Navigation Commands
        [Command]
        public void Dashboard()
        {
            AddView("DashboardView", "Dashboard");
        }
        [Command]
        public void StockMarket()
        {
            AddView("StockMarketView", "Market Watch");
        }
        [Command]
        public void Portfolio()
        {
            AddView("AccountPortfolioView", "Portfolio");
        }
        [Command]
        public void ManageOrders()
        {
            AddView("ManageOrderView", "Manage Orders");
        }
        [Command]
        public void OrderHistory()
        {
            AddView("OrderHistoryView", "Orders History");
        }
        [Command]
        public void Profile()
        {
            AddView("ProfileView", "Profile");
        }
        [Command]
        public void Appearance()
        {
            AddView("AppearanceView", "Appearance");
        }
        #endregion

        //View Changed Event Handler
        private void OnDocumentActivated(object sender, ActiveDocumentChangedEventArgs e)
        {
            Title = (string)e.NewDocument?.Title ?? "HOME";
        }

        //Open Views Layout Method
        public IEnumerable<IDocument> GetOpenedDocuments()
        {
            return DocumentManagerService.Documents;
        }
        public void SaveOpenedDocumentsState()
        {
            var documentStates = GetOpenedDocuments().Select(doc =>
            new DocumentState
            {
                ViewType = ((DockingDocumentUIServiceBase<DocumentPanel, DocumentGroup>.Document)doc).DocumentType,
                Title = doc.Title.ToString()
            }).ToList();
            var serializer = new XmlSerializer(typeof(List<DocumentState>));
            using (var writer = new StreamWriter(fileName))
            {
                serializer.Serialize(writer, documentStates);
            }
        }
        public void RestoreOpenedDocumentsState()
        {
            string fileName = ConfigurationManager.AppSettings["LayoutFilePath"];

            if (File.Exists(fileName))
            {
                var serializer = new XmlSerializer(typeof(List<DocumentState>));
                List<DocumentState> documentStates = new List<DocumentState>();
                using (var reader = new StreamReader(fileName))
                {
                    documentStates = (List<DocumentState>)serializer.Deserialize(reader);
                }
                if (documentStates != null && documentStates.Count >= 1)
                {
                    foreach (var doc in documentStates)
                    {
                        AddView(doc.ViewType, doc.Title);
                    }
                }
            }
            landingPageLoaded = true;
            DocumentManagerService.ActiveDocumentChanged += OnDocumentActivated;
        } 

        //Open New View
        public void AddView(string view, string title)
        {
            Navigating();
            if (DocumentManagerService == null)
            {
                var exception = new InvalidOperationException("DocumentManagerService is not available.");
                Logger.LogError(exception.Message, exception);
            }

            IDocument document = DocumentManagerService.CreateDocument(view, null, this);
            document.Title = title;
            document.Show();
            Title = title;
            Logger.LogInfo($"Loaded View: {view}");
            Navigated();
            if(landingPageLoaded)
            {
                SaveOpenedDocumentsState();
            }
        }
    }
    
    //Document State Class
    public class DocumentState
    {
        public string Content { get; set; }
        public string Title { get; set; }
        public string ViewType { get; set; }
    }
}