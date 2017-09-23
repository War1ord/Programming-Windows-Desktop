using System;
using Business;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Data.Tests
{
    [TestClass]
    public class DataContextCreate
    {
        protected bool DbOnline { get; set; }

        [TestMethod]
        public void DataContextCreateDatabase()
        {
			DataContextDbCreate.Initialize();
			DbOnline = DataContextDbCreate.Initialized;
            Assert.IsTrue(DbOnline, "DataBase did not create successfully.");
        }
    }
}
