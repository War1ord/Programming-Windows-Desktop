using System;

namespace Sql_Business_Functions.Business.Models
{
    public class SqlAction
    {
        #region Fields
        private DateTime? _created;
        private ActionLink _link;
        private ConnectionSetting _connection;
        private string _sql;
        #endregion

        public SqlAction()
        {
            _created = DateTime.Now;
        }

        public ConnectionSetting Connection
        {
            get { return _connection ?? (_connection = new ConnectionSetting()); }
            set { _connection = value; }
        }

        public ActionLink Link
        {
            get { return _link ?? (_link = new ActionLink()); }
            set { _link = value; }
        }

        public string Sql
        {
            get { return _sql ?? (_sql = string.Empty); }
            set { _sql = value; }
        }

        public DateTime Created
        {
            get { return (_created ?? (_created = DateTime.Now)).Value; }
            set { _created = value; }
        }
    }
}