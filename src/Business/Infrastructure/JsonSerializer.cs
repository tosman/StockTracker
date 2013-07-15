using Newtonsoft.Json;
using System.Text;

namespace StockTracker.Business.Infrastructure
{
    public static class JsonSerializer
    {
        public static string ToJson(this object obj, bool includeReference = true)
        {
            return JsonConvert.SerializeObject(obj, Formatting.Indented, new JsonSerializerSettings()
            {
                PreserveReferencesHandling = (includeReference) ? PreserveReferencesHandling.Objects : PreserveReferencesHandling.None
            });
        }

        public static T FromJson<T>(this string json)
        {
            return JsonConvert.DeserializeObject<T>(json, new JsonSerializerSettings()
            {
                PreserveReferencesHandling = PreserveReferencesHandling.Objects
            });
        }

        public static void UpdateFromJson(this object obj, string json)
        {
            JsonConvert.PopulateObject(json, obj);
        }

        public static byte[] GetBytes(this string s)
        {
            return Encoding.Default.GetBytes(s);
        }
    }
}
