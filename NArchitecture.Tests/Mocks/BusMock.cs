using System;
using System.Security.Claims;
using System.Threading.Tasks;

namespace NArchitecture.Tests
{
    public class BusMock : IBus
    {
        public Task<bool> Authorize(ClaimsPrincipal user, IMessage message)
        {
            throw new NotImplementedException();
        }

        public Task Notify(IEvent @event)
        {
            throw new NotImplementedException();
        }

        public Task Request(IRequest request)
        {
            throw new NotImplementedException();
        }

        public Task<TResponse> Request<TResponse>(IRequest<TResponse> request)
        {
            throw new NotImplementedException();
        }

        public Task Validate(IMessage message)
        {
            throw new NotImplementedException();
        }
    }
}
