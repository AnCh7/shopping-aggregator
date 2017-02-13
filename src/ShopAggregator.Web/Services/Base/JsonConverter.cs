using Newtonsoft.Json;

namespace ShopAggregator.Web.Services.Base
{
    public interface IJsonConverter
    {
        T Deserialize<T>(string s);
        T DeserializeAnonymousType<T>(string s, T definition);
        string Serialize(object obj);
    }

    public class JsonNetConverter : IJsonConverter
    {
        public T Deserialize<T>(string s)
        {
            return JsonConvert.DeserializeObject<T>(s);
        }

        public string Serialize(object obj)
        {
            return JsonConvert.SerializeObject(obj);
        }

        public T DeserializeAnonymousType<T>(string s, T definition)
        {
            return JsonConvert.DeserializeAnonymousType(s, definition);
        }
    }
}