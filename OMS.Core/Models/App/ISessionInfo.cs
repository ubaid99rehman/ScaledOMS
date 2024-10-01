using System;

namespace OMS.Core.Models.User
{
    public interface ISessionInfo
    {
        string ConnectionStatus { get; set; }
        string Ping {  get; set; }
        string SessionTime {  get; set; }
        DateTime LoginTime {  get; set; }
    }
}
