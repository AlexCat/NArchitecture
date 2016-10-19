using System.Threading.Tasks;

namespace NArchitecture.Tests
{
    public class SimpleEventHandlerFailing : EventHandler<SimpleEvent>
    {
        protected override Task Handle(EventHandlerContext context, SimpleEvent @event)
        {
            throw new System.InvalidOperationException();
        }
    }
}
