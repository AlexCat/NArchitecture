using System.Threading.Tasks;
using System;

namespace NArchitecture.Tests
{
    public class SimpleRequestHandler : RequestHandler<SimpleRequest>
    {
        protected override Task Handle(RequestHandlerContext<SimpleRequest> context)
        {
            return Task.FromResult(0);
        }
    }
}
