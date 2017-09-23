using Microsoft.VisualStudio.TestTools.UnitTesting;
using RulesEngine.Models;
using TestApplication.Data;

namespace Tests.TestApplication.Data
{
	[TestClass]
	public class TestDataHelper
	{
		[TestMethod]
		public void AddAssemblyReferences()
		{
			using (var db = new DataContext())
			{
				db.Set<AssemblyReference>().Add(
					new AssemblyReference("PresentationFramework.dll", "WPF Framework"));
				db.SaveChanges();
			}
		}
	}
}
