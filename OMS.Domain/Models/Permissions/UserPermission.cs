using OMS.Core.Models.User;
using System;

namespace OMS.Core.Models.Permissions
{
    public class UserPermission : BaseModel, IUserPermission
    {
        private int _UserID;
        private int _PermissionID;
        private DateTime _AssignedDate;
        private IPermission _Permissions;
        private IUser _Users;
        
        public int UserID
        {
            get { return _UserID; }
            set { _UserID = value; }
        }
        public int PermissionID
        {
            get { return _PermissionID; }
            set { _PermissionID = value; }
        }
        public DateTime AssignedDate
        {
            get { return _AssignedDate; }
            set { _AssignedDate = value; }
        }
        public IPermission Permissions
        {
            get { return _Permissions; }
            set { _Permissions = value; }
        }
        public IUser Users
        {
            get { return _Users; }
            set { _Users = value; }
        }
    }
}
