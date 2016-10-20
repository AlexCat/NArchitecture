namespace NArchitecture
{
    public class EventHandlerContext
    {
        public IServiceBus ServiceBus { get; }

        public EventHandlerContext(IServiceBus bus)
        {
            ServiceBus = bus;
        }
    }
}
