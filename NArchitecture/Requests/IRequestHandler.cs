using System.Threading.Tasks;

namespace NArchitecture
{
    public interface IRequestHandler
    {
        bool CanHandle(IRequest request);
        Task Handle(RequestHandlerContext context, IRequest request);
    }
}
