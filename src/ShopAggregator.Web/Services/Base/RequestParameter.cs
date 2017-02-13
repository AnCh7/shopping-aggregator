using RestSharp;

namespace ShopAggregator.Web.Services.Base
{
    public class RequestParameter
    {
        public string Key { get; set; }
        public object Value { get; set; }
        public ParameterType Type { get; set; }
    }
}