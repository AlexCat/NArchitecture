namespace NArchitecture
{
    public class RequestHandlerContext
    {
        public IBus Bus { get; }
        public IMessage Request { get; }
        public object Response { get; set; }

        public RequestHandlerContext(IBus bus, IMessage request)
        {
            Bus = bus;
            Request = request;
        }
    }

    public class RequestHandlerContext<TRequest> : RequestHandlerContext
        where TRequest : IRequest
    {
        public new TRequest Request { get { return (TRequest)base.Request; } }

        public RequestHandlerContext(IBus bus, TRequest request) : base(bus, request) { }
    }

    public class RequestHandlerContext<TRequest, TResponse> : RequestHandlerContext
        where TRequest : IRequest<TResponse>
    {
        public new TRequest Request { get { return (TRequest)base.Request; } }
        public new TResponse Response
        {
            get { return (TResponse)base.Response; }
            set { base.Response = value; }
        }

        public RequestHandlerContext(IBus bus, TRequest request) : base(bus, request) { }
    }
}
