using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ViewModels;

namespace UI.Test
{
	[TestClass]
	public class ViewModelTests
	{
		[TestMethod]
		public void MainViewModel_CreateLink_Test()
		{
			var model = new MainWindowViewModel { LinkFolder = @"C:\linkTest", TargetFolder = @"C:\" };
			model.CreateCommand.Execute(null);
			Assert.IsTrue(model.IsValid, "The Folders is not valid.");
		}
	}
}
