using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Net;

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
        protected HttpClient _httpClientProxy;
        private IHttpResponseParser<TResponse> _responseParser { get; set; }
        private IRequestParser<TRequest> _requestParser { get; set; }


        protected HttpRequestHandler(HttpClient httpClientProxy, IRequestParser<TRequest> requestParser, IHttpResponseParser<TResponse> responseParser)
        {
            _httpClientProxy = httpClientProxy;
            _requestParser = requestParser;
            _responseParser = responseParser;
        }

        public async Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken)
        {
            try
            {
                HttpRequestMessage requestMessage = _requestParser.Parse(request);
                HttpResponseMessage response = await _httpClientProxy.SendAsync(requestMessage, cancellationToken);
                response.EnsureSuccessStatusCode();
                return await _responseParser.ParseAsync(response);
            }
            catch (FormatException e)
            {
                throw new ApplicationException("Application does not work properly.", e);
            }
            catch (HttpRequestException e)
            {
                throw e;
            }
            catch (Exception e)
            {
                throw new Exception("Unknown exception.", e);
            }
        }
    }



}