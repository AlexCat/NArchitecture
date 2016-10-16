namespace NArchitecture.Requests
{
    public class RequestHandlerContext
    {
        private object response;

        public RequestHandlerContext(IBus bus, IRequest request)
        {
            Bus = bus;
            Request = request;
        }

        public IBus Bus { get; }
        public IRequest Request { get; }
        public object Response { get { return response; } }

        public void Respond(object response)
        {
            this.response = response;
        }
    }
}
