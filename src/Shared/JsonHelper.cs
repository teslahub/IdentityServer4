using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using IdentityModel.Client;
using Microsoft.IdentityModel.Tokens;
using Testing.Dynamic;

namespace IdentityServer
{
    internal static class JsonHelper
    {
        public static Dictionary<string, object> GetPayload(TokenResponse response)
        {
            return GetPayload(response.AccessToken);
        }

        public static Dictionary<string, object> GetPayload(string jwt)
        {
            var token = jwt.Split('.').Skip(1).Take(1).First();
            var dictionary = JsonSerializer.Deserialize<DJson>(Base64UrlEncoder.Decode(token)).InnerDict;
            return dictionary;
        }

        public static Dictionary<string, object> ToDictionary(ProtocolResponse response)
        {
            return ToDictionary(response.Raw);
        }

        public static Dictionary<string, object> ToDictionary(string json)
        {
            return JsonSerializer.Deserialize<DJson>(json).InnerDict;
        }

        public static IEnumerable<object> AsDJsonInnerList(this object djson)
        {
            return ((DJson)djson).InnerList.AsEnumerable();
        }

        public static Dictionary<string, object> AsDJsonInnerDict(this object djson)
        {
            return ((DJson)djson).InnerDict;
        }

        public static string NormalizeJson(string json)
        {
            var obj = Newtonsoft.Json.JsonConvert.DeserializeObject<Newtonsoft.Json.Linq.JObject>(json);
            return Newtonsoft.Json.JsonConvert.SerializeObject(obj);
        }
    }
}
