using college_interview_task_v4;
using System.Net.Http;
using System.Threading.Tasks;

namespace currencyClassLibrary
{
    public class ResponseParser : IHttpResponseParser<CurrencyResults>
    {
        public async Task<CurrencyResults> ParseAsync(HttpResponseMessage response)
        {
            return (await response.Content.ReadAsAsync<CurrencyResults[]>())[0];
        }
    }
}
