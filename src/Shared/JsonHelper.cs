using IdentityModel.Client;
using Microsoft.IdentityModel.Tokens;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using Testing.Dynamic;

namespace IdentityServer
{
    internal static class JsonHelper
    {
        public static Dictionary<string, object> GetPayload(TokenResponse response) => GetPayload(response.AccessToken);

        public static Dictionary<string, object> GetPayload(string jwt)
        {
            var token = jwt.Split('.').Skip(1).Take(1).First();
            var dictionary = JsonSerializer.Deserialize<DJson>(Base64UrlEncoder.Decode(token)).InnerDict;
            return dictionary;
        }

        public static Dictionary<string, object> ToDictionary(ProtocolResponse response) => ToDictionary(response.Raw);

        public static Dictionary<string, object> ToDictionary(string json) => JsonSerializer.Deserialize<DJson>(json).InnerDict;

        public static IEnumerable<object> AsDJsonInnerList(this object djson) => ((DJson) djson).InnerList.AsEnumerable();

        public static Dictionary<string, object> AsDJsonInnerDict(this object djson) => ((DJson) djson).InnerDict;
    }
}
