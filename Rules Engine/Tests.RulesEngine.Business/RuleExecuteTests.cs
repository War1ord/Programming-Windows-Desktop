using Microsoft.VisualStudio.TestTools.UnitTesting;
using RulesEngine.Business;

namespace Tests.RulesEngine.Business
{
	[TestClass]
	public class RuleExecuteTests
	{
		[TestMethod]
		public void TestMethod1()
		{
			var code = @"using System.Windows;
							namespace MyNamespace
							{
								class MyClass
								{
									public void TestMessage()
									{
										System.Windows.MessageBox.Show(""Testing Message"");
									}
								}
							}";
			RunCode.ExecuteCode(code, "MyNamespace", "MyClass", "TestMessage", false, new[] { "PresentationFramework.dll" }, null);
		}
	}


}
