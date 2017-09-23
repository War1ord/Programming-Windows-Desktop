using System.Linq;
using OfficeOpenXml;
using System;
using System.Data;
using System.IO;
    
namespace Business.Helpers
{
    public static class ExcelHelpers
    {
        public static byte[] BuildExcelFile(DataSet dataSet, string datasetnames = null)
        {
            var file = new byte[0];
            string[] dataTableNames = null;
            if (!string.IsNullOrWhiteSpace(datasetnames))
            {
                dataTableNames = datasetnames.Split(new[]
                    {
                        Environment.NewLine, ",", ";", ":", "|"
                    }, StringSplitOptions.RemoveEmptyEntries);
            }
            if (dataSet != null)
            {
                using (var memory = new MemoryStream())
                using (var excel = new ExcelPackage(memory))
                {
                    foreach (DataTable table in dataSet.Tables)
                    {
                        var index = dataSet.Tables.IndexOf(table);
                        var tableName = dataTableNames != null && datasetnames.Any()
                                        ? dataTableNames[index]
                                        : table.TableName;
                        var sheet = excel.Workbook.Worksheets.Add(tableName);
                        sheet.Cells[1, 1].LoadFromDataTable(table, true);
                        for (int c = 0; c < table.Columns.Count; c++)
                        {
                            if (table.Columns[c].DataType.FullName == typeof(DateTime).FullName)
                            {
                                sheet.Column(c + 1).Style.Numberformat.Format = "yyyy-mm-dd HH:mm:ss";
                            }
                        }
                        sheet.Cells.AutoFitColumns(10);
                    }
                    file = excel.GetAsByteArray(); // use GetAsByteArray instead of Save because of use of MemoryStream and not FileInfo or FileStream.
                }
            }
            return file;
        }
    }
}