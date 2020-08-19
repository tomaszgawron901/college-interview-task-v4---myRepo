using college_interview_task_v4;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace exchangeRateClassLibrary
{
    public class ResponseParser : IHttpResponseParser<ExchangeRateResults>
    {
        public async Task<ExchangeRateResults> ParseAsync(HttpResponseMessage response)
        {
            return (await response.Content.ReadAsAsync<ExchangeRateResults[]>())[0];
        }
    }
}
