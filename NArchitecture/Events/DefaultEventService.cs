using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NArchitecture.Events
{
    public class DefaultEventService : IEventService
    {
        private readonly IList<IEventHandler> handlers;

        public DefaultEventService(IEnumerable<IEventHandler> handlers)
        {
            this.handlers = handlers.ToArray();
        }

        public async Task Notify(IBus bus, IEvent @event)
        {
            Guard.AgainstNull(nameof(bus), bus);
            Guard.AgainstNull(nameof(@event), @event);

            var exceptions = new List<Exception>();

            foreach(var handler in handlers)
            {
                try
                {
                    await handler.Handle(@event);
                }
                catch(Exception ex)
                {
                    exceptions.Add(ex);
                }
            }

            if (exceptions.Count > 0)
            {
                throw new AggregateException(exceptions);
            }
        }
    }
}
