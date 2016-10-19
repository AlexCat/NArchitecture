using System.Threading.Tasks;

namespace NArchitecture.Tests
{
    public class ComplexRequestHandler : RequestHandler<ComplexRequest, int>
    {
        protected override Task Handle(RequestHandlerContext<int> context, ComplexRequest request)
        {
            return Task.FromResult(0);
        }
    }
}
