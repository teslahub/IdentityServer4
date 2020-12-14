using FluentAssertions;
using IdentityModel;
using IdentityServer.UnitTests.Common;
using IdentityServer4.Configuration;
using IdentityServer4.Extensions;
using IdentityServer4.Models;
using Microsoft.AspNetCore.Authentication;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text.Json;
using Xunit;

namespace IdentityServer.UnitTests.Extensions
{
    public class JwtPayloadCreationTests
    {
        private readonly Token _token;

        public JwtPayloadCreationTests()
        {
            var claims = new List<Claim>
            {
                new Claim(JwtClaimTypes.Scope, "scope1"),
                new Claim(JwtClaimTypes.Scope, "scope2"),
                new Claim(JwtClaimTypes.Scope, "scope3"),
            };

            _token = new Token(OidcConstants.TokenTypes.AccessToken)
            {
                CreationTime = DateTime.UtcNow,
                Issuer = "issuer",
                Lifetime = 60,
                Claims = claims.Distinct(new ClaimComparer()).ToList(),
                ClientId = "client"
            };
        }

        [Fact]
        public void Should_create_scopes_as_array_by_default()
        {
            var options = new IdentityServerOptions();
            var payloadDict = _token.CreateJwtPayloadDictionary(options, new SystemClock(), TestLogger.Create<JwtPayloadCreationTests>());

            var payload = JsonHelper.ToDictionary(JsonSerializer.Serialize(payloadDict));
            var scopes = payload[JwtClaimTypes.Scope].AsDJsonInnerList().ToArray();
            scopes.Count().Should().Be(3);
            scopes[0].Should().Be("scope1");
            scopes[1].Should().Be("scope2");
            scopes[2].Should().Be("scope3");
        }

        [Fact]
        public void Should_create_scopes_as_string()
        {
            var options = new IdentityServerOptions
            {
                EmitScopesAsSpaceDelimitedStringInJwt = true
            };

            var payloadDict = _token.CreateJwtPayloadDictionary(options, new SystemClock(), TestLogger.Create<JwtPayloadCreationTests>());

            var payload = JsonHelper.ToDictionary(JsonSerializer.Serialize(payloadDict));

            payload.Should().NotBeNull();
            var scopes = payload[JwtClaimTypes.Scope];
            scopes.Should().Be("scope1 scope2 scope3");
        }
    }
}
