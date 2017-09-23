using System.IO;
using System.Linq;
using System.Windows;
using Common.Helpers;
using FoldersLinkRightClickMenu.Helpers;
using FoldersLinkRightClickMenu.Singletons;
using Microsoft.Win32;

namespace FoldersLinkRightClickMenu
{
	/// <summary>
	/// Interaction logic for App.xaml
	/// </summary>
	public partial class App : Application
	{
		#region Properties

		private string LinkFolder { get; set; }

		#endregion

		#region Overrides

		protected override void OnStartup(StartupEventArgs e)
		{
		    SetRegistry();
			if (e.Args.Any() && IsValid(e.Args[0]))
			{
                var obj = ApplicationSingleton.Instance;
                LinkFolder = e.Args[0];
			    ApplicationSingleton.Instance.LinkFolder = LinkFolder;
			}
			base.OnStartup(e);
		}

	    #endregion

		#region Helpers

	    private void SetRegistry()
	    {
            //Windows Registry Editor Version 5.00

            //[HKEY_CLASSES_ROOT\Folder\shell\ListFilesFoldersExpress]
            //@="List Files...."

            //[HKEY_CLASSES_ROOT\Folder\shell\ListFilesFoldersExpress\command]
            //@="C:\\Program Files (x86)\\ListFilesFoldersExpress\\ListFilesFoldersExpress.exe \" %1\""

            {
                RegistryKey key = Registry.ClassesRoot.CreateSubKey(@"Folder\shell\Folders Link Right Click Menu");
                if (key != null)
                {
                    key.SetValue("", "Create Link Folder To...");
                    key.Close();
                }
            }
            {
                RegistryKey key = Registry.ClassesRoot.CreateSubKey(@"Folder\shell\Folders Link Right Click Menu\command");
                if (key != null)
                {
                    key.SetValue("", AssemblyHelpers.AssemblyFilePath + " \" %0\"");
                    key.Close();
                }
            }

            {
                RegistryKey key; key = Registry.ClassesRoot.CreateSubKey(@"Directory\Background\shell\Folders Link Right Click Menu");
                if (key != null)
                {
                    key.SetValue("", "Create Link Folder To ...");
                    key.Close();
                }
            }
            {
                RegistryKey key = Registry.ClassesRoot.CreateSubKey(@"Directory\Background\shell\Folders Link Right Click Menu\command");
                if (key != null)
                {
                    key.SetValue("", "\"" + AssemblyHelpers.AssemblyFilePath + "\" " + " \"%V\"");
                    key.Close();
                }
            }
        }

	    private bool IsValid(string argumentString)
		{
			try
			{
				return !string.IsNullOrWhiteSpace(argumentString)
				       && Directory.Exists(argumentString);
			}
			catch
			{
				return false;
			}
		}

		#endregion
	}
}
