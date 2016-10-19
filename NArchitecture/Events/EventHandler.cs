using System.Threading.Tasks;

namespace NArchitecture
{
    public abstract class EventHandler<TEvent> : IEventHandler
        where TEvent : IEvent
    {
        public async virtual Task Handle(EventHandlerContext context, IEvent @event)
        {
            if (@event is TEvent)
            {
                await Handle(context, (TEvent)@event);
            }
        }

        protected abstract Task Handle(EventHandlerContext context, TEvent @event);
    }
}
