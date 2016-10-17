namespace NArchitecture.Events
{
    public class EventOptions : BaseOptions
    {
        public EventOptions() : base(typeof(IEventHandler)) { }

        public void AddEventHandler<TEventHandler>()
            where TEventHandler : class, IEventHandler
        {
            handlers.Add(typeof(TEventHandler));
        }
    }
}
