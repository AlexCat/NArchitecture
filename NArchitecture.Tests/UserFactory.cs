using System;
using System.Security.Claims;

namespace NArchitecture
{
    public static class UserFactory
    {
        public static ClaimsPrincipal CreateUser(Action<ClaimsIdentity> configure)
        {
            var identity = new ClaimsIdentity();
            configure(identity);
            return new ClaimsPrincipal(identity);
        }
    }
}
