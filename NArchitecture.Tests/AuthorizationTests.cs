using NArchitecture.Tests.Security;
using System;
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
                i.AddDateOfBirthClaim(new DateTime(1986, 3, 10));
            });

            var authorizationService = ServiceFactory.CreateAuthorizationService(c =>
            {
                c.Options.AddPolicy("Over21", p => p.AddRequirements(new MinimumAgeRequirement(21)));
                c.AddAuthorizationHandler<MinimumAgeHandler>();
            });

            Assert.True(await authorizationService.Authorize(user, null, "Over21"));
        }

        [Fact]
        public async Task FailedRequirementTest()
        {
            var user = UserFactory.CreateUser(i =>
            {
                i.AddDateOfBirthClaim(new DateTime(1986, 3, 10));
            });

            var authorizationService = ServiceFactory.CreateAuthorizationService(c =>
            {
                c.Options.AddPolicy("Over40", p => p.AddRequirements(new MinimumAgeRequirement(40)));
                c.AddAuthorizationHandler<MinimumAgeHandler>();
            });

            Assert.False(await authorizationService.Authorize(user, null, "Over40"));
        }

        [Fact]
        public async Task NoRequirementTest()
        {
            var user = UserFactory.CreateUser(i =>
            {
                i.AddDateOfBirthClaim(new DateTime(1986, 3, 10));
            });

            var authorizationService = ServiceFactory.CreateAuthorizationService(c => { });
            var requirements = new IAuthorizationRequirement[0];

            Assert.False(await authorizationService.Authorize(user, null, requirements));
        }
    }
}
