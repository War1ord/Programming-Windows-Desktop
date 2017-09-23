using Microsoft.Win32;
using System.Linq;
using System.Reflection;

namespace Folder_To_List
{
	public static class RegistrySettings
	{
		public static void ConfigFolderRightClickMenu()
		{
			var executingAssembly = Assembly.GetExecutingAssembly();
			var location = executingAssembly.Location;
			var applicationName = executingAssembly.FullName.Split(',').First();
			//Windows Registry Editor Version 5.00

			//[HKEY_CLASSES_ROOT\Folder\shell\ListFilesFoldersExpress]
			//@="List Files...."

			//[HKEY_CLASSES_ROOT\Folder\shell\ListFilesFoldersExpress\command]
			//@="C:\\Program Files (x86)\\ListFilesFoldersExpress\\ListFilesFoldersExpress.exe \" %1\""

			using (var key = Registry.ClassesRoot.CreateSubKey(string.Format(@"Folder\shell\{0}", applicationName)))
			{
				if (key != null)
				{
					key.SetValue("", "Folder To List");
				}
			}
			using (var key = Registry.ClassesRoot.CreateSubKey(string.Format(@"Folder\shell\{0}\command", applicationName)))
			{
				if (key != null)
				{
					key.SetValue("", "\"" + location + "\" " + " \"%V\"");
				}
			}
			using (var key = Registry.ClassesRoot.CreateSubKey(string.Format(@"Directory\Background\shell\{0}", "Folder To List")))
			{
				if (key != null)
				{
					key.SetValue("", applicationName);
				}
			}
			using (var key = Registry.ClassesRoot.CreateSubKey(string.Format(@"Directory\Background\shell\{0}\command", applicationName)))
			{
				if (key != null)
				{
					key.SetValue("", "\"" + location + "\" " + " \"%V\"");
				}
			}
		}
	}
}