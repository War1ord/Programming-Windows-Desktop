using System.Linq;

namespace Sql_Business_Functions.Business
{
    public static class ExcelHelpers
    {
        public static byte[] BuildExcelFile(System.Data.DataTable dataTable, string dataTableName)
        {
            var file = new byte[0];
            string[] dataTableNames = null;
            if (!string.IsNullOrEmpty(dataTableName))
            {
                dataTableNames = dataTableName.Split(new[]
                    {
                        System.Environment.NewLine, ",", ";", ":", "|"
                    }, System.StringSplitOptions.RemoveEmptyEntries);
            }
            if (dataTable != null)
            {
                using (var memory = new System.IO.MemoryStream())
                using (var excel = new OfficeOpenXml.ExcelPackage(memory))
                {
                    var sheet = excel.Workbook.Worksheets.Add(dataTable.TableName);
                    sheet.Cells[1, 1].LoadFromDataTable(dataTable, true);
                    for (int c = 0; c < dataTable.Columns.Count; c++)
                    {
                        if (dataTable.Columns[c].DataType.FullName == typeof(System.DateTime).FullName)
                        {
                            sheet.Column(c + 1).Style.Numberformat.Format = "yyyy-mm-dd HH:mm:ss";
                        }
                    }
                    sheet.Cells.AutoFitColumns(10);
                    file = excel.GetAsByteArray(); // use GetAsByteArray instead of Save because of use of MemoryStream and not FileInfo or FileStream.
                }
            }
            return file;
        }
    }
}