using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Business.Tests
{
	[TestClass]
	public class CmdMkLinkTests
	{
		[TestMethod]
		public void CmdMkLink_Help_Test()
		{ 
			var manager = new CmdManager { CmdString = Commands.Help };
			Assert.IsTrue(manager.IsCmdStringValid, "The Command sent was invalid!");
			manager.Action.Execute(null);
		}
		[TestMethod]
		public void CmdMkLink_Create_Test()
		{
			var cmd = string.Format(Commands.CreateSymbolicLinkStringFormat, @"c:\test", @"c:\");
			var manager = new CmdManager { CmdString = cmd };
			Assert.IsTrue(manager.IsCmdStringValid, "The Command sent was invalid!");
			manager.Action.Execute(null);
		}
	}
}
