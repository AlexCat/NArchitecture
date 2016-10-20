using System.Security.Claims;

namespace NArchitecture
{
    public class RequestHandlerContext
    {
        public IServiceBus ServiceBus { get; }
        public ClaimsPrincipal User { get; }

        public RequestHandlerContext(IServiceBus bus, ClaimsPrincipal user)
        {
            ServiceBus = bus;
            User = user;
        }
    }

    public class RequestHandlerContext<TResponse> : RequestHandlerContext
    {
        public TResponse Response { get; set; }

        public RequestHandlerContext(IServiceBus bus, ClaimsPrincipal user) : base(bus, user) { }
    }
}
