using System.Security.Claims;
using System.Threading.Tasks;

namespace NArchitecture
{
    public interface IRequestService
    {
        Task Request(IServiceBus bus, ClaimsPrincipal user, IRequest request); 
        Task<TResponse> Request<TResponse>(IServiceBus bus, ClaimsPrincipal user, IRequest<TResponse> request);
    }
}
