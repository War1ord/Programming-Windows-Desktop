namespace Sql_Business_Functions.Business.Models
{
    public class Filter
    {
        public string Column_Name { get; set; }
        public FilterType Type { get; set; }
        public object Value { get; set; }
    }
}