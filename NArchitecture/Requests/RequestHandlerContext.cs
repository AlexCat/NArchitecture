using System.Security.Claims;

namespace NArchitecture
{
    public class RequestHandlerContext : BaseHandlerContext
    {
        public RequestHandlerContext(IServiceBus bus, ClaimsPrincipal user) : base(bus, user) { }
    }

    public class RequestHandlerContext<TResponse> : RequestHandlerContext
    {
        public TResponse Response { get; set; }

        public RequestHandlerContext(IServiceBus bus, ClaimsPrincipal user) : base(bus, user) { }
    }
}
