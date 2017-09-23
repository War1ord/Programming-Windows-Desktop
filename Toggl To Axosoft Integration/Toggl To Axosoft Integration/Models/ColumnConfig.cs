using System;
using System.Collections.Generic;
using Toggl_To_Axosoft_Integration.Extentions;

namespace Toggl_To_Axosoft_Integration.Models
{
    public class ColumnConfig
    {
        public List<Column> Columns { get; set; }
    }

    public class Column
    {
        public string Header { get; set; }
        public string DataField { get; set; }

        public DateTime AsDate()
        {
            return Header.AsDateTime();
        }
    }

}