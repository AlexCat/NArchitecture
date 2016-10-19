using System;
using System.Threading.Tasks;
using Xunit;


namespace NArchitecture.Tests
{
    public class BusTests
    {
        [Fact(DisplayName = "Bus authorizes the message according to the configuration")]
        public async Task PurchaseAlcoholAuthorizationTest()
        {
            var authorizedUser = UserFactory.CreateUser(i =>
            {
                i.AddDateOfBirthClaim(new DateTime(1986, 3, 10));
            });

            var unauthorizedUser = UserFactory.CreateUser(i =>
            {
                i.AddDateOfBirthClaim(new DateTime(2006, 3, 10));
            });

            var bus = BusFactory.CreateBus(options =>
            {
                options.Authorization.Options.AddPolicy("Over21", p => p.AddRequirements(new MinimumAgeRequirement(21)));
                options.Authorization.AddAuthorizationHandler<MinimumAgeHandler>();
                options.AddMessageAuthorization<PurchaseAlcohol>("Over21");
            });

            Assert.True(await bus.Authorize(authorizedUser, new PurchaseAlcohol()));
            Assert.False(await bus.Authorize(unauthorizedUser, new PurchaseAlcohol()));
        }
    }
}
