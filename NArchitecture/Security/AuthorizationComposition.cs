namespace NArchitecture
{
    public class AuthorizationComposition : BaseComposition
    {
        public AuthorizationComposition() : base(typeof(IAuthorizationHandler))
        {
            Options = new AuthorizationOptions();
        }

        public void AddAuthorizationHandler<TAuthorizationHandler>()
            where TAuthorizationHandler : class, IAuthorizationHandler
        {
            handlers.Add(typeof(TAuthorizationHandler));
        }

        public AuthorizationOptions Options { get; }
    }
}
