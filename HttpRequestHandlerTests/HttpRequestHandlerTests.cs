using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Net.Http;
using System.Threading;

namespace httpRequestHandlerTests
{
    [TestClass]
    public class HandleMethodTests
    {
        SampleHttpRequestHandler requestHandler = new SampleHttpRequestHandler();

        [DataTestMethod]
        [DataRow("0")]
        [ExpectedException(typeof(HttpRequestException))]
        public void Return_HttpRequestException_when_httpClient_sendAsync_returns_exception(string request)
        {
            requestHandler.Handle(request, new CancellationToken()).GetAwaiter().GetResult();
        }

        [DataTestMethod]
        [DataRow("1")]
        [ExpectedException(typeof(ApplicationException))]
        public void Return_ApplicationException_when_data_cannot_be_parsed(string request)
        {
            requestHandler.Handle(request, new CancellationToken()).GetAwaiter().GetResult();
        }

        [DataTestMethod]
        [DataRow("2")]
        public void Return_parsed_HttpResponseMessage(string request)
        {
            var output = requestHandler.Handle(request, new CancellationToken()).GetAwaiter().GetResult();
            Assert.AreEqual(output, 200);
        }

        [DataTestMethod]
        [DataRow("3")]
        [DataRow("4")]
        [ExpectedException(typeof(HttpRequestException))]
        public void Return_HttpRequestException_when_not_SuccessStatusCode(string request)
        {
            requestHandler.Handle(request, new CancellationToken()).GetAwaiter().GetResult();
        }
    }
}
