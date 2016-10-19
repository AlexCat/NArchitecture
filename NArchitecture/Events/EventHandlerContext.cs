namespace NArchitecture
{
    public class EventHandlerContext
    {
        public IBus Bus { get; }

        public EventHandlerContext(IBus bus)
        {
            Bus = bus;
        }
    }
}
