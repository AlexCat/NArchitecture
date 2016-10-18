using System;
using System.Security.Claims;

namespace NArchitecture.Tests
{
    public static class UserFactory
    {
        public static ClaimsPrincipal CreateUser(Action<ClaimsIdentity> configure)
        {
            var identity = new ClaimsIdentity();
            configure(identity);
            return new ClaimsPrincipal(identity);
        }

        public static void AddDateOfBirthClaim(this ClaimsIdentity identity, DateTime dateOfBirth)
        {
            identity.AddClaim(new Claim(ClaimTypes.DateOfBirth, dateOfBirth.ToString(), ClaimValueTypes.DateTime, "http://example.com"));
        }
    }
}
