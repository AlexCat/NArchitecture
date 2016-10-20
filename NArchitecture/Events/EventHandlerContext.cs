using System.Security.Claims;

namespace NArchitecture
{
    public class EventHandlerContext : BaseHandlerContext
    {
        public EventHandlerContext(IServiceBus bus, ClaimsPrincipal user) : base(bus, user) { }
    }
}
