using System;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace Folder_To_List
{
	public static class Program
	{
		[STAThread]
		public static void Main(string[] args)
		{
			RegistrySettings.ConfigFolderRightClickMenu();
			if (args != null && args.Length > 0 && !string.IsNullOrEmpty(args[0]))
			{
				using (var dialog = new SaveFileDialog
				{
					Filter = "Excel file(*.xlsx)|*.xlsx",
					InitialDirectory = Path.GetDirectoryName(args[0]),
					FileName = Path.GetFileName(args[0]),
				})
				{
					var result = dialog.ShowDialog();
					if (result == DialogResult.OK || result == DialogResult.Yes)
					{
						var list = FolderToData.GetList(args[0]);
						if (list != null && list.Any())
						{
							try
							{
								var excel = FolderToData.BuildExcelFile(list);
								var fileName = dialog.FileName;
								File.WriteAllBytes(fileName, excel);
							}
							catch (Exception e)
							{
								MessageBox.Show(string.Format("There was a problem building or saving the excel file.{1}Message: {0}"
										, e.Message, Environment.NewLine),
									"Error!",
									MessageBoxButtons.OK,
									MessageBoxIcon.Error);
							}
						}
						else
						{
							MessageBox.Show("No files on folder",
									"Error!",
									MessageBoxButtons.OK,
									MessageBoxIcon.Error);
						}
					}
				}
			}
		}
	}
}
