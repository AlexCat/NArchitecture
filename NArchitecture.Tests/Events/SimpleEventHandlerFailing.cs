using System.Threading.Tasks;

namespace NArchitecture.Tests
{
    public class SimpleEventHandlerFailing : EventHandler<SimpleEvent>
    {
        protected override Task Handle(EventHandlerContext<SimpleEvent> context)
        {
            throw new System.InvalidOperationException();
        }
    }
}
