namespace Sql_Business_Functions.Business.Models
{
    public class EmailSetting
    {
        public string Smtp_Serber_Ip_Or_Name { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string FromAddress { get; set; }
    }
}