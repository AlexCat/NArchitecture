using NArchitecture.Security;
using NArchitecture.Tests.Security;
using System;
using System.Security.Claims;
using System.Threading.Tasks;
using Xunit;


namespace NArchitecture.Tests
{
    public class BusTests
    {
        [Fact]
        public async Task PurchaseAlcoholAuthorizationTest()
        {
            var authorizedUser = UserFactory.CreateUser(i =>
            {
                i.AddClaim(new Claim(ClaimTypes.DateOfBirth, new DateTime(1986, 3, 10).ToString(), ClaimValueTypes.DateTime, "http://contoso.com"));
            });

            var unauthorizedUser = UserFactory.CreateUser(i =>
            {
                i.AddClaim(new Claim(ClaimTypes.DateOfBirth, new DateTime(2006, 3, 10).ToString(), ClaimValueTypes.DateTime, "http://contoso.com"));
            });

            var bus = BusFactory.CreateBus(options =>
            {
                options.Authorization.Options.AddPolicy("Over21", new AuthorizationPolicy(new IAuthorizationRequirement[] { new MinimumAgeRequirement(21) }));
                options.Authorization.AddAuthorizationHandler<MinimumAgeHandler>();
                options.AddMessageAuthorization<PurchaseAlcohol>("Over21");
            });

            Assert.True(await bus.Authorize(authorizedUser, new PurchaseAlcohol()));
            Assert.False(await bus.Authorize(unauthorizedUser, new PurchaseAlcohol()));
        }
    }
}
