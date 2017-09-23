using System;
using AxosoftAPI.NET;
using AxosoftAPI.NET.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ApiTests
{
    [TestClass]
    public class AxosoftTests
    {
        [TestMethod]
        public void AxosoftInitalTest()
        {
            var client = new Proxy
            {
                ClientId = "d19f8d74-11e5-44a6-a830-ddbafba3048a",
                ClientSecret = "3887e709-c1b8-458a-8a7d-c5c8310f22e5", 
                //ClientId = "9a837d3b-a70d-40b4-8544-d90f35763a85",
                //ClientSecret = "", 
                Url = "https://mrgroup.axosoft.com", 
            };
            var token = client.ObtainAccessTokenFromUsernamePassword("Charles.Kuperus@medrockgroup.com", "Enterlol12341234_+", ScopeEnum.ReadWrite);
            //var token = client.ObtainAccessTokenFromAuthorizationCode("Mxk3uhXXS7Yx9EHcfOrPe4JhgB5qm_SZ0xAykViwUzEOOZXmihd29sPhnXPq6fq_dfCqAMDkekFalUnSDaZ9454ZLokzbrQva7aQ", "", ScopeEnum.ReadWrite);
            //client.AccessToken = token;
            var workLogResult = new AxosoftAPI.NET.WorkLogs(client).Get();
        }
    }
}
