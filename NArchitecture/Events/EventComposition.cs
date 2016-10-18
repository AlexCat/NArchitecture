namespace NArchitecture.Events
{
    public class EventComposition : BaseComposition
    {
        public EventComposition() : base(typeof(IEventHandler)) { }

        public void AddEventHandler<TEventHandler>()
            where TEventHandler : class, IEventHandler
        {
            handlers.Add(typeof(TEventHandler));
        }
    }
}
