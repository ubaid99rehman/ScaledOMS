using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OMS.Core.Models.User
{
    public class SessionInfo : INotifyPropertyChanged
    {
        public SessionInfo() 
        {
            ConnectionStatus = "Connected!";
            Ping = "0ms";
            SessionTime = "00:00:00";
            LoginTime = DateTime.Now;
        }

        private string _connectionstatus;
        public string ConnectionStatus
        {
            get { return _connectionstatus; }
            set 
            {
                if (value != _connectionstatus)
                {
                    _connectionstatus = value;
                    OnPropertyChanged(nameof(ConnectionStatus)); 
                }
            }
        }

        private string _ping;
        public string Ping
        {
            get { return _ping; }
            set 
            {
                if (value != _ping)
                {
                    _ping = value;
                    OnPropertyChanged(nameof(Ping)); 
                }
            }
        }

        private string _sessionTime;
        public string SessionTime
        {
            get { return _sessionTime; }
            set 
            { 
                if(_sessionTime != value)
                {
                    _sessionTime = value;
                    OnPropertyChanged(nameof(SessionTime)); 
                }
            }
        }

        private DateTime _loginTime;
        public DateTime LoginTime
        {
            get { return _loginTime; }
            set 
            { 
                if (_loginTime != value)
                {
                    _loginTime = value;
                    OnPropertyChanged(nameof(LoginTime)); 
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
