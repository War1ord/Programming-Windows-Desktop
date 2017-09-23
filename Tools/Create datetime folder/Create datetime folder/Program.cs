using Microsoft.Win32;
using System;
using System.Linq;
using System.Reflection;

namespace Create_datetime_folder
{
    public static class Program
    {
        public static void Main(string[] args)
        {
            SetRegistry();
            if (args != null && args.Any())
            {
                var path = args.FirstOrDefault();
                if (!string.IsNullOrEmpty(path))
                {
                    if (System.IO.Directory.Exists(path))
                    {
                        try
                        {
                            System.IO.Directory.CreateDirectory(System.IO.Path.Combine(path, DateTime.Now.ToString("yyyy-MM-dd HHmmss")));
                        }
                        catch (Exception e)
                        {
                            
                        }
                    }
                }
            }
        }

        private static void SetRegistry()
        {
            var location = Assembly.GetExecutingAssembly().Location;
            //Windows Registry Editor Version 5.00

            //[HKEY_CLASSES_ROOT\Folder\shell\ListFilesFoldersExpress]
            //@="List Files...."

            //[HKEY_CLASSES_ROOT\Folder\shell\ListFilesFoldersExpress\command]
            //@="C:\\Program Files (x86)\\ListFilesFoldersExpress\\ListFilesFoldersExpress.exe \" %1\""

            {
                RegistryKey key = Registry.ClassesRoot.CreateSubKey(@"Folder\shell\Create datetime folder");
                if (key != null)
                {
                    key.SetValue("", "Create datetime folder...");
                    key.Close();
                }
            }
            {
                RegistryKey key = Registry.ClassesRoot.CreateSubKey(@"Folder\shell\Create datetime folder\command");
                if (key != null)
                {
                    key.SetValue("", location + " \" %0\"");
                    key.Close();
                }
            }

            {
                RegistryKey key = Registry.ClassesRoot.CreateSubKey(@"Directory\Background\shell\Create datetime folder");
                if (key != null)
                {
                    key.SetValue("", "Create datetime folder...");
                    key.Close();
                }
            }
            {
                RegistryKey key = Registry.ClassesRoot.CreateSubKey(@"Directory\Background\shell\Create datetime folder\command");
                if (key != null)
                {
                    key.SetValue("", "\"" + location + "\" " + " \"%V\"");
                    key.Close();
                }
            }
        }
    }
}
