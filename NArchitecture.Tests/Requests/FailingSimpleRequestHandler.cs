using NArchitecture;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace NArchitecture.Tests
{
    internal class FailingSimpleRequestHandler : IHandleRequest<SimpleRequest>
    {
        public Task Handle(SimpleRequest request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
