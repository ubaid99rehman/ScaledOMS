using DevExpress.Mvvm;
using DevExpress.Mvvm.DataAnnotations;
using DevExpress.Xpf.Docking;
using DevExpress.Xpf.Docking.Native;
using OMS.Core.Logging;
using OMS.Core.Models.App;
using OMS.Core.Models.Permissions;
using OMS.Core.Models.User;
using OMS.Core.Services.AppServices;
using OMS.Core.Services.AppServices.RealtimeServices;
using OMS.Core.Services.Cache;
using OMS.Domain.Models.App;
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
        #region Services
        private IAppTimerService AppTimerService;
        private ILogHelper Logger;
        private ICacheService CacheService;
        private IUserService UserService;
        private IPermissionService PermissionService;

        //Navigation Service
        [ServiceProperty(Key = "documentManagerService")]
        public virtual IDocumentManagerService DocumentManagerService
        {
            get { return GetService<IDocumentManagerService>(); }
        }
        #endregion

        #region Private Members
        //TitleBar Title
        private string _title;
        //Loading Indicator
        private bool _isLoadingScreen;
        //Screen visibility based on role permissions
        private bool _CanViewDashboard;
        private bool _CanViewMarketWatch;
        private bool _CanViewManageOrders;
        private bool _CanViewOrderHistory;
        private bool _CanViewPortfolio;
        private bool _CanViewAddOrder;
        private bool _CanViewEditOrder;
        private bool _CanViewOpenOrders;
        //Theme Layout File
        private string filePath;
        //Landing Page Displayed 
        private bool landingPageLoaded;
        private IUser CurrentUser;
        #endregion

        #region Public Members
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
        #endregion

        //Constructor
        public MainViewModel(IAppTimerService timerService, ICacheService cacheService, 
            ILogHelper logHelper, IPermissionService permissionService, IUserService userService )
        {
            Logger = logHelper;
            filePath = ConfigurationManager.AppSettings["LayoutFilePath"];
            landingPageLoaded = false;
            CacheService = cacheService;
            AppTimerService = timerService;
            PermissionService  = permissionService;
            UserService = userService;
            CurrentUser = UserService.GetUser();
            AppTimerService.StartSession();
            Title = "OMS";
            //Permissions
            CanViewDashboard = false;
            CanViewMarketWatch = false;
            CanViewPortfolio = false;
            CanViewOrderHistory = false;
            CanViewManageOrders = false;
            CanViewAddOrder = false;
            CanViewEditOrder = false;

            SetRolePermissions();
        }

        #region Navigation Commands
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

        #region Loading Decorator Visibility Methods
        private void Navigating()
        {
            IsLoadingScreen = true;
        }
        private void Navigated()
        {
            IsLoadingScreen = false;
        }
        #endregion

        //View Changed Event Handler
        private void SetRolePermissions()
        {
            ObservableCollection<IPermission> userPermissions = PermissionService.GetUserViewPermissions();
            foreach (IPermission permission in userPermissions)
            {
                switch (permission.Screen.ScreenName)
                {
                    case "DashboardView": CanViewDashboard = true; break;
                    case "StockMarketView": CanViewMarketWatch = true;  break;
                    case "OrderHistoryView": CanViewOrderHistory = true;  break;
                    case "AddOrderView": CanViewAddOrder = true;  break;
                    case "EditOrderView": CanViewEditOrder = true;  break;
                    case "ManageOrderView": CanViewManageOrders = true; break;
                    case "AccountPortfolioView": CanViewPortfolio = true; break;
                    default: break;
                }
            }
        }
        private void OnDocumentActivated(object sender, ActiveDocumentChangedEventArgs e)
        {
            Title = (string)e.NewDocument?.Title ?? "HOME";
        }
        
        //Open New View
        private void AddView(string view, string title)
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

        //Open Views Layout Method
        public IEnumerable<IDocument> GetOpenedDocuments()
        {
            return DocumentManagerService.Documents;
        }
        public void SaveOpenedDocumentsState()
        {
            var documentStates = GetOpenedDocuments().Select(doc =>
            new AppScreen
            {
                ViewName = ((DockingDocumentUIServiceBase<DocumentPanel, DocumentGroup>.Document)doc).DocumentType,
                Title = doc.Title.ToString()
            }).ToList();
            var serializer = new XmlSerializer(typeof(List<AppScreen>));

            string fileName = Path.Combine(filePath, $"{CurrentUser.UserID}.xml");
            using (var writer = new StreamWriter(fileName))
            {
                serializer.Serialize(writer, documentStates);
            }
        }
        public void RestoreOpenedDocumentsState()
        {
            string fileName = Path.Combine(filePath, $"{CurrentUser.UserID}.xml");
            if (File.Exists(fileName))
            {
                var serializer = new XmlSerializer(typeof(List<AppScreen>));
                List<AppScreen> documentStates = new List<AppScreen>();
                using (var reader = new StreamReader(fileName))
                {
                    documentStates = (List<AppScreen>)serializer.Deserialize(reader);
                }
                if (documentStates != null && documentStates.Count >= 1)
                {
                    foreach (var doc in documentStates)
                    {
                        AddView(doc.ViewName, doc.Title);
                    }
                }
            }
            landingPageLoaded = true;
            DocumentManagerService.ActiveDocumentChanged += OnDocumentActivated;
        } 
    }
}