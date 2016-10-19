namespace NArchitecture
{
    public class RequestComposition : BaseComposition
    {
        public RequestComposition() : base(typeof(IRequestHandler)) { }

        public void AddRequestHandler<TRequestHandler>()
            where TRequestHandler : class, IRequestHandler
        {
            handlers.Add(typeof(TRequestHandler));
        }
    }
}
