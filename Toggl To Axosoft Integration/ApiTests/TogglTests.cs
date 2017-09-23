using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Toggl.QueryObjects;
using Toggl.Services;
using Toggl_To_Axosoft_Integration.Business.Api.Toggl;
using Toggl_To_Axosoft_Integration.Extentions;
using Toggl_To_Axosoft_Integration.Business;
using Toggl_To_Axosoft_Integration.Models;

namespace ApiTests
{
    [TestClass]
    public class TogglTests
    {
        [TestMethod]
        public void TestTogglApiAssemble()
        {
            var hours = new TimeEntryService(ConfigurationManager.AppSettings.Get("ToggleApiKey"))
                .List(new TimeEntryParams
                {
                    StartDate = DateTime.Now.AddDays(-7),
                    EndDate = DateTime.Now,
                })
                .Where(w => !string.IsNullOrEmpty(w.Description))
                .ToList();

            Assert.IsTrue(hours.Any());
        }

        [TestMethod]
        public void Business2TestTogglApi()
        {
            var hours = TogglTimeEntriesService.GetTogglCalculatedDateEntries(new DateRange(DateTime.Now.AddDays(-7), DateTime.Now));
            Assert.IsTrue(hours != null && hours.Any());
        }

        [TestMethod]
        public void TogglProjects_GetList_List()
        {
            //Arrange
            //Act
            var projects = new ProjectService(Config.ToggleApiKey).List();

            //Assert
            Assert.IsTrue(projects.Any());
        }

    }
}
