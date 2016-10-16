using NArchitecture;
using System.Threading;
using System.Threading.Tasks;

namespace NArchitecture.Tests
{
    internal class SimpleRequestHandler : IHandleRequest<SimpleRequest>
    {
        public Task Handle(SimpleRequest request, CancellationToken cancellationToken)
        {
            return Task.FromResult(0);
        }
    }
}
