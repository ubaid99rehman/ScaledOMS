using OMS.Core.Models.Permissions;
using System;
using System.Collections.Generic;

namespace OMS.Core.Models.App
{
    public class Screen : BaseModel, IScreen
    {
        private int _ScreenID;
        private string _ScreenName;
        private string _ScreenDescription;
        private DateTime _CreatedDate;
        private int _CreatedBy;
        private DateTime _UpdatedDate;
        private int _UpdatedBy;
        private ICollection<IPermission> _Permissions;

        public int ScreenID
        {
            get { return _ScreenID; }
            set { _ScreenID = value; }
        }
        public string ScreenName
        {
            get { return _ScreenName; }
            set { _ScreenName = value; }
        }
        public string ScreenDescription
        {
            get { return _ScreenDescription; }
            set { _ScreenDescription = value; }
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

        public ICollection<IPermission> Permissions
        {
            get => _Permissions;
            set => _Permissions = value;
        }

    }
}
