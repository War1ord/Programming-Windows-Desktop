using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;

namespace Folder_Summary
{
	public partial class frmFolderSummary : Form
	{
		private bool isValid = false;
		private long total_size = 0L;
		private int total_file_count = 0;

		public frmFolderSummary()
		{
			InitializeComponent();
			ProcessCommandLineArguments();
		}

		#region Helpers
		private FileInfo[] GetFilesInfo(string[] all_files)
		{
			var list = new List<FileInfo>();
			foreach (var item in all_files)
			{
				list.Add(new FileInfo(item));
			}
			return list.ToArray();
		}
		private long GetFilesSize(FileInfo[] fi_all_files)
		{
			var result = 0L;
			foreach (var item in fi_all_files)
			{
				result += item.Length;
			}
			return result;
		}
		private static string BytesToString(long byteCount)
		{
			string[] suf = { "B", "KB", "MB", "GB", "TB", "PB", "EB" }; //Longs run out around EB
			if (byteCount == 0) return "0" + suf[0];
			long bytes = Math.Abs(byteCount);
			int place = Convert.ToInt32(Math.Floor(Math.Log(bytes, 1024)));
			double num = Math.Round(bytes / Math.Pow(1024, place), 1);
			return (Math.Sign(byteCount) * num).ToString() + suf[place];
		}
		public string GetBytesReadable(long bytesLength)
		{
			// Get absolute value
			long absolute_i = (bytesLength < 0 ? -bytesLength : bytesLength);
			// Determine the suffix and readable value
			string suffix;
			double readable;
			if (absolute_i >= 0x1000000000000000) // Exabyte
			{
				suffix = "EB";
				readable = (bytesLength >> 50);
			}
			else if (absolute_i >= 0x4000000000000) // Petabyte
			{
				suffix = "PB";
				readable = (bytesLength >> 40);
			}
			else if (absolute_i >= 0x10000000000) // Terabyte
			{
				suffix = "TB";
				readable = (bytesLength >> 30);
			}
			else if (absolute_i >= 0x40000000) // Gigabyte
			{
				suffix = "GB";
				readable = (bytesLength >> 20);
			}
			else if (absolute_i >= 0x100000) // Megabyte
			{
				suffix = "MB";
				readable = (bytesLength >> 10);
			}
			else if (absolute_i >= 0x400) // Kilobyte
			{
				suffix = "KB";
				readable = bytesLength;
			}
			else
			{
				return bytesLength.ToString("0 B"); // Byte
			}
			// Divide by 1024 to get fractional value
			readable = (readable / 1024);
			// Return formatted number with suffix
			return readable.ToString("0.### ") + suffix;
		}
		#endregion

		private void ProcessCommandLineArguments()
		{
			try
			{
				var args = Environment.GetCommandLineArgs();
				if (args != null && !string.IsNullOrEmpty(args[0]) && args.Length > 0)
				{
					var root = args.Length <= 1 ? args[0] : args[1];
					Text = root;
					if (Directory.Exists(root))
					{
						var root_info = new DirectoryInfo(root);
						var all_files = Directory.GetFiles(root, "*.*", SearchOption.AllDirectories);
						var all_files_info = GetFilesInfo(all_files);

						var first_level_folders = Directory.GetDirectories(root, "*.*", SearchOption.TopDirectoryOnly);

						/******************************************************************************
						 * Data to extract
						 * - Total Size & file count
						 * - Per first level folders size & file count
						 *****************************************************************************/
						total_size = GetFilesSize(all_files_info);
						total_file_count = all_files.Length;

					}
				}
				isValid = true;
			}
			catch (Exception e)
			{
				MessageBox.Show(this, e.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
				isValid = false;
			}
		}

		private void Form1_Load(object sender, EventArgs e)
		{
			if (!isValid) Environment.Exit(0);
			lblTotalValue.Text = string.Format("{0} - file count {1}", GetBytesReadable(total_size), total_file_count.ToString());
		}

	}
}
