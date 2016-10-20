namespace NArchitecture
{
    public class RequestHandlerContext
    {
        public IServiceBus ServiceBus { get; }

        public RequestHandlerContext(IServiceBus bus)
        {
            ServiceBus = bus;
        }
    }

    public class RequestHandlerContext<TResponse> : RequestHandlerContext
    {
        public TResponse Response { get; set; }

        public RequestHandlerContext(IServiceBus bus) : base(bus) { }
    }
}
