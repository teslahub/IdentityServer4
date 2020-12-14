using IdentityModel;
using IdentityServer.UnitTests.Common;
using IdentityServer4.Configuration;
using IdentityServer4.Models;
using IdentityServer4.Services;
using Microsoft.AspNetCore.Authentication;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text.Json;
using System.Threading.Tasks;
using Xunit;

namespace IdentityServer.UnitTests.Services.Default
{
    public class DefaultTokenCreationServiceTests
    {
        private Token GenerateToken(List<Claim> claims)
        {
            return new Token(OidcConstants.TokenTypes.AccessToken)
            {
                CreationTime = DateTime.UtcNow,
                Issuer = "issuer",
                Lifetime = 60,
                Claims = claims.Distinct(new ClaimComparer()).ToList(),
                ClientId = "client"
            };
        }

        [Fact]
        public async Task Should_create_custom_claim_array()
        {
            var options = new IdentityServerOptions();

            var claimJson = JsonSerializer.Serialize(new[] { "aaa", "bbb" });

            var token = GenerateToken(new List<Claim> { new Claim("test_claim_json_array", claimJson, IdentityServer4.IdentityServerConstants.ClaimValueTypes.Json) });

            var mock = new Mock<IKeyMaterialService>();
            var signingCredentials = TestCert.LoadSigningCredentials();
            mock.Setup(k => k.GetSigningCredentialsAsync(It.IsAny<IEnumerable<string>>())).ReturnsAsync(signingCredentials).Verifiable();

            var service = new DefaultTokenCreationService(new SystemClock(),
                mock.Object,
                options,
                TestLogger.Create<DefaultTokenCreationService>());

            var result = await service.CreateTokenAsync(token);

            var payload = JsonHelper.GetPayload(result);

            var customClaims = payload["test_claim_json_array"].AsDJsonInnerList();
            Assert.Equal(new[] { "aaa", "bbb" }, customClaims);

            mock.VerifyAll();
        }

        [Fact]
        public async Task Should_create_custom_claim_object()
        {
            var options = new IdentityServerOptions();

            var claimJson = JsonSerializer.Serialize(new { block = new[] { "aaa", "111" }, street = "bbb" });

            var token = GenerateToken(new List<Claim> { new Claim("test_claim_json_complex", claimJson, IdentityServer4.IdentityServerConstants.ClaimValueTypes.Json) });

            var mock = new Mock<IKeyMaterialService>();
            var signingCredentials = TestCert.LoadSigningCredentials();
            mock.Setup(k => k.GetSigningCredentialsAsync(It.IsAny<IEnumerable<string>>())).ReturnsAsync(signingCredentials).Verifiable();

            var service = new DefaultTokenCreationService(new SystemClock(),
                mock.Object,
                options,
                TestLogger.Create<DefaultTokenCreationService>());

            var result = await service.CreateTokenAsync(token);

            var payload = JsonHelper.GetPayload(result);

            var customClaims = payload["test_claim_json_complex"].AsDJsonInnerDict();
            Assert.Equal(2, customClaims.Count);
            Assert.Equal(new[] { "aaa", "111" }, customClaims["block"].AsDJsonInnerList());
            Assert.Equal("bbb", customClaims["street"]);

            mock.VerifyAll();
        }
    }
}
