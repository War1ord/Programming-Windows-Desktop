namespace TimeTracking.Business.Excel
{
	public class ExcelFile
	{
		public void Load(string excelDataFileName)
		{
			foreach (var worksheet in global::Excel.Workbook.Worksheets(excelDataFileName))
			{
				foreach (var row in worksheet.Rows)
				{
					foreach (var cell in row.Cells)
					{
						if (cell != null) // Do something with the cells
						{
							
						}
					}
				}
			}
		}
	}
}