using System.Security.Claims;

namespace NArchitecture
{
    public abstract class BaseHandlerContext
    {
        public IServiceBus ServiceBus { get; }
        public ClaimsPrincipal User { get; }

        public BaseHandlerContext(IServiceBus bus, ClaimsPrincipal user)
        {
            ServiceBus = bus;
            User = user;
        }
    }
}
