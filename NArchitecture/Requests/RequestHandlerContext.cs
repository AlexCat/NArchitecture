namespace NArchitecture
{
    public class RequestHandlerContext
    {
        public IBus Bus { get; }

        public RequestHandlerContext(IBus bus)
        {
            Bus = bus;
        }
    }

    public class RequestHandlerContext<TResponse> : RequestHandlerContext
    {
        public TResponse Response { get; set; }

        public RequestHandlerContext(IBus bus) : base(bus) { }
    }
}
