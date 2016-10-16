using NArchitecture.Events;
using System.Threading.Tasks;

namespace NArchitecture.Tests.Events
{
    public class SimpleEventHandlerFailing : EventHandler<SimpleEvent>
    {
        protected override Task Handle(SimpleEvent @event)
        {
            throw new System.InvalidOperationException();
        }
    }
}
