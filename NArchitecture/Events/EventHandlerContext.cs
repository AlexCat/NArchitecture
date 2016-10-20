using System.Security.Claims;

namespace NArchitecture
{
    public class EventHandlerContext
    {
        public IServiceBus ServiceBus { get; }
        public ClaimsPrincipal User { get; }

        public EventHandlerContext(IServiceBus bus, ClaimsPrincipal user)
        {
            ServiceBus = bus;
            User = user;
        }
    }
}
