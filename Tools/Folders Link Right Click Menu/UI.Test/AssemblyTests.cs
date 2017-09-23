using System;
using System.IO;
using System.Reflection;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace UI.Test
{
    [TestClass]
    public class AssemblyTests
    {
        [TestMethod]
        public void ExecutingAssembly_FullPath_Get()
        {
            var fullpath = Assembly.GetExecutingAssembly().Location;
        }
    }
}
