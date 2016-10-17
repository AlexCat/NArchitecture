namespace NArchitecture.Requests
{
    public class RequestOptions : BaseOptions
    {
        public RequestOptions() : base(typeof(IRequestHandler)) { }

        public void AddRequestHandler<TRequestHandler>()
            where TRequestHandler : class, IRequestHandler
        {
            handlers.Add(typeof(TRequestHandler));
        }
    }
}
