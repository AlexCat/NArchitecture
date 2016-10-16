using NArchitecture;
using System.Threading.Tasks;
using System.Threading;

namespace NArchitecture.Tests
{
    internal class ValidatableRequestHandler : IHandleRequest<ValidatableRequest>
    {
        public Task Handle(ValidatableRequest request, CancellationToken cancellationToken = default(CancellationToken))
        {
            return Task.FromResult(0);
        }
    }
}
