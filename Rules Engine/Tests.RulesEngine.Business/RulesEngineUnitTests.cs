using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RulesEngine.Models;

namespace Tests.RulesEngine.Business
{
	[TestClass]
	public class RulesEngineUnitTests
	{
		public RulesEngineUnitTests()
		{
			var instance = global::RulesEngine.Business.RulesEngine.Instance;
		}

		[TestMethod]
		public void AddSomeDummyRulesWithCode()
		{
			#region some code blocks
			var code1 = @"using System.Windows;
				namespace TestingNamespace
				{
					class TestingClass
					{
						public void TestMessage1()
						{
							System.Windows.MessageBox.Show(""Testing Message 1"");
						}
					}
				}";
			var code2 = @"using System.Windows;
				namespace TestingNamespace
				{
					class TestingClass
					{
						public void TestMessage2()
						{
							System.Windows.MessageBox.Show(""Testing Message 2"");
						}
					}
				}";
			var code3 = @"using System.Windows;
				namespace TestingNamespace
				{
					class TestingClass
					{
						public void TestMessage3()
						{
							System.Windows.MessageBox.Show(""Testing Message 3"");
						}
					}
				}";
			var code4 = @"using System.Windows;
				namespace TestingNamespace
				{
					class TestingClass
					{
						public void TestMessage4()
						{
							System.Windows.MessageBox.Show(""Testing Message 4"");
						}
					}
				}"; 
			#endregion

			var reference = new AssemblyReference("PresentationFramework.dll", "WPF Framework");

		}
	}
}
