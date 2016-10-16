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
        private ClaimsIdentity identity;
        private ClaimsPrincipal user;
        private AuthorizationOptions options;
        private IAuthorizationHandler[] handlers;
        private IAuthorizationService authorizationService;

        public AuthorizationTests()
        {
            identity = new ClaimsIdentity(new Claim[]
            {
                new Claim(ClaimTypes.DateOfBirth, new DateTime(1986, 3, 10).ToString(), ClaimValueTypes.DateTime, "http://contoso.com")
            });
            user = new ClaimsPrincipal(identity);
            options = new AuthorizationOptions();
            options.AddPolicy("Over21", new AuthorizationPolicy(new IAuthorizationRequirement[] { new MinimumAgeRequirement(21) }));
            options.AddPolicy("Over40", new AuthorizationPolicy(new IAuthorizationRequirement[] { new MinimumAgeRequirement(40) }));
            handlers = new IAuthorizationHandler[] {
                new MinimumAgeHandler()
            };
            authorizationService = new DefaultAuthorizationService(options, handlers);
        }

        [Fact]
        public async Task SucceededRequirementTest()
        {
            Assert.True(await authorizationService.Authorize(user, null, "Over21"));
        }

        [Fact]
        public async Task FailedRequirementTest()
        {
            Assert.False(await authorizationService.Authorize(user, null, "Over40"));
        }

        [Fact]
        public async Task NoRequirementTest()
        {
            var requirements = new IAuthorizationRequirement[0];
            Assert.False(await authorizationService.Authorize(user, null, requirements));
        }
    }
}
