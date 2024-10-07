using DevExpress.Mvvm;
using OMS.Core.Services.AppServices;
using System.Collections.ObjectModel;
using OMS.Core.Models.Orders;
using OMS.Core.Models.User;
using OMS.Core.Models.Roles;
using System.Linq;

namespace OMS.ViewModels
{
    public class OrderHistoryViewModel : ViewModelBase
    {
        //Service
        private IUserService _userService;
        private IOrderService OrderService;
        private IPermissionService PermissionService;

        

        //Private Data Member
        private ObservableCollection<IOrder> orders;
        private ObservableCollection<IUser> users;
        private bool _ShowUsers;
        private IUser _SelectedUser;

        public ObservableCollection<IOrder> Orders
        {
            get => orders ?? (orders = new ObservableCollection<IOrder>());
            set
            {
                SetProperty(ref orders, value, nameof(Orders));
            }
        }
        public ObservableCollection<IUser> Users
        {
            get => users ?? (Users = new ObservableCollection<IUser>());
            set
            {
                SetProperty(ref users, value, nameof(Users));
            }
        }
        public bool ShowUsers
        {
            get => _ShowUsers;
            set => SetProperty(ref _ShowUsers, value, nameof(ShowUsers));
        }
        public IUser SelectedUser
        {
            get => _SelectedUser;
            set 
            {
                if(SetProperty(ref _SelectedUser, value, nameof(SelectedUser)))
                {
                    if (SelectedUser != null)
                    {
                        Orders = OrderService.GetOrdersByUser(SelectedUser.UserID);
                    }
                }
            }
        }

        //Constructor
        public OrderHistoryViewModel(IOrderService orderService, IUserService userService, IPermissionService permissionService)
        {
            OrderService = orderService;
            _userService = userService;
            PermissionService = permissionService;
            LoadUsers();
            LoadOrders();
            _userService = userService;
        }

        //Data Loading Method
        private void LoadUsers()
        {
            IUser user = _userService.GetUser();
            ObservableCollection<IUserRole> userRoles = PermissionService.GetUserRoles();
            foreach(IUserRole role in userRoles)
            {
                if(role.RoleID ==3)
                {
                    ShowUsers = true;
                    Users = _userService.GetUsers();
                    SelectedUser = Users.FirstOrDefault();
                }
            }
        }
        private void LoadOrders()
        {
            Orders = OrderService.GetAll();
        }
    }
}
