using System;
using FoldersLinkRightClickMenu.Helpers;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.Win32;

namespace UI.Test
{
    [TestClass]
    public class RegistryTests
    {
        [TestMethod]
        public void RegisrtyKey_Create_Test()
        {
            //Windows Registry Editor Version 5.00

            //[HKEY_CLASSES_ROOT\Folder\shell\ListFilesFoldersExpress]
            //@="List Files...."

            //[HKEY_CLASSES_ROOT\Folder\shell\ListFilesFoldersExpress\command]
            //@="C:\\Program Files (x86)\\ListFilesFoldersExpress\\ListFilesFoldersExpress.exe \" %1\""

            {
                RegistryKey key;
                key = Registry.ClassesRoot.CreateSubKey(@"Folder\shell\Folders Link Right Click Menu");
                if (key != null)
                {
                    key.SetValue("", "Create Link Folder To...");
                    key.Close();
                }
            }
            {
                RegistryKey key;
                key = Registry.ClassesRoot.CreateSubKey(@"Folder\shell\Folders Link Right Click Menu\command");
                if (key != null)
                {
                    key.SetValue("", AssemblyHelpers.AssemblyFilePath + " \" %0\"");
                    key.Close();
                }
            }
        }
        [TestMethod]
        public void RegisrtyKey_CreateDirectorBackground_Test()
        {
            //Windows Registry Editor Version 5.00

            //[HKEY_CLASSES_ROOT\Folder\shell\ListFilesFoldersExpress]
            //@="List Files...."

            //[HKEY_CLASSES_ROOT\Folder\shell\ListFilesFoldersExpress\command]
            //@="C:\\Program Files (x86)\\ListFilesFoldersExpress\\ListFilesFoldersExpress.exe \" %1\""

            {
                RegistryKey key;
                key = Registry.ClassesRoot.CreateSubKey(@"Directory\Background\shell\Folders Link Right Click Menu");
                if (key != null)
                {
                    key.SetValue("", "... Create Link Folder To ...");
                    key.Close();
                }
            }
            {
                RegistryKey key;
                key = Registry.ClassesRoot.CreateSubKey(@"Directory\Background\shell\Folders Link Right Click Menu\command");
                if (key != null)
                {
                    key.SetValue("", AssemblyHelpers.AssemblyFilePath + " \" %0\"");
                    key.Close();
                }
            }
        }
    }
}
