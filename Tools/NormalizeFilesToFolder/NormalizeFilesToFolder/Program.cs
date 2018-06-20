﻿using System;
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
		static readonly ConsoleColor defaultBackgroundColor = BackgroundColor;
		static readonly ConsoleColor defaultForegroundColor = ForegroundColor;

		public static void Main(string[] args)
		{
			WriteLine("Starting Normalize Files to Folder...");
			// Set Folder Menu Context in registry
			SetRegistry();
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
			var parts = Get3MostLastPathParts(file);
			// > Rebuild the filename to new filename
			var newFilename = string.Join(" - ", parts);
			WriteLine("New Filename: {0}", newFilename);
			// > move \ rename file to root folder path
			var SourceFileName = file;
			var DestFileName = Path.Combine(folderpath, newFilename);
			File.Move(SourceFileName, DestFileName);
		}

		private static string[] Get3MostLastPathParts(string file)
		{
			const int MostLastCountOfParts = 3;
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

			//[HKEY_CLASSES_ROOT\Folder\shell\ListFilesFoldersExpress]
			//@="List Files...."

			//[HKEY_CLASSES_ROOT\Folder\shell\ListFilesFoldersExpress\command]
			//@="C:\\Program Files (x86)\\ListFilesFoldersExpress\\ListFilesFoldersExpress.exe \" %1\""

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
					key.SetValue("", executingassembly.Location + " \" %0\"");
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
					key.SetValue("", "\"" + executingassembly.Location + "\" " + " \"%V\"");
					key.Close();
				}
			}
		}
	}
}
