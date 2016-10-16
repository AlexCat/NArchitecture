using NArchitecture;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace NArchitecture.Tests
{
    internal class FailingComplexRequestHandler : IHandleRequest<ComplexRequest, int>
    {
        public Task<int> Handle(ComplexRequest request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
