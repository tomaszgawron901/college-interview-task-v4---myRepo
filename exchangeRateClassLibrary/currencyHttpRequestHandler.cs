using System;
using System.Net.Http;
using college_interview_task_v4;

namespace currencyClassLibrary
{
    public class CurrencyHttpRequestHandler: HttpRequestHandler<DateTime, CurrencyResults>
    {
        public CurrencyHttpRequestHandler(HttpClient client)
            : base(new RequestParser(), new ResponseParser())
        {
            httpClient = client;
        }

        protected override HttpClient httpClient { get; set; }
    }
}
