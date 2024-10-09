using DevExpress.Mvvm;
using OMS.Core.Services.AppServices;
using System.Collections.ObjectModel;
using OMS.Core.Models.User;
using OMS.Core.Models.Roles;
using System.Linq;
using System.ComponentModel;

namespace OMS.ViewModels
{
    public class OrderHistoryViewModel : ViewModelBase
    {
        //Service
        private IUserService _userService;
        private IOrderService OrderService;
        private IPermissionService PermissionService;

        //Private Data Member
        private ICollectionView orders;
        private ObservableCollection<IUser> users;
        private bool _ShowUsers;
        private IUser _SelectedUser;

        public ICollectionView Orders
        {
            get => OrderService.GetOrdersByUser(SelectedUser.UserID);
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
        public OrderHistoryViewModel(IOrderService orderService, 
            IUserService userService, IPermissionService permissionService)
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
                    Users = _userService.GetAll();
                    SelectedUser = Users.FirstOrDefault();
                }
            }
        }
        private void LoadOrders()
        {
            if(SelectedUser !=null && SelectedUser.UserID > 0)
            {
                Orders = OrderService.GetOrdersByUser(SelectedUser.UserID);
            }
        }
    }
}
