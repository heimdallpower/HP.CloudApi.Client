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
    }
}
