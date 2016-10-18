using System.Threading.Tasks;

namespace NArchitecture
{
    public interface IRequestService
    {
        Task Request(IBus bus, IRequest request);
        Task<TResponse> Request<TResponse>(IBus bus, IRequest<TResponse> request);
    }
}
