using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OMS.Core.Models.User
{
    public class SessionInfo : BaseModel, ISessionInfo
    {
        #region Private Members
        private string _connectionstatus;
        private string _ping;
        private string _sessionTime;
        private DateTime _loginTime; 
        #endregion

        //Constructor
        public SessionInfo() 
        {
            ConnectionStatus = "Connected!";
            Ping = "0ms";
            SessionTime = "00:00:00";
            LoginTime = DateTime.Now;
        }
        
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
    }
}
