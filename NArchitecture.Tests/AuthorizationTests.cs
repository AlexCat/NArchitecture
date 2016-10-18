using NArchitecture.Security;
using NArchitecture.Tests.Security;
using System;
using System.Security.Claims;
using System.Threading.Tasks;
using Xunit;

namespace NArchitecture.Tests
{
    public class AuthorizationTests
    {
        [Fact]
        public async Task SucceededRequirementTest()
        {
            var user = UserFactory.CreateUser(i =>
            {
                i.AddClaim(new Claim(ClaimTypes.DateOfBirth, new DateTime(1986, 3, 10).ToString(), ClaimValueTypes.DateTime, "http://contoso.com"));
            });

            var authorizationService = ServiceFactory.CreateAuthorizationService(o =>
            {
                o.AddPolicy("Over21", new AuthorizationPolicy(new IAuthorizationRequirement[] { new MinimumAgeRequirement(21) }));
                o.AddAuthorizationHandler<MinimumAgeHandler>();
            });

            Assert.True(await authorizationService.Authorize(user, null, "Over21"));
        }

        [Fact]
        public async Task FailedRequirementTest()
        {
            var user = UserFactory.CreateUser(i =>
            {
                i.AddClaim(new Claim(ClaimTypes.DateOfBirth, new DateTime(1986, 3, 10).ToString(), ClaimValueTypes.DateTime, "http://contoso.com"));
            });

            var authorizationService = ServiceFactory.CreateAuthorizationService(o =>
            {
                o.AddPolicy("Over40", new AuthorizationPolicy(new IAuthorizationRequirement[] { new MinimumAgeRequirement(40) }));
                o.AddAuthorizationHandler<MinimumAgeHandler>();
            });

            Assert.False(await authorizationService.Authorize(user, null, "Over40"));
        }

        [Fact]
        public async Task NoRequirementTest()
        {
            var user = UserFactory.CreateUser(i =>
            {
                i.AddClaim(new Claim(ClaimTypes.DateOfBirth, new DateTime(1986, 3, 10).ToString(), ClaimValueTypes.DateTime, "http://contoso.com"));
            });

            var authorizationService = ServiceFactory.CreateAuthorizationService(o => { });
            var requirements = new IAuthorizationRequirement[0];

            Assert.False(await authorizationService.Authorize(user, null, requirements));
        }
    }
}
