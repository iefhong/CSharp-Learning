using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.Owin.Testing;
using OwinHosting;
using System.Net;
using System.Threading.Tasks;
using System.Net.Http;

namespace OwinTests
{
    [TestClass]
    public class OwinTests
    {
        [TestMethod]
        public async System.Threading.Tasks.Task Owin_returns_200_on_request_to_rootAsync()
        {
            var statusCode = await CallServer(async x =>
            {
                var response = await x.GetAsync("/");
                return  response.StatusCode;
            });
            Assert.AreEqual(HttpStatusCode.OK, statusCode);

            //using (var server = TestServer.Create<Startup>())
            //{
            //    var response = await server.HttpClient.GetAsync("/");
            //    Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
            //    // TODO: Validate response
            //}
        }

        [TestMethod]
        public async System.Threading.Tasks.Task Owin_returns_hello_world_on_request_to_rootAsync()
        {
            var body = await CallServer(async x =>
            {
                var response = await x.GetAsync("/");
                return await response.Content.ReadAsStringAsync();
            });
            Assert.AreEqual("Hello World", body);


            //using (var server = TestServer.Create<Startup>())
            //{
            //    var response = await server.HttpClient.GetAsync("/");
            //    var body = await response.Content.ReadAsStringAsync();
            //    Assert.AreEqual("Hello World", body);
            //    // TODO: Validate response
            //}
        }

        [TestMethod]
        public async System.Threading.Tasks.Task Owin_returns_contenttype_on_request_to_jpg()
        {
            var contenttype = await CallServer(async x =>
            {
                var response = await x.GetAsync("/sven.jpg");
                return response.Content.Headers.ContentType.MediaType;
            });
            Assert.AreEqual("image/jpeg", contenttype);


            //using (var server = TestServer.Create<Startup>())
            //{
            //    var response = await server.HttpClient.GetAsync("/sven.jpg");
            //    var contenttype = response.Content.Headers.ContentType;
            //    Assert.AreEqual("image/jpeg", contenttype);
            //    // TODO: Validate response
            //}
        }

        private async Task<T> CallServer<T>(Func<HttpClient, Task<T>> callback)
        {
            using (var server = TestServer.Create<Startup>())
            {
                return await callback(server.HttpClient);
            }
        }
    }
}
