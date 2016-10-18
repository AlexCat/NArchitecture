using NArchitecture.Requests;
using System.Threading.Tasks;

namespace NArchitecture.Tests.Requests
{
    public class ComplexRequestHandler : RequestHandler<ComplexRequest, int>
    {
        protected override Task Handle(RequestHandlerContext<ComplexRequest, int> context)
        {
            return Task.FromResult(0);
        }
    }
}
