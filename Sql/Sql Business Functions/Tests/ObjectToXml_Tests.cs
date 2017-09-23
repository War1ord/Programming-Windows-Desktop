using Microsoft.VisualStudio.TestTools.UnitTesting;
using Sql_Business_Functions.Business.Helpers;
using Sql_Business_Functions.Business.Models;
using System.Collections.Generic;

namespace Tests
{
    [TestClass]
    public class ObjectToXml_Tests
    {
        [TestMethod]
        public void Script_To_Xml()
        {
            var script = new Script
            {
                Actions = 
                {
                    new SqlAction
                    {
                        Connection = 
                        {
                            Server_Ip_Or_Name = "TestServer",
                            Database_Name = "TestDB",
                            Username = "Username",
                            Password = "Password",
                        },
                        Link = 
                        {
                            Type = ActionType.Select,
                            Select = 
                            {
                                Type = ActionSelectType.Normal,
                                Table_Or_View_Name = "Test Table",
                                Columns = new []
                                {
                                    "test column 1",
                                    "test column 2",
                                    "test column 3",
                                },
                                Filters = new []
                                {
                                    new Filter
                                    {
                                        Column_Name = "test column name filter 1",
                                        Type = FilterType.Like,
                                        Value = "test filter string",
                                    },
                                    new Filter
                                    {
                                        Column_Name = "test column name filter 2",
                                        Type = FilterType.Equals,
                                        Value = 2,
                                    },
                                },
                                Order_By_Columns = new []
                                {
                                    "test order by column 1",
                                    "test order by column 2",
                                    "test order by column 3",
                                }
                            },
                        },
                        Sql = "Sql select Text test",
                    },
                },
            };

            var xml = script.ToXml();
            Assert.IsTrue(xml != null && xml.Trim() != ""
                , "The xml was empty that was returned by ToXml method");
        }
        [TestMethod]
        public void Script_To_Obj()
        {
            var script = new Script
            {
                Actions = 
                {
                    new SqlAction
                    {
                        Connection = 
                        {
                            Server_Ip_Or_Name = "TestServer",
                            Database_Name = "TestDB",
                            Username = "Username",
                            Password = "Password",
                        },
                        Link = 
                        {
                            Type = ActionType.Select,
                            Select = new SelectAction()
                            {
                                Type = ActionSelectType.Normal,
                                Table_Or_View_Name = "Test Table",
                                Columns = new []
                                {
                                    "test column 1",
                                    "test column 2",
                                    "test column 3",
                                },
                                Filters = new []
                                {
                                    new Filter
                                    {
                                        Column_Name = "test column name filter 1",
                                        Type = FilterType.Like,
                                        Value = "test filter string",
                                    },
                                    new Filter
                                    {
                                        Column_Name = "test column name filter 2",
                                        Type = FilterType.Equals,
                                        Value = 2,
                                    },
                                    new Filter
                                    {
                                        Column_Name = "test column name filter 3 date",
                                        Type = FilterType.Equals,
                                        Value = System.DateTime.Now,
                                    },
                                },
                                Order_By_Columns = new []
                                {
                                    "test order by column 1",
                                    "test order by column 2",
                                    "test order by column 3",
                                }
                            }
                        },
                        Sql = "Sql select Text test",
                    },
                },
            };

            var xml = script.ToXml();
            Assert.IsTrue(xml != null && xml.Trim() != ""
                , "The xml was empty that was returned by ToXml method");
            var newScript = xml.ToObject<Script>();
            var filter1Type = newScript.Actions[0].Link.Select.Filters[0].Value.GetType();
            var filter3Type = newScript.Actions[0].Link.Select.Filters[2].Value.GetType();
            Assert.IsTrue(newScript != null
                , "The object could not be Deserialized");
        }
    }
}