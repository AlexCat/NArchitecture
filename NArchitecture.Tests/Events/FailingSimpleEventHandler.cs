using NArchitecture;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace NArchitecture.Tests
{
    internal class FailingSimpleEventHandler : IHandleEvent<SimpleEvent>
    {
        public Task Handle(SimpleEvent message, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
