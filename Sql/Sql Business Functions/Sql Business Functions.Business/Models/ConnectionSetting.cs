namespace Sql_Business_Functions.Business.Models
{
    public class ConnectionSetting
    {
        private string _username;
        private string _password;
        public string Server_Ip_Or_Name { get; set; }
        public string Database_Name { get; set; }
        public string Username
        {
            get { return _username ?? (_username = string.Empty); }
            set { _username = value; }
        }
        public string Password
        {
            get { return _password ?? (_password = string.Empty); }
            set { _password = value; }
        }
        public bool Integrated_Security { get; set; }
    }
}