using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace TimeTracking.Business.Tests
{
	[TestClass]
	public class WorkItemsTests
	{
		[TestMethod]
		public void Add_New_NoErrors()
		{
			//Arrange
			var item = new TimeTracking.Models.WorkItem
			{
				StartDateTime = DateTime.Now,
				Description = "Work Done",
			};
			//Act
			var result = new Business.WorkItems().Start(item);
			//Assert
			Assert.IsTrue(result.IsSuccessful, result.Message);
		}
	}
}
