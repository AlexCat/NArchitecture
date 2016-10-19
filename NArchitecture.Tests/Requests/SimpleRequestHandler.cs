using System.Threading.Tasks;
using System;

namespace NArchitecture.Tests
{
    public class SimpleRequestHandler : RequestHandler<SimpleRequest>
    {
        protected override Task Handle(RequestHandlerContext context, SimpleRequest request)
        {
            return Task.FromResult(0);
        }
    }
}
