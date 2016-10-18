using System.Threading.Tasks;

namespace NArchitecture.Events
{
    public abstract class EventHandler<TEvent> : IEventHandler
        where TEvent : IEvent
    {
        public async virtual Task Handle(EventHandlerContext context)
        {
            if (context is EventHandlerContext<TEvent>)
            {
                await Handle((EventHandlerContext<TEvent>)context);
            }
        }

        protected abstract Task Handle(EventHandlerContext<TEvent> context);
    }
}
