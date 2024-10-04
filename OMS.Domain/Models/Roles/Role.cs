using OMS.Core.Models.Permissions;
using System;
using System.Collections.Generic;


namespace OMS.Core.Models.Roles
{
    public class Role : BaseModel, IRole
    {
        private int _roleID;
        private string _roleName;
        private string _roleDescription;
        private DateTime _createdDate;
        private int _createdBy;
        private DateTime _updatedDate;
        private int _updatedBy;
        private ICollection<IPermission> _permissions;

        public int RoleID
        {
            get => _roleID;
            set { SetProperty(ref _roleID, value); }
        }
        public string RoleName
        {
            get => _roleName;
            set {  SetProperty(ref _roleName, value);}
        }
        public string RoleDescription
        {
            get { return _roleDescription; }
            set {  SetProperty(ref _roleDescription, value);}
        }
        public DateTime CreatedDate
        {
            get => _createdDate;
            set { SetProperty(ref _createdDate, value); }
        }
        public int CreatedBy
        {
            get => _createdBy;
            set
            {
                SetProperty(ref _createdBy, value);
            }
        }
        public DateTime UpdatedDate
        {
            get => _updatedDate;
            set 
            {
                SetProperty(ref _updatedDate, value);
            }
        }
        public int UpdatedBy
        {
            get => _updatedBy;
            set
            {
                SetProperty(ref _updatedBy, value);
            }
        }
        public ICollection<IPermission> Permissions
        {
            get => _permissions; 
            set => SetProperty(ref _permissions, value);
        }
    
    }
}
