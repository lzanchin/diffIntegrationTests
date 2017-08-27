using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Net.Http;

namespace DiffIntegrationTests
{
    [TestClass]
    public class DiffIntegrationTest
    {
        [TestMethod]
        public void RequestShouldRespond()
        {
            var client = new HttpClient();

            var content = new StringContent("Some text new");
            var response = client.PostAsync("http://localhost:49591/v1/diff/1/left", content).Result;

            //Assert.AreEqual("Some text", response.Content.ReadAsStringAsync().Result);
            Assert.AreEqual(System.Net.HttpStatusCode.OK, response.StatusCode);
        }
    }
}
