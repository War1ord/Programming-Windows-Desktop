using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TestApplication.Data;

namespace Tests.TestApplication.Data
{
	[TestClass]
	public class DataContextTests
	{
		[TestMethod]
		public void DataContextCreate()
		{
			DataContext.Initialize();
		}
	}
}
