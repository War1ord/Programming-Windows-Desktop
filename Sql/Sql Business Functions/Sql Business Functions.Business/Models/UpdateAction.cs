namespace Sql_Business_Functions.Business.Models
{
    public class UpdateAction
    {
        public string Table_Name { get; set; }
        public Column[] Columns { get; set; }
        public Filter[] Filters { get; set; }
    }
}