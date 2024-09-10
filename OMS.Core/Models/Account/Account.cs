using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OMS.Core.Models.Account
{
    public class Account : INotifyPropertyChanged
    {
        public int? _accountID { get; set; }
        public int? AccountID
        {
            get => _accountID;
            set
            {
                if (_accountID != value)
                {
                    _accountID = value;
                    OnPropertyChanged(nameof(AccountID));
                }
            }
        }

        #region Event Handler
        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion
    }
}
