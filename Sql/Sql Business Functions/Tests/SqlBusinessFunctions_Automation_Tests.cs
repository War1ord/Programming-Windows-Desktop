using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Sql_Business_Functions.Business;
using Sql_Business_Functions.Business.Models;
using System.Collections.Generic;

namespace Tests
{
    [TestClass]
    public class SqlBusinessFunctions_Automation_Tests
    {
        [TestMethod]
        public void Run_SelectDataTable_SuccessResult()
        {
            //Arrange
            var business = new SqlBusinessFunctions
            {
                Script = 
                {
                    Actions = 
                    {
                        new SqlAction
                        {
                            Connection = 
                            {
                                Server_Ip_Or_Name = @"(localdb)\v11.0",
                                Database_Name = @"master",
                                //Username = @"sa",
                                //Password = @"Enterlol123123_+",
                                Integrated_Security = true,
                            },
                            Link =
                            {
                                Type = ActionType.Select,
                                Select =
                                {
                                    Type = ActionSelectType.Normal,
                                    Table_Or_View_Name = "sys.objects",
                                    Columns = new []
                                    {
                                        "object_id",
                                        "name",
                                    }
                                }
                            },
                        },
                    }
                }
            };
            //Act
            var results = business.Run();
            //Assert
            Assert.IsTrue(results.TrueForAll(i => i.IsSuccessful), "some or all sql action did not run successful");
        }
    }
}
