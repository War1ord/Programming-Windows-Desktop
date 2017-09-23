using OfficeOpenXml;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Folder_To_List
{
	public class FolderToData
	{
		public static FolderData[] GetList(string path)
		{
			var list = new List<string> { path };
			list.AddRange(Directory.GetDirectories(path, "*", SearchOption.AllDirectories));
			list.AddRange(list.SelectMany(Directory.GetFiles).ToList());
			return list.Select(f => new FolderData
			{
				file = f,
				array = f.Split('\\'),
			}).ToArray();
		}

		public static byte[] BuildExcelFile(FolderData[] list)
		{
			using (var stream = new MemoryStream())
			using (var excel = new ExcelPackage(stream))
			using (var sheet = excel.Workbook.Worksheets.Add("Folder Data"))
			{
				sheet.Cells[1, 1].LoadFromArrays(list.Select(i => i.array).ToArray());
				sheet.Cells.AutoFitColumns();
				return excel.GetAsByteArray();
			}
		}
	}

	public class FolderData
	{
		public string file { get; set; }
		public string[] array { get; set; }
	}
}