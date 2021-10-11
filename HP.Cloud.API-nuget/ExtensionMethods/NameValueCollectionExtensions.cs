using System.Collections.Specialized;
using System.Text;
using System.Web;

namespace HeimdallPower.ExtensionMethods
{
    public static class NameValueCollectionExtensions
    {
        public static string ToQueryString(this NameValueCollection nameValueCollection)
        {
            NameValueCollection httpValueCollection = HttpUtility.ParseQueryString(string.Empty, Encoding.UTF8);
            httpValueCollection.Add(nameValueCollection);
            return $"?{httpValueCollection}";
        }

        public static NameValueCollection AddQueryParam(this NameValueCollection nameValueCollection, NameValueCollection queryParam)
        {
            nameValueCollection.Add(queryParam);
            return nameValueCollection;
        }
        public static NameValueCollection AddQueryParam(this NameValueCollection nameValueCollection, string key, string value)
        {
            nameValueCollection[key] = value;
            return nameValueCollection;
        }
    }
}
