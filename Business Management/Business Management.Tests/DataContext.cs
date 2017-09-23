using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;

namespace Business_Management.Tests
{
	[TestClass]
	public class DataContext
	{
		[TestMethod]
		public void TestMethod1()
		{
			using (var db = Data.DataContext.Instance)
			{
				var banks = db.Banks.ToList();
			}
		}
	}
}
