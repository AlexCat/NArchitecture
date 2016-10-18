using NArchitecture.Requests;
using System;
using System.Threading.Tasks;

namespace NArchitecture.Tests.Requests
{
    public class ComplexRequestHandlerFailing : RequestHandler<ComplexRequest, int>
    {
        protected override Task Handle(RequestHandlerContext<ComplexRequest, int> context)
        {
            throw new InvalidOperationException();
        }
    }
}
