using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using college_interview_task_v4;

namespace exchangeRateClassLibrary
{
    public class ExchangeRateHttpRequestHandler: HttpRequestHandler<DateTime, ExchangeRateResults>
    {
        public ExchangeRateHttpRequestHandler(HttpClient client)
            : base(client, new RequestParser(), new ResponseParser())
        {}
    }
}
