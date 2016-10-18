using NArchitecture.Requests;
using System.Threading.Tasks;
using System;

namespace NArchitecture.Tests.Requests
{
    public class SimpleRequestHandler : RequestHandler<SimpleRequest>
    {
        protected override Task Handle(RequestHandlerContext<SimpleRequest> context)
        {
            return Task.FromResult(0);
        }
    }
}
