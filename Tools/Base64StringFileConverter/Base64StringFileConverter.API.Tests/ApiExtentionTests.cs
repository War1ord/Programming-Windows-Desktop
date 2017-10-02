using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.IO;
using System.Linq;

namespace Base64StringFileConverter.API.Tests
{
	[TestClass]
	public class ApiExtentionTests
	{
		[TestMethod]
		public void Read_All_TestDataFile_To_List()
		{
			var testDataFilesPath =
				Path.Combine(
					Environment.CurrentDirectory,
					"TestDataFiles"
				);
			var list = Directory
				.GetFiles(testDataFilesPath, "*.*", SearchOption.AllDirectories)
				.Select(file => new
				{
					Info = new FileInfo(file),
					Content = File.ReadAllBytes(file).ToBase64String(),
				})
				.ToArray();
		}
	}
}
