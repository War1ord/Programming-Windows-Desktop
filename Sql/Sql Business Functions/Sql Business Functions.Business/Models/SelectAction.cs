namespace Sql_Business_Functions.Business.Models
{
    public class SelectAction
    {
        #region fields
        private Email _email;
        private EmailSetting _emailSetting; 
        #endregion

        public ActionSelectType Type { get; set; }

        public EmailSetting EmailSetting
        {
            get { return _emailSetting ?? (new EmailSetting()); }
            set { _emailSetting = value; }
        }
        public Email Email
        {
            get { return _email ?? (_email = new Email()); }
            set { _email = value; }
        }

        public string File_Path { get; set; }
        public string Table_Or_View_Name { get; set; }
        public string[] Columns { get; set; }
        public Filter[] Filters { get; set; }
        public string[] Order_By_Columns { get; set; }
    }
}