namespace NArchitecture
{
    public class EventHandlerContext
    {
        public IBus Bus { get; }
        public IEvent Event { get; }

        public EventHandlerContext(IBus bus, IEvent @event)
        {
            Bus = bus;
            Event = @event;
        }
    }

    public class EventHandlerContext<TEvent> : EventHandlerContext
        where TEvent : IEvent
    {
        public new TEvent Event { get { return (TEvent)base.Event; } }

        public EventHandlerContext(IBus bus, TEvent @event) : base(bus, @event) { }
    }
}
