using Microsoft.Win32;
using System;
using System.IO;
using System.Linq;
using System.Reflection;

namespace Create_datetime_folder
{
	public class Program
	{
		internal void SetRegistry()
		{
			var location = Assembly.GetExecutingAssembly().Location;
			//Windows Registry Editor Version 5.00

			//[HKEY_CLASSES_ROOT\Folder\shell\ListFilesFoldersExpress]
			//@="List Files...."

			//[HKEY_CLASSES_ROOT\Folder\shell\ListFilesFoldersExpress\command]
			//@="C:\\Program Files (x86)\\ListFilesFoldersExpress\\ListFilesFoldersExpress.exe \" %1\""

			//{
			//	RegistryKey key = Registry.ClassesRoot.CreateSubKey(@"Folder\shell\Backup selected files");
			//	if (key != null)
			//	{
			//		key.SetValue("", "Backup selected files");
			//		key.Close();
			//	}
			//}
			//{
			//	RegistryKey key = Registry.ClassesRoot.CreateSubKey(@"Folder\shell\Backup selected files\command");
			//	if (key != null)
			//	{
			//		key.SetValue("", location + " \" %0\"");
			//		key.Close();
			//	}
			//}
			using (RegistryKey key = Registry.ClassesRoot.CreateSubKey(@"*\shell\Backup selected files"))
			{
				if (key != null)
				{
					key.SetValue("", "Backup selected files");
				}
			}
			using (RegistryKey key = Registry.ClassesRoot.CreateSubKey(@"*\shell\Backup selected files\command"))
			{
				if (key != null)
				{
					key.SetValue("", "\"" + location + "\" " + " \"%V\"");
				}
			}
		}
		internal void Process(string[] args)
		{

			if (args != null && args.Any())
			{
				var isError = false;
				var now = DateTime.Now;
				string backupPath = Path.Combine("Backup", now.ToString("yyyy-MM-dd HHmm"));
				//NOTE: these files might not be in the same directory, maybe if the user used the file manager search function and selected folders, then clicked on backup selected files. 
				// get file list
				var files = args.Where(i => !string.IsNullOrEmpty(i)).ToList();
				// loop over files
				foreach (var sourceFile in files)
				{
					try
					{
						// check if backup directory path exists, if not create
						var fileName = Path.GetFileName(sourceFile);
						var sourcePath = sourceFile.Replace(fileName, "");
						var destinationPath = Path.Combine(sourcePath, backupPath);
						var destinationFile = Path.Combine(destinationPath, fileName);
						if (!Directory.Exists(destinationPath))
						{
							Directory.CreateDirectory(destinationPath);
						}
						// copy file
						File.Copy(sourceFile, destinationFile);
					}
					catch (Exception e)
					{
						isError = true;
						Console.WriteLine(string.Format(@"{0} : Error with file : {1} : {2} : ", DateTime.Now, sourceFile, e.Message));
					}
				}
				//var path = args.FirstOrDefault();
				//if (!string.IsNullOrEmpty(path))
				//{
				//	if (System.IO.Directory.Exists(path))
				//	{
				//		try
				//		{
				//			System.IO.Directory.CreateDirectory(System.IO.Path.Combine(path, DateTime.Now.ToString("yyyy-MM-dd HHmmss")));
				//		}
				//		catch{}
				//	}
				//}
				if (isError) Console.Read();
			}
		}

	}
}
