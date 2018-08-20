using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;

using Microsoft.Win32;

using static System.Console;

namespace NormalizeFilesToFolder
{
    /// <summary>
    /// an Application that finds all files in folder path passed via arguments, and move and rename them to that folder path
    /// </summary>
    internal static class Program
    {
        private const int MostLastCountOfParts = 4;
        private static readonly ConsoleColor defaultBackgroundColor = BackgroundColor;
        private static readonly ConsoleColor defaultForegroundColor = ForegroundColor;

        public static void Main(string[] args)
        {
            // Set Folder Menu Context in registry
            SetRegistry();

            // Confirmation
            WriteLine("Are you sure you want to normalize this folder?");
            var ans = ReadKey();
            if (!(ans.KeyChar.Equals('y') || ans.KeyChar.Equals('Y')))
            {
                return;
            }

            WriteLine("Starting Normalize Files to Folder...");
            // Validate parameters
            if (args == null || args.Length <= 0)
            {
                ForegroundColor = ConsoleColor.Red;
                WriteLine("Invalid Arguments...");
                ForegroundColor = defaultForegroundColor;
                return;
            }
            // Get folder path from arguments
            var folderpath = args[0];
            // Get all files in folder path
            var files = Directory.GetFiles(folderpath, "*.*", SearchOption.AllDirectories);
            // If no files exit application
            if (files == null || files.Length <= 0)
            {
                return;
            }
            WriteLine("{0} Found files...", files.Length);
            // Iterate over all files
            foreach (var file in files)
            {
                try
                {
                    WriteLine("Starting ... Moving and Renaming : {0}", file);
                    MoveRenameFile(file, folderpath);
                    WriteLine("Finished ... Moving and Renaming : {0}", file);
                }
                catch (Exception e)
                {
                    ForegroundColor = ConsoleColor.Red;
                    WriteLine("{0} : {1}", file, e.Message);
                    ForegroundColor = defaultForegroundColor;
                }
            }
        }

        private static void MoveRenameFile(string file, string folderpath)
        {
            // > Get the 3 most last path parts
            var parts = GetMostLastPathParts(file, MostLastCountOfParts);
            // > Rebuild the filename to new filename
            var newFilename = string.Join(" - ", parts);
            WriteLine("New Filename: {0}", newFilename);
            // > move \ rename file to root folder path
            var SourceFileName = file;
            var DestFileName = Path.Combine(folderpath, newFilename);
            File.Move(SourceFileName, DestFileName);
        }

        private static string[] GetMostLastPathParts(string file, int MostLastCountOfParts)
        {
            var result = new List<string>();
            var parts = file.Split('\\');
            var lastparts = new string[0];
            for (var i = parts.Length - MostLastCountOfParts; i < parts.Length; i++)
            {
                result.Add(parts[i]);
            }
            return result.ToArray();
        }

        private static void SetRegistry()
        {
            //Windows Registry Editor Version 5.00

            //[HKEY_CLASSES_ROOT\Folder\shell\<name>]
            //@="List Files...."

            //[HKEY_CLASSES_ROOT\Folder\shell\<name>\command]

            var executingassembly = Assembly.GetExecutingAssembly();
            {
                var key = Registry.ClassesRoot.CreateSubKey(@"Folder\shell\Normalize Files");
                if (key != null)
                {
                    key.SetValue("", "Normalize Files");
                    key.Close();
                }
            }
            {
                var key = Registry.ClassesRoot.CreateSubKey(@"Folder\shell\Normalize Files\command");
                if (key != null)
                {
                    key.SetValue("", $"{executingassembly.Location} \" %0\"");
                    key.Close();
                }
            }
            {
                var key = Registry.ClassesRoot.CreateSubKey(@"Directory\Background\shell\Normalize Files");
                if (key != null)
                {
                    key.SetValue("", "Normalize Files");
                    key.Close();
                }
            }
            {
                var key = Registry.ClassesRoot.CreateSubKey(@"Directory\Background\shell\Normalize Files\command");
                if (key != null)
                {
                    key.SetValue("", $"\"{executingassembly.Location}\"  \"%V\"");
                    key.Close();
                }
            }
        }
    }
}