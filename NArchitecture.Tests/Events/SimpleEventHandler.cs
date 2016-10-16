using NArchitecture;
using System.Threading;
using System.Threading.Tasks;

namespace NArchitecture.Tests
{
    internal class SimpleEventHandler : IHandleEvent<SimpleEvent>
    {
        public Task Handle(SimpleEvent message, CancellationToken cancellationToken)
        {
            return Task.FromResult(0);
        }
    }
}
