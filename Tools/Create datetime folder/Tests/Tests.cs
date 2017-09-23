using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Tests
{
    [TestClass]
    public class Tests
    {
        [TestMethod]
        public void Test_Program()
        {
            Create_datetime_folder.Program.Main(new[] { @"\\KUPERUS-SERVER\Share" });
        }
    }
}
