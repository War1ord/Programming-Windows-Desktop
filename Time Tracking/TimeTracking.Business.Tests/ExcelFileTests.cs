using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace TimeTracking.Business.Tests
{
	[TestClass]
	public class ExcelFileTests
	{
		[TestMethod]
		public void Load_Normal_NoErrors()
		{
			//Arrange
			new Excel.ExcelFile().Load(@"E:\SvnProjects\trunk\Windows\Time Tracking\NJA_Project_TimeLogging.xlsx");
			//Act

			//Assert

		}
	}
}
