using System.Threading.Tasks;

namespace NArchitecture.Tests
{
    public class SimpleEventHandler : EventHandler<SimpleEvent>
    {
        protected override Task Handle(EventHandlerContext context, SimpleEvent @event)
        {
            return Task.FromResult(0);
        }
    }
}
