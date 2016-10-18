using NArchitecture.Requests;
using System;
using System.Threading.Tasks;

namespace NArchitecture.Tests.Requests
{
    public class SimpleRequestHandlerFailing : RequestHandler<SimpleRequest>
    {
        protected override Task Handle(RequestHandlerContext<SimpleRequest> context)
        {
            throw new InvalidOperationException();
        }
    }
}
