using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CmdCommands.Tests
{
    [TestClass]
    public class ViewModelTests
    {
        [TestMethod]
        public void SaveDirCommandsToReg()
        {
            var model = new ViewModels.MainViewModel();
            var dirCommands = new string[]
            {
                @"dir c:\",
                @"dir c:\Windows",
                @"dir d:\",
            };
            model.Set("dir", dirCommands);
            model.Save();
        }

        [TestMethod]
        public void GetCommandsFromReg()
        {
            var model = new ViewModels.MainViewModel();
            Assert.IsTrue(model.Collection.Any(), "No keys in reg.");
        }
    }
}
