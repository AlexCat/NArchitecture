using System.Threading.Tasks;

namespace NArchitecture
{
    public interface IAuthorizationHandler
    {
        Task Handle(AuthorizationHandlerContext context);
    }
}
