namespace Sql_Business_Functions.Business.Models
{
    public class InsertAction
    {
        public string Table_Name { get; set; }
        public Column[] Columns { get; set; }
        public string Return_Column_Name { get; set; }
    }
}