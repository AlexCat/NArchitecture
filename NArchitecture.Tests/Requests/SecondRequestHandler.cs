using NArchitecture;
using System.Threading;
using System.Threading.Tasks;

namespace NArchitecture.Tests
{
    internal class SecondRequestHandler : MessageHandler, IHandleRequest<SecondRequest>
    {
        public Task Handle(SecondRequest message, CancellationToken cancellationToken = default(CancellationToken))
        {
            return Task.FromResult(0);
        }
    }
}
