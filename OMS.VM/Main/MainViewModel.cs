using DevExpress.Mvvm;
using DevExpress.Mvvm.DataAnnotations;
using DevExpress.Xpf.Charts;
using DevExpress.Xpf.Charts.Native;
using DevExpress.Xpf.Docking;
using DevExpress.Xpf.Docking.Native;
using OMS.Common.Helper;
using OMS.Core.Logging;
using OMS.Core.Models.App;
using OMS.Core.Models.Permissions;
using OMS.Core.Services;
using OMS.Core.Services.AppServices;
using OMS.Core.Services.AppServices.RealtimeServices;
using OMS.Core.Services.Cache;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Drawing.Text;
using System.IO;
using System.Linq;

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
        private IDocumentStateService DocumentStateService;

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
        //Landing Page Displayed 
        private bool landingPageLoaded;
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
        public MainViewModel(IAppTimerService appTimerService, ICacheService cacheService, ITimerService timerService,  
            IDocumentStateService documentStateService, ILogHelper logHelper, IPermissionService permissionService, IUserService userService )
        {
            Logger = logHelper;
            landingPageLoaded = false;
            Title = "OMS";

            timerService.Start();
            CacheService = cacheService;
            AppTimerService = appTimerService;
            PermissionService  = permissionService;
            DocumentStateService = documentStateService;
            UserService = userService;
            LoadLayoutFile();
            SetRolePermissions();
        }

        private void LoadLayoutFile()
        {
            int id = UserService.GetUser().UserID;
            if(id > 0)
            {
                string filePath = ConfigurationHelper.GetAppSetting("LayoutFilePath");
                string fileName = Path.Combine(filePath, $"{id}.xml");
                DocumentStateService.LoadFile(fileName);    
            }
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

            //Permissions
            CanViewDashboard = false;
            CanViewMarketWatch = false;
            CanViewPortfolio = false;
            CanViewOrderHistory = false;
            CanViewManageOrders = false;
            CanViewAddOrder = false;
            CanViewEditOrder = false;

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

        //Document Open Close
        private void AddView(string view, string title)
        {
            if(title.Equals("DashboardView"))
            {
                return;
            }
            Navigating();
            if (DocumentManagerService == null)
            {
                var exception = new InvalidOperationException("DocumentManagerService is not available.");
                Logger.LogError(exception.Message, exception);
                return;
            }
            IDocument existingDocument = GetDocument(title);
            if (existingDocument == null)
            {
                // Create a new document if no existing document is found
                IDocument document = DocumentManagerService.CreateDocument(view, null, this);
                document.Title = title;
                document.DestroyOnClose = true;
                document.Show();
                Title = title;
                if(document.Content is IDisposable doisposable)
                {
                    doisposable.Dispose();
                }
                    
                Logger.LogInfo($"Loaded new View: {view}");
            }
            Navigated();
            if (landingPageLoaded)
            {
                SaveOpenedDocumentsState();
            }
        }
        private IDocument GetDocument(string title)
        {
            return DocumentManagerService.Documents.FirstOrDefault(doc => doc.Title.ToString() == title);
        }



        //Layouts
        public IEnumerable<IDocument> GetOpenedDocuments()
        {
            return DocumentManagerService.Documents;
        }
        public void SaveOpenedDocumentsState()
        {
            var documentStates = GetOpenedDocuments().Select(doc =>
            new ScreenState
            {
                ViewName = ((DockingDocumentUIServiceBase<DocumentPanel, DocumentGroup>.Document)doc).DocumentType,
                Title = doc.Title.ToString()
            }).ToList();
           
            DocumentStateService.SaveDocumentStates(documentStates);
        }
        public void RestoreOpenedDocumentsState()
        {
            var documentStates = DocumentStateService.GetDocumentStates();
            if (documentStates != null && documentStates.Count >= 1)
            {
                foreach (var doc in documentStates)
                {
                    AddView(doc.ViewName, doc.Title);
                }
            }

            landingPageLoaded = true;
            DocumentManagerService.ActiveDocumentChanged += OnDocumentActivated;
        } 
    }
}