using college_interview_task_v4;
using System;
using System.Net.Http;

namespace currencyClassLibrary
{
    public class RequestParser : IRequestParser<DateTime>
    {

        public HttpRequestMessage Parse(DateTime request)
        {
            string urlString = string.Format("http://api.nbp.pl/api/exchangerates/tables/c/{0:yyyy-MM-dd}", request);
            return new HttpRequestMessage(HttpMethod.Get, new Uri(urlString, UriKind.Absolute));
        }
    }
}
