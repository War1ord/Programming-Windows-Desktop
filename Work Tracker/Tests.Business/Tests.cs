using Microsoft.VisualStudio.TestTools.UnitTesting;
using Work_Tracker.Business;
using Work_Tracker.Business.Models;

namespace Tests.Business
{
    [TestClass]
    public class Tests
    {
        /// <summary>
        /// Works the tracker service_ add new work and steps_ no errors.
        /// </summary>
        [TestMethod]
        public void WorkTrackerService_AddNewWorkAndSteps_NoErrors()
        {
            using (var workTrackingService = WorkTrackerService.Default
                .Load()
                .Add(new WorkItem { Description = "Regular Job1" })
                .Add(new WorkItem { Description = "Regular Job2" }))
            {
                //Arrange
                //Act
                foreach (var work in workTrackingService.WorkItems)
                {
                    using (var workItemService = work.GetService())
                    {
                        workItemService
                            .AddDefaultCheck(new Check
                            {
                                Description = "if I have packed by laptop"
                            })
                            .AddDefaultCheck(new Check
                            {
                                Description = "if I have packed by lunch"
                            })
                            .AddStep(new Step
                            {
                                Description = "Wakeup"
                            })
                            .AddStep(new Step
                            {
                                Description = "Go to work"
                            });
                        foreach (var step in work.Steps)
                        {
                            using (var stepService = step.GetService())
                            {
                                stepService
                                    .Add(new Check
                                    {
                                        Description = "if I have packed by laptop"
                                    })
                                    .Add(new Check
                                    {
                                        Description = "if I have packed by lunch"
                                    })
                                    .Add(new Extra
                                    {
                                        Name = "test 1", Type = "test type 1", Value = "test value 1"
                                    })
                                    .Add(new Extra
                                    {
                                        Name = "test 2", Type = "test type 2", Value = "test value 2"
                                    }); 
                            }
                        }
                    }
                }
                //Assert
                Assert.IsTrue(workTrackingService.WorkItems.Count > 0); 
                Assert.IsTrue(workTrackingService.Result.Type != ResultType.Error); 
            }
        }

        [TestMethod]
        public void WorkTrackerService_LoadAddAndSaveNewWorkAndSteps_NoErrors()
        {
            using (var workTrackingService = WorkTrackerService.Default
                .Load()
                .Add(new WorkItem { Description = "Regular Job1" })
                .Add(new WorkItem { Description = "Regular Job2" }))
            {
                //Arrange
                //Act
                foreach (var work in workTrackingService.WorkItems)
                {
                    using (var workItemService = work.GetService())
                    {
                        workItemService
                            .AddDefaultCheck(new Check
                            {
                                Description = "if I have packed by laptop"
                            })
                            .AddDefaultCheck(new Check
                            {
                                Description = "if I have packed by lunch"
                            })
                            .AddStep(new Step
                            {
                                Description = "Wakeup"
                            })
                            .AddStep(new Step
                            {
                                Description = "Go to work"
                            });
                        workItemService.Save();
                        foreach (var step in work.Steps)
                        {
                            using (var stepService = step.GetService())
                            {
                                stepService
                                    .Add(new Check
                                    {
                                        Description = "if I have packed by laptop"
                                    })
                                    .Add(new Check
                                    {
                                        Description = "if I have packed by lunch"
                                    })
                                    .Add(new Extra
                                    {
                                        Name = "test 1",
                                        Type = "test type 1",
                                        Value = "test value 1"
                                    })
                                    .Add(new Extra
                                    {
                                        Name = "test 2",
                                        Type = "test type 2",
                                        Value = "test value 2"
                                    });
                                stepService.Save();
                            }
                        }
                    }
                }
                //Assert
                Assert.IsTrue(workTrackingService.WorkItems.Count > 0);
                Assert.IsTrue(workTrackingService.Result.Type != ResultType.Error);
            }
        }

        /// <summary>
        /// creates the database if not exists.
        /// </summary>
        [TestMethod]
        public void Database_Init()
        {
            Database.Init();
        }
    }
}
