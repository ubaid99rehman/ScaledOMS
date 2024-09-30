using DevExpress.Mvvm;
using DevExpress.Mvvm.DataAnnotations;
using DevExpress.Xpf.Docking;
using DevExpress.Xpf.Docking.Native;
using OMS.Core.Models;
using OMS.Core.Services.AppServices;
using OMS.Core.Services.AppServices.RealtimeServices;
using OMS.Core.Services.MarketServices.RealtimeServices;
using OMS.Logging;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Xml.Serialization;

namespace OMS.ViewModels
{
    public partial class MainViewModel : ViewModelBase
    {
        //Layout File
        private string fileName;
        //Landing Page Displayed 
        private bool landingPageLoaded;
        //DateTime Service
        private IAppTimerService AppTimerService;
        //Navigation Service
        [ServiceProperty(Key = "documentManagerService")]
        public virtual IDocumentManagerService DocumentManagerService
        {
            get { return GetService<IDocumentManagerService>(); }
        }

        #region Props
        public AppTime CurrentTime
        {
            get { return AppTimerService.GetCurrentDateTime(); }
        }

        private string _title;
        public string Title
        {
            get { return _title; }
            set { SetProperty(ref _title, value, nameof(Title)); }
        }

        private bool _isLoadingScreen;
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

        private AddOrderModel _orderModel;
        public AddOrderModel NewOrderModel
        {
            get { return _orderModel; }
            set { SetProperty(ref _orderModel, value, nameof(NewOrderModel)); }
        }
        #endregion

        #region Constructor
        public MainViewModel(IAppTimerService timerService,
            IOrderService orderService, IAccountService accountService,
            IStockDataService stockDataService)
        {
            fileName = ConfigurationManager.AppSettings["LayoutFilePath"];
            landingPageLoaded = false;
            AppTimerService = timerService;
            AppTimerService.StartSession();
            Title = "OMS";
            DocumentManagerService.ActiveDocumentChanged += OnDocumentActivated;
            NewOrderModel = new AddOrderModel(orderService, stockDataService, accountService);
        }
        #endregion

        #region Loading Decorator
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

        [Command]
        public void OnViewLoaded()
        {
            if (DocumentManagerService != null)
            {
                Dashboard();
            }
            else
            {
                var exception = new InvalidOperationException("DocumentManagerService is not available.");
                LogHelper.LogError(exception.Message, exception);
            }
        }
        #endregion

        #region Event Handler
        private void OnDocumentActivated(object sender, ActiveDocumentChangedEventArgs e)
        {
            Title = (string)e.NewDocument?.Title ?? "HOME";
        }
        #endregion

        #region Pagess Layout Method
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
        } 
        #endregion

        //Render New Page/View
        public void AddView(string view, string title)
        {
            Navigating();
            if (DocumentManagerService == null)
            {
                var exception = new InvalidOperationException("DocumentManagerService is not available.");
                LogHelper.LogError(exception.Message, exception);
            }

            IDocument document = DocumentManagerService.CreateDocument(view, null, this);
            document.Title = title;
            document.Show();
            Title = title;
            Navigated();
            if(landingPageLoaded)
            {
                SaveOpenedDocumentsState();
            }
        }
    }
    public class DocumentState
    {
        public string Content { get; set; }
        public string Title { get; set; }
        public string ViewType { get; set; }
    }
}
    
