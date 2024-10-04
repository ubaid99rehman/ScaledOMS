using OMS.Core.Models.App;
using System;

namespace OMS.Core.Models.Permissions
{
    public class Permission : BaseModel, IPermission
    {
        private int _PermissionID;
        private int _ScreenID;
        private bool _CanCreate;
        private bool _CanRead;
        private bool _CanUpdate;
        private bool _CanDelete;
        private DateTime _CreatedDate;
        private int _CreatedBy;
        private DateTime _UpdatedDate;
        private int _UpdatedBy;
        private IScreen _Screen;

        public int PermissionID
        {
            get { return _PermissionID; }
            set { _PermissionID = value; }
        }
        public int ScreenID
        {
            get { return _ScreenID; }
            set { _ScreenID = value; }
        }
        public bool CanCreate
        {
            get { return _CanCreate; }
            set { _CanCreate = value; }
        }
        public bool CanRead
        {
            get { return _CanRead; }
            set { _CanRead = value; }
        }
        public bool CanUpdate
        {
            get { return _CanUpdate; }
            set { _CanUpdate = value; }
        }
        public bool CanDelete
        {
            get
            {
                return _CanDelete;
            }
            set { _CanDelete = value; }
        }
        public DateTime CreatedDate
        {
            get { return _CreatedDate; }
            set { _CreatedDate = value; }
        }
        public int CreatedBy
        {
            get { return _CreatedBy; }
            set { _CreatedBy = value; }
        }
        public DateTime UpdatedDate
        {
            get { return _UpdatedDate; }
            set { _UpdatedDate = value; }
        }
        public int UpdatedBy
        {
            get { return _UpdatedBy; }
            set { _UpdatedBy = value; }
        }
        public IScreen Screen
        {
            get { return _Screen; }
            set { _Screen = value; }
        }
    }
}
