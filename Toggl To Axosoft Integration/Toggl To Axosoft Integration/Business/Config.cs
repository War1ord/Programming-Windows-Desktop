using System.Configuration;

namespace Toggl_To_Axosoft_Integration.Business
{
    public static class Config
    {
        #region Private Fields

        private static string _toggleApiKey;
        private static string _axosoftApiKey;

        #endregion

        public static string ToggleApiKey
        {
            get { return !string.IsNullOrWhiteSpace(_toggleApiKey) ? _toggleApiKey : (_toggleApiKey = ConfigurationManager.AppSettings.Get("ToggleApiKey")); }
            set { _toggleApiKey = value; }
        }

        public static string AxosoftApiKey
        {
            get { return !string.IsNullOrWhiteSpace(_axosoftApiKey) ? _axosoftApiKey : (_axosoftApiKey = ConfigurationManager.AppSettings.Get("AxosoftApiKey")); }
            set { _axosoftApiKey = value; }
        }
    }
}