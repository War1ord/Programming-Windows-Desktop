using System.Reflection;
using Microsoft.Win32;

namespace Windows_Run_Shortcodes
{
    public static class RegistrySettings
    {
        public static void ConfigExeFileRightClickMenu()
        {
            var executingAssembly = Assembly.GetExecutingAssembly();
            var location = executingAssembly.Location;
            var applicationName = executingAssembly.FullName.Split(',')[0];
            //Windows Registry Editor Version 5.00

            //[HKEY_CLASSES_ROOT\Folder\shell\ListFilesFoldersExpress]
            //@="List Files...."

            //[HKEY_CLASSES_ROOT\Folder\shell\ListFilesFoldersExpress\command]
            //@="C:\\Program Files (x86)\\ListFilesFoldersExpress\\ListFilesFoldersExpress.exe \" %1\""

            using (var key = Registry.ClassesRoot.CreateSubKey(string.Format(@"exefile\shell\{0}", applicationName)))
            {
                if (key != null)
                {
                    key.SetValue("", applicationName);
                    key.Close();
                }
            }
            using (var key = Registry.ClassesRoot.CreateSubKey(string.Format(@"exefile\shell\{0}\command", applicationName)))
            {
                if (key != null)
                {
                    key.SetValue("", location + " \" %0\"");
                    key.Close();
                }
            }
        }
    }
}