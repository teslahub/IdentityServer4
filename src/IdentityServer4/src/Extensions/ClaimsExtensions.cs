// Copyright (c) Brock Allen & Dominick Baier. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.


using IdentityModel;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text.Json;

namespace IdentityServer4.Extensions
{
    internal static class ClaimsExtensions
    {
        // should not throws exception
        public static Dictionary<string, object> ToClaimsDictionary(this IEnumerable<Claim> claims)
        {
            var d = new Dictionary<string, object>();

            if (claims == null)
            {
                return d;
            }

            var distinctClaims = claims.Distinct(new ClaimComparer());

            foreach (var claim in distinctClaims)
            {
                if (!d.ContainsKey(claim.Type))
                {
                    d.Add(claim.Type, SafeGetValue(claim));
                }
                else
                {
                    var value = d[claim.Type];

                    if (value is List<object> list)
                    {
                        list.Add(SafeGetValue(claim));
                    }
                    else
                    {
                        d.Remove(claim.Type);
                        d.Add(claim.Type, new List<object> { value, SafeGetValue(claim) });
                    }
                }
            }

            return d;
        }

        // should not throws exception, similar implementation with TokenExtensions.AddObject(Claim claim)
        private static object SafeGetValue(Claim claim)
        {
            if (claim.ValueType == ClaimValueTypes.Integer || claim.ValueType == ClaimValueTypes.Integer32)
            {
                if (int.TryParse(claim.Value, out int value))
                {
                    return value;
                }
            }

            if (claim.ValueType == ClaimValueTypes.Integer64)
            {
                if (long.TryParse(claim.Value, out long value))
                {
                    return value;
                }
            }

            if (claim.ValueType == ClaimValueTypes.Boolean)
            {
                if (bool.TryParse(claim.Value, out var value))
                {
                    return value;
                }
            }

            if (claim.ValueType == IdentityServerConstants.ClaimValueTypes.Json)
            {
                try
                {
                    return JsonSerializer.Deserialize<JsonElement>(claim.Value);
                }
                catch { }
            }

            return claim.Value;
        }
    }
}