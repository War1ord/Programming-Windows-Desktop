using System.Collections.Generic;

namespace Sql_Business_Functions.Business.Models
{
    public class Script
    {
        private List<SqlAction> _actions;

        public List<SqlAction> Actions
        {
            get { return _actions ?? (_actions = new List<SqlAction>()); }
            set { _actions = value; }
        }
    }
}
