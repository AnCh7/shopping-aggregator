using System.Collections.Generic;
using System.Threading.Tasks;
using System.Net;
using System.Text.RegularExpressions;
using RestSharp;
using ShopAggregator.Web.Models;
using ShopAggregator.Web.Services.Base;

namespace ShopAggregator.Web.Services
{
    public class ShopAggregatorService
    {
        private readonly IApiGateway _gateway;
        private readonly IJsonConverter _jsonConverter;

        public ShopAggregatorService(string url)
        {
            _gateway = new ApiGateway(url);
            _jsonConverter = new JsonNetConverter();
        }

        public async Task<OperationResult<PaginationResponse<Shop>>> GetAllShops(PaginationRequest request)
        {
            var parameters = new List<RequestParameter>
            {
                new RequestParameter { Key = "page", Value = request.Page, Type = ParameterType.QueryString },
                new RequestParameter { Key = "pageSize", Value = request.PageSize, Type = ParameterType.QueryString }
            };

            var response = await _gateway.Get("/shops", parameters);
            var errorResult = CheckErrors(response);
            return CreateResult<PaginationResponse<Shop>>(response.Content, errorResult);
        }

        public async Task<OperationResult<Shop>> GetShop(int shopId)
        {
            var response = await _gateway.Get($"/shop/" + shopId, new List<RequestParameter>());
            var errorResult = CheckErrors(response);
            return CreateResult<Shop>(response.Content, errorResult);
        }

        public async Task<OperationResult<Stock>> GetShopProducts(int shopId)
        {
            var response = await _gateway.Get($"/shops/" + shopId + "/products", new List<RequestParameter>());
            var errorResult = CheckErrors(response);
            return CreateResult<Stock>(response.Content, errorResult);
        }

        public async Task<OperationResult<PaginationResponse<Product>>> GetAllProducts(PaginationRequest request)
        {
            var parameters = new List<RequestParameter>
            {
                new RequestParameter { Key = "page", Value = request.Page, Type = ParameterType.UrlSegment },
                new RequestParameter { Key = "pageSize", Value = request.PageSize, Type = ParameterType.UrlSegment }
            };

            var response = await _gateway.Get("/products", parameters);
            var errorResult = CheckErrors(response);
            return CreateResult<PaginationResponse<Product>>(response.Content, errorResult);
        }

        public async Task<OperationResult<Product>> GetProduct(int productId)
        {
            var response = await _gateway.Get($"/products/" + productId, new List<RequestParameter>());
            var errorResult = CheckErrors(response);
            return CreateResult<Product>(response.Content, errorResult);
        }

        private OperationResult CheckErrors(IRestResponse response)
        {
            var result = new OperationResult();
            var content = response.Content;

            // Network transport or framework errors
            if (response.ErrorException != null)
            {
                result.Errors.Add(response.ErrorMessage);
            }
            // Transport errors
            else if (response.ResponseStatus != ResponseStatus.Completed)
            {
                result.Errors.Add("ResponseStatus: " + response.ResponseStatus);
            }
            // HTTP errors
            else if (response.StatusCode != HttpStatusCode.OK && response.StatusCode != HttpStatusCode.Created)
            {
                result.Errors.Add(response.StatusDescription);
            }
            else
            {
                result.Success = true;
            }

            if (!result.Success)
            {
                // Checking content
                if (string.IsNullOrWhiteSpace(content))
                {
                    result.Errors.Add("Empty response content");
                }
                else if (new Regex(@"<[^>]+>").IsMatch(content))
                {
                    result.Errors.Add("Response content contains HTML : " + content);
                }
                else
                {
                    result.Errors.Add("Response content is not valid");
                }
            }

            return result;
        }

        private OperationResult<T> CreateResult<T>(string json, OperationResult error)
        {
            var result = new OperationResult<T>();

            if (error.Success)
            {
                result.Result = _jsonConverter.Deserialize<T>(json);
            }
            else
            {
                result.Errors.AddRange(error.Errors);
                result.Success = false;
            }

            return result;
        }
    }
}