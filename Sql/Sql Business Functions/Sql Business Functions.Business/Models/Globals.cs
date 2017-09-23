using System.Collections.Generic;

namespace Sql_Business_Functions.Business.Models
{
    public static class Globals
    {
        private static List<Global> _list;

        public static List<Global> List
        {
            get { return _list ?? (_list = new List<Global>()); }
            set { _list = value; }
        }
    }
}