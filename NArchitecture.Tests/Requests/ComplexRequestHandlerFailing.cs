using System;
using System.Threading.Tasks;

namespace NArchitecture.Tests
{
    public class ComplexRequestHandlerFailing : RequestHandler<ComplexRequest, int>
    {
        protected override Task Handle(RequestHandlerContext<int> context, ComplexRequest request)
        {
            throw new InvalidOperationException();
        }
    }
}
