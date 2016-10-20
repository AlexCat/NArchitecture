using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace NArchitecture
{
    public class DefaultEventService : IEventService
    {
        private readonly IList<IEventHandler> handlers;

        public DefaultEventService(IEnumerable<IEventHandler> handlers)
        {
            this.handlers = handlers.ToArray();
        }

        public async Task Notify(IServiceBus bus, ClaimsPrincipal user, IEvent @event)
        {
            Guard.AgainstNull(nameof(bus), bus);
            Guard.AgainstNull(nameof(user), user);
            Guard.AgainstNull(nameof(@event), @event);

            var context = new EventHandlerContext(bus, user);
            var exceptions = new List<Exception>();

            foreach(var handler in handlers)
            {
                try
                {
                    await handler.Handle(context, @event);
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
