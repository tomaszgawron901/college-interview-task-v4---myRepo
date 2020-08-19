using System;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace college_interview_task_v4
{
    public interface IHttpResponseParser<TResult>
    {
        Task<TResult> ParseAsync(HttpResponseMessage response);
    }

    public interface IRequestParser<TInput>
    {
        HttpRequestMessage Parse(TInput request);
    }


    public abstract class HttpRequestHandler<TRequest, TResponse>
    {
        abstract protected HttpClient httpClient { get; set; }
        private IHttpResponseParser<TResponse> _responseParser { get;}
        private IRequestParser<TRequest> _requestParser { get;}


        protected HttpRequestHandler(IRequestParser<TRequest> requestParser, IHttpResponseParser<TResponse> responseParser)
        {
            _requestParser = requestParser;
            _responseParser = responseParser;
        }

        public async Task<TResponse> Handle(TRequest request, CancellationToken? cancellationToken=null)
        {
            HttpRequestMessage requestMessage;
            HttpResponseMessage response;

            try
            {
                requestMessage = _requestParser.Parse(request);
            }
            catch
            {
                throw new ApplicationException("Wrongly defined _requestParser or request.");
            }

            try
            {
                if(cancellationToken is null)
                {
                    response = await httpClient.SendAsync(requestMessage);
                }
                else
                {
                    response = await httpClient.SendAsync(requestMessage, (CancellationToken)cancellationToken);
                }
                
                response.EnsureSuccessStatusCode();
                
            }
            catch (HttpRequestException e)
            {
                throw new HttpRequestException("Unable to get data", e);
            }
            catch (TaskCanceledException e)
            {
                throw new TaskCanceledException("Task canceled", e);
            }
            catch (Exception e)
            {
                throw new Exception("Unknown exception.", e);
            }

            try
            {
                return await _responseParser.ParseAsync(response);
            }
            catch
            {
                throw new ApplicationException("Wrongly defined _responseParser or response.");
            }
        }
    }
}