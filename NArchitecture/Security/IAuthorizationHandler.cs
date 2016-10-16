using System.Threading.Tasks;

namespace NArchitecture.Security
{
    public interface IAuthorizationHandler
    {
        Task Handle(AuthorizationHandlerContext context);
    }
}
