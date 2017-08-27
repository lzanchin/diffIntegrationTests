using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Net.Http;

namespace DiffIntegrationTests
{
    [TestClass]
    public class DiffIntegrationTest
    {
        [TestInitialize]
        public void Setup()
        {
            
        }
        [TestMethod]
        public void RequestShouldRespond()
        {
            var client = new HttpClient();
            var content = new StringContent("Some text new");
            var response = client.PostAsync("http://localhost:49591/v1/diff/1/left", content).Result;

            //Assert.AreEqual("Some text", response.Content.ReadAsStringAsync().Result);
            Assert.AreEqual(System.Net.HttpStatusCode.OK, response.StatusCode);
        }
        [TestMethod]
        public void SendDataToTheLeftEndpoint()
        {
            var client = new HttpClient();
            var content = new StringContent("Some text new");
            var response = client.PostAsync("http://localhost:49591/v1/diff/1/left", content).Result;

            Assert.AreEqual("\"Data saved\"", response.Content.ReadAsStringAsync().Result);

        }

        [TestMethod]
        public void SendDataToTheRightEndpoint()
        {
            var client = new HttpClient();
            var content = new StringContent("Some text right");
            var response = client.PostAsync("http://localhost:49591/v1/diff/1/right", content).Result;

            Assert.AreEqual("\"Data saved\"", response.Content.ReadAsStringAsync().Result);

        }

        [TestMethod]
        public void SendRequestToCompareWithInvalidIds()
        {
            int id = 5;
            string expectedResponse = "Id " + id + " does not exists in Left and Right side";
            var client = new HttpClient();
            var content = new StringContent("Some text new");
            var response = client.GetAsync("http://localhost:49591/v1/diff/" + id).Result;
            if (!response.Content.ReadAsStringAsync().Result.Contains(expectedResponse)){
                Assert.Fail();
            }
        }

        [TestMethod]
        public void SendRequestToCompareWithValidIds()
        {
            int id = 5;
            string expectedResponse = "Id " + id + " does not exists in Left and Right side";
            var client = new HttpClient();
            var leftContent = new StringContent("Some text left");
            var rightContent = new StringContent("Some text right");
            client.PostAsync("http://localhost:49591/v1/diff/"+id+"/left", leftContent);
            client.PostAsync("http://localhost:49591/v1/diff/"+id+"/right", rightContent);
            var response = client.GetAsync("http://localhost:49591/v1/diff/" + id).Result;
            if (response.Content.ReadAsStringAsync().Result.Contains(leftContent+""))
            {
                Assert.Fail();
            }
            if (response.Content.ReadAsStringAsync().Result.Contains(rightContent+""))
            {
                Assert.Fail();
            }
        }

    }
}
