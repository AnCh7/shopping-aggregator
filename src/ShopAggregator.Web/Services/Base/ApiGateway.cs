using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using RestSharp;

namespace ShopAggregator.Web.Services.Base
{
    public interface IApiGateway
    {
        Task<IRestResponse> Get(string endpoint, IEnumerable<RequestParameter> parameters);
        Task<IRestResponse> Post(string endpoint, IEnumerable<RequestParameter> parameters);
    }

    public class ApiGateway : IApiGateway
    {
        private readonly RestClient _restClient;

        public ApiGateway(string url)
        {
            if (string.IsNullOrEmpty(url)) throw new ArgumentNullException(nameof(url));
            _restClient = new RestClient(url);
        }

        public Task<IRestResponse> Get(string endpoint, IEnumerable<RequestParameter> parameters)
        {
            var request = CreateRequest(endpoint, parameters);
            request.Method = Method.GET;
            var tcs = new TaskCompletionSource<IRestResponse>();
            _restClient.ExecuteAsync(request, response => { tcs.SetResult(response); });
            return tcs.Task;
        }

        public Task<IRestResponse> Post(string endpoint, IEnumerable<RequestParameter> parameters)
        {
            var request = CreateRequest(endpoint, parameters);
            request.Method = Method.POST;
            var tcs = new TaskCompletionSource<IRestResponse>();
            _restClient.ExecuteAsync(request, response => { tcs.SetResult(response); });
            return tcs.Task;
        }

        private IRestRequest CreateRequest(string endpoint, IEnumerable<RequestParameter> parameters)
        {
            var restRequest = new RestRequest(endpoint) {RequestFormat = DataFormat.Json};
            foreach (var parameter in parameters)
            {
                restRequest.AddParameter(parameter.Key, parameter.Value, parameter.Type);
            }
            return restRequest;
        }
    }
}