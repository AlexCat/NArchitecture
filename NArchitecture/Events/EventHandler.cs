using System.Threading.Tasks;

namespace NArchitecture.Events
{
    public abstract class EventHandler<TEvent> : IEventHandler
        where TEvent : IEvent
    {
        public async virtual Task Handle(IEvent @event)
        {
            if (@event is TEvent)
            {
                await Handle((TEvent)@event);
            }
        }

        protected abstract Task Handle(TEvent @event);
    }
}
