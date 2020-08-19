using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using college_interview_task_v4;

namespace httpRequestHandlerTests
{
    class FakeHttpClient: HttpClient
    {
        public static Uri UriWithString = new Uri("http://fake.api.com/1");
        public static Uri UriWithInt = new Uri("http://fake.api.com/2");
        public static Uri UriExceptionThrow = new Uri("http://fake.api.com/0");

        public FakeHttpClient() : base()
        {}

        public override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            return Task.Run(() =>
            {
                HttpResponseMessage response = new HttpResponseMessage();
                if (request.RequestUri == UriExceptionThrow)
                {
                    throw new HttpRequestException();
                }
                else if (request.RequestUri == UriWithString)
                {
                    response.StatusCode = HttpStatusCode.OK;
                    response.Content = new StringContent("string");
                }
                else if (request.RequestUri == UriWithInt)
                {
                    response.StatusCode = HttpStatusCode.OK;
                    response.Content = new StringContent("200");
                }
                else
                {
                    response.StatusCode = HttpStatusCode.NotFound;
                }
                return response;
            });
        }

    }

    class SampleHttpRequestHandler : HttpRequestHandler<string, int>
    {
        public SampleHttpRequestHandler()
            :base(new SampleRequestParser(), new SampleResponseParser())
        {
            httpClient = new FakeHttpClient();
        }
        protected override HttpClient httpClient { get; set; }
    }

    class SampleRequestParser : IRequestParser<string>
    {
        public HttpRequestMessage Parse(string request)
        {
            string urlString = string.Format("http://fake.api.com/{0}", request);
            return new HttpRequestMessage(HttpMethod.Get, new Uri(urlString, UriKind.Absolute));
        }
    }

    class SampleResponseParser : IHttpResponseParser<int>
    {
        public Task<int> ParseAsync(HttpResponseMessage response)
        {
            return Task.Run( ()=> {
                return int.Parse(response.Content.ReadAsStringAsync().Result);
            });
        }
    }

}
