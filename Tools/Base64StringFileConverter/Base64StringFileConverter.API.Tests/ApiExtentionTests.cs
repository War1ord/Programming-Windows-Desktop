using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.IO;

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

		}
	}
}
