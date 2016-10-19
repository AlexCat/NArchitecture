using System.Threading.Tasks;

namespace NArchitecture.Tests
{
    public class SimpleEventHandler : EventHandler<SimpleEvent>
    {
        protected override Task Handle(EventHandlerContext<SimpleEvent> context)
        {
            return Task.FromResult(0);
        }
    }
}
