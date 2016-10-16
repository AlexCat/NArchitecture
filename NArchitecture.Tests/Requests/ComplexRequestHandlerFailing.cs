using NArchitecture.Requests;
using System;
using System.Threading.Tasks;

namespace NArchitecture.Tests.Requests
{
    public class ComplexRequestHandlerFailing : RequestHandler<ComplexRequest, int>
    {
        protected override Task<int> Handle(ComplexRequest request)
        {
            throw new InvalidOperationException();
        }
    }
}
