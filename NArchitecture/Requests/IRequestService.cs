using System.Threading.Tasks;

namespace NArchitecture
{
    public interface IRequestService
    {
        Task Request(IServiceBus bus, IRequest request); 
        Task<TResponse> Request<TResponse>(IServiceBus bus, IRequest<TResponse> request);
    }
}
