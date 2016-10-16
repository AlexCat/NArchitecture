using System.Threading.Tasks;

namespace NArchitecture
{
    public interface IRequestService
    {
        Task Send(IBus bus, IRequest request);
        Task<TResponse> Send<TResponse>(IBus bus, IRequest<TResponse> request);
    }
}
